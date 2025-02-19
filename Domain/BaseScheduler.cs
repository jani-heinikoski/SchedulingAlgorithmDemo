namespace Domain
{
    public abstract class BaseScheduler : ISchedulerSimulator
    {
        protected ulong _currentTime = 0;

        protected readonly List<Process> _processes;

        protected readonly InformationTracker _infoTracker;
        public InformationTracker InfoTracker => _infoTracker;

        public BaseScheduler()
        {
            _infoTracker = new();
            _processes = [];
        }

        protected abstract void EnqueueProcess(Process process);

        protected virtual void EnqueueProcessesFromList()
        {
            if (_processes == null)
            {
                return;
            }
            Predicate<Process> arrivalTimePredicate = process => process.ArrivalTime == _currentTime;
            List<Process> enqueueProcesses = _processes.FindAll(arrivalTimePredicate);
            _processes.RemoveAll(arrivalTimePredicate);
            foreach (Process process in enqueueProcesses)
            {
                EnqueueProcess(process);
            }
        }

        protected void NextTimeStep()
        {
            if (_currentTime < ulong.MaxValue)
            {
                _currentTime++;
            }
            else
            {
                throw new OverflowException($"_currentTime counter has overflown. Max. time is ${ulong.MaxValue}.");
            }
        }

        protected abstract bool IsFinished();

        protected abstract void Schedule();

        public virtual void ScheduleAll(List<Process> processes)
        {
            _processes.Clear();
            _processes.AddRange(processes);
            while (!IsFinished())
            {
                Schedule();
            }
            IsFinished();
        }
    }
}
