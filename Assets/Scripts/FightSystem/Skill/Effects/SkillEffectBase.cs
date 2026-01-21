//Author : _SourceCode
//CreateTime : 2026-01-20-15:27:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Skill.Interfaces;
using MyFrame.FightSystem.Skill.Refs;
using System.Text;

namespace MyFrame.FightSystem.Skill.Effects
{
    public abstract class SkillEffectBase : ISkillEffect
    {
        protected readonly SkillEffectInfo _effectInfo;
        
        /// <summary>
        /// Runtime Id, for tracing purpose,The best is :1,2,3...
        /// </summary>
        protected string _runtimeId;
        protected string _skillId;

        protected SkillEffectBase _next;
        /// <summary>
        /// Execute Effect Logic
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        protected abstract EffectResult OnExecute(in SkillEffectContext ctx);

        public SkillEffectBase(SkillEffectInfo effectInfo,  string runtimeId ,string skillId, SkillEffectBase next)
        {
            _effectInfo = effectInfo;
            _runtimeId = runtimeId;
            _skillId = skillId;
            _next = next;
        }

        public EffectReport Execute(in SkillEffectContext ctx)
        {
            var sb = new StringBuilder(256);

            var id = string.IsNullOrWhiteSpace(_runtimeId) ? GetType().Name : _runtimeId;
            var indent = new string(' ', ctx.Trace.Depth * 2);

            sb.AppendLine($"{indent}[Enter] {id} target={_effectInfo .TargetType} desc={_effectInfo.Descreption}");

            // Condition
            if (_effectInfo.Condition != null)
            {
                var c = _effectInfo.Condition.Check(ctx.ConditionContxt);
                sb.AppendLine($"{indent}  [Condition] success={c.Success} msg={c.MessageKey}");

                if (!c.Success)
                {
                    var skipped = EffectResult.Skip("ConditionNotMet", "Effect condition not met: " + c.MessageKey);
                    ctx.Trace?.Record(id, skipped);
                    sb.AppendLine($"{indent}  [Result] status=Skipped code={skipped.Code} applied={skipped.Applied} msg={skipped.MessageKey}");

                    TryRunNext(ctx, skipped, sb);
                    sb.AppendLine($"{indent}[Exit] {id}");
                    return new EffectReport(sb.ToString());
                }
            }

            // Dependency
            if (_effectInfo.Dependency != null)
            {
                var ok = _effectInfo.Dependency.Evaluate(ctx.Trace);
                sb.AppendLine($"{indent}  [Dependency] ok={ok} dep={_effectInfo.Dependency.GetType().Name}");

                if (!ok)
                {
                    var skipped = EffectResult.Skip("DependencyNotMet", "Effect dependency not met");
                    ctx.Trace?.Record(id, skipped);
                    sb.AppendLine($"{indent}  [Result] status=Skipped code={skipped.Code} applied={skipped.Applied} msg={skipped.MessageKey}");

                    TryRunNext(ctx, skipped, sb);
                    sb.AppendLine($"{indent}[Exit] {id}");
                    return new EffectReport(sb.ToString());
                }
            }

            // Execute
            sb.AppendLine($"{indent}  [Execute] OnExecute start");
            var res = OnExecute(ctx);
            sb.AppendLine($"{indent}  [Execute] OnExecute end status={res.Status} applied={res.Applied} code={res.Code} msg={res.MessageKey}");

            // Record
            ctx.Trace?.Record(id, res);
            sb.AppendLine($"{indent}  [Record] Trace.Record({id})");

            // Next
            TryRunNext(ctx, res, sb);

            sb.AppendLine($"{indent}[Exit] {id}");
            return new EffectReport(sb.ToString());
        }

        private void TryRunNext(in SkillEffectContext ctx, in EffectResult current, StringBuilder sb)
        {
            var indent = new string(' ', ctx.Trace.Depth * 2);

            if (_next == null)
            {
                sb.AppendLine($"{indent}  [Next] null");
                return;
            }

            bool go = (current.Status & _effectInfo.NextFlow) != 0;

            sb.AppendLine($"{indent}  [Next] go={go} flow={_effectInfo.NextFlow} next={_next.GetType().Name}");

            if (!go) return;

            ctx.Trace.Depth++;
            var nextReport = _next.Execute(ctx);
            ctx.Trace.Depth--;

            if (!string.IsNullOrWhiteSpace(nextReport.Message))
            {
                sb.Append(nextReport.Message);
            }
        }
    }



}