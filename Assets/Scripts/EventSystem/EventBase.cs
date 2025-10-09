//Author : _SourceCode
//CreateTime : 2025-09-10-14:01:26
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MyFrame.FightSystem.Event
{
    // ======================== 扩展面向接口（便于后续演进） ========================

    /// <summary>
    /// 核心事件总线接口：仅保留最基础能力。
    /// 保持接口稳定，便于未来替换实现（内存版、分布式版、带中间件版等）。
    /// </summary>
    public interface IEventBusCore
    {
        /// <summary>订阅指定类型的事件，返回可释放的退订句柄。</summary>
        IDisposable Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent;
        /// <summary>发布事件（同步、立即分发）。</summary>
        void Publish<TEvent>(TEvent evt) where TEvent : IEvent;
    }

    /// <summary>
    /// 异常处理策略接口。通过注入自定义实现可接入日志/告警系统。
    /// </summary>
    public interface IEventErrorHandler
    {
        void OnPublishException(object evt, Exception ex);
        void OnHandlerException(object evt, Delegate handler, Exception ex);
    }

    /// <summary>
    /// 默认异常处理：开发期打印，生产可替换为 ILogger / Sentry / 自研监控。
    /// </summary>
    public sealed class DefaultErrorHandler : IEventErrorHandler
    {
        public void OnPublishException(object evt, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[EventBus] Publish error for {evt.GetType().Name}: {ex}");
        }
        public void OnHandlerException(object evt, Delegate handler, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[EventBus] Handler {handler.Method.DeclaringType?.Name}.{handler.Method.Name} error: {ex}");
        }
    }

    // ======================== 最小可用实现（线程安全） ========================

    /// <summary>
    /// 内存事件总线（极简）：
    /// - 同步分发；
    /// - 线程安全（全局锁）；
    /// - 数据结构简单：Type -> List<Delegate>；
    /// - 可扩展：保留构造参数与受保护方法，便于后续接入中间件、优先级、过滤器、粘性事件、异步队列等。
    /// </summary>
    public class EventBusCore : IEventBusCore
    {
        protected readonly object Gate = new();
        protected readonly Dictionary<Type, List<Delegate>> Subscribers = new();
        protected readonly IEventErrorHandler ErrorHandler;

        /// <summary>
        /// 可注入异常处理器；未来也可在此注入 DI 容器、分发策略等。
        /// </summary>
        public EventBusCore(IEventErrorHandler? errorHandler = null)
        {
            ErrorHandler = errorHandler ?? new DefaultErrorHandler();
        }

        /// <inheritdoc />
        public virtual IDisposable Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            lock (Gate)
            {
                var key = typeof(TEvent);
                if (!Subscribers.TryGetValue(key, out var list))
                {
                    list = new List<Delegate>();
                    Subscribers[key] = list;
                }
                list.Add(handler);
            }

            return new Unsubscriber(this, typeof(TEvent), handler);
        }

        /// <inheritdoc />
        public virtual void Publish<TEvent>(TEvent evt) where TEvent : IEvent
        {
            if (evt == null) throw new ArgumentNullException(nameof(evt));

            List<Delegate>? snapshot = null;
            try
            {
                // 快照以避免分发过程中集合被修改导致的并发问题
                lock (Gate)
                {
                    if (Subscribers.TryGetValue(typeof(TEvent), out var list) && list.Count > 0)
                        snapshot = list.ToList();
                }

                if (snapshot == null) return;

                foreach (var d in snapshot)
                {
                    try
                    {
                        // 强类型调度：Action<TEvent>
                        if (d is Action<TEvent> typed)
                        {
                            typed(evt);
                        }
                        else
                        {
                            // 兼容边界（一般不会触发）：
                            d.DynamicInvoke(evt);
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorHandler.OnHandlerException(evt!, d, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.OnPublishException(evt!, ex);
            }
        }

        // ----------------------- 退订句柄 -----------------------
        private sealed class Unsubscriber : IDisposable
        {
            private readonly EventBusCore _bus;
            private readonly Type _type;
            private readonly Delegate _handler;
            private int _disposed; // 0/1

            public Unsubscriber(EventBusCore bus, Type type, Delegate handler)
            { _bus = bus; _type = type; _handler = handler; }

            public void Dispose()
            {
                if (Interlocked.Exchange(ref _disposed, 1) == 1) return;
                lock (_bus.Gate)
                {
                    if (_bus.Subscribers.TryGetValue(_type, out var list))
                    {
                        list.Remove(_handler);
                        if (list.Count == 0) _bus.Subscribers.Remove(_type);
                    }
                }
            }
        }

        // ======================== 可扩展挂点（预留） ========================
        // 1) 若要支持“优先级”：将 List<Delegate> 替换为 List<Subscription>，包含 Priority；Subscribe 时排序；
        // 2) 若要支持“过滤器/MaxCalls”：同上，Subscription 内增属性与计数；
        // 3) 若要支持“粘性事件”：增加 Dictionary<Type, object> Sticky；PublishSticky/SubscribeSticky；
        // 4) 若要支持“中间件”：在 Publish 中外包一层 Dispatch(context, next) 管道；
        // 5) 若要支持“异步队列”：将 PublishAsync 入队（Channel/BlockingCollection），单消费者线程消费后仍复用 Dispatch；
        // 6) 若要支持“分布式”：保留 IEventBusCore 接口，新增 Redis/RabbitMQ/Kafka 实现，或在本地实现外再做桥接适配器。
    }

}

