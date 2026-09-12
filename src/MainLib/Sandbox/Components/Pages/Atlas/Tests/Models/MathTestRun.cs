namespace Sandbox.Components.Pages.Atlas.Tests.Models
{
    internal enum MathTestRunStatus
    {
        None,
        Success,
        Failed,
    }

    internal class MathTestRun(string name)
    {
        public string Name { get; } = name.Replace("Test", "");
        public MathTestRunStatus Status { get; set; }
        public string? Result { get; set; }
        public Exception? Exception { get; set; }
        public string? Error => Exception?.Message;
    }
}
