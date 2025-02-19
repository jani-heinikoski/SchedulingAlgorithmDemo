namespace Domain
{
    /// <summary>
    /// Interface representing a scheduler algorithm which is simulated by scheduling a process list.
    /// Scheduling information is stored in the InformationTracker.
    /// </summary>
    public interface ISchedulerSimulator
    {
        /// <summary>
        /// Consumes the processes list and schedules all processes.
        /// </summary>
        void ScheduleAll(List<Process> Processes);

        /// <summary>
        /// Gets the scheduling information.
        /// </summary>
        InformationTracker InfoTracker { get; }
    }
}
