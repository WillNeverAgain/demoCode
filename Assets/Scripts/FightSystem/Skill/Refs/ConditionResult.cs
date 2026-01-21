//Author : _SourceCode
//CreateTime : 2026-01-14-17:17:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
namespace MyFrame.FightSystem.Skill.Refs
{
    public readonly struct ConditionResult
    {
        public readonly bool Success;
        public readonly string Code;
        public readonly string MessageKey;
        /// <summary>
        /// Message Arguments
        /// </summary>
        public readonly object[] Args;

        private ConditionResult(bool success, string code, string messageKey, object[] args)
        {
            Success = success;
            Code = code;
            MessageKey = messageKey;
            Args = args;
        }

        public static ConditionResult Ok(string messageKey = null, params object[] args) => new(true, string.Empty, messageKey, args);

        public static ConditionResult Fail(string code, string messageKey = null, params object[] args)
            => new(false, code, messageKey, args);

        public override string ToString()
            => Success ? "OK" : $"FAIL({Code}) {MessageKey}";
    }


    public enum EffectStatus
    {
        None = 0,
        Success = 1 << 0,
        Fail = 1 << 1,
        Skipped = 1 << 2,

        Alays = Success | Fail | Skipped,
    }

    public readonly struct EffectResult
    {
        public readonly EffectStatus Status;
        public readonly bool Applied;
        public readonly string Code;
        public readonly string MessageKey;

        public bool Success => Status == EffectStatus.Success;

        private EffectResult(EffectStatus status, bool applied, string code, string msg)
        {
            Status = status;
            Applied = applied;
            Code = code;
            MessageKey = msg;
        }

        public static EffectResult Ok(string msg, bool applied = true)
            => new(EffectStatus.Success, applied, "", msg);

        public static EffectResult Fail(string code, string msg)
            => new(EffectStatus.Fail, false, code, msg);

        public static EffectResult Skip(string code, string msg)
            => new(EffectStatus.Skipped, false, code, msg);
    }

}
