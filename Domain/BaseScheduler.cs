namespace Domain
{
    public abstract class BaseScheduler : ISchedulerSimulator
    {
        protected ulong _currentTimeStep = 0;

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
            bool arrivalTimePredicate(Process process) => process.ArrivalTime == _currentTimeStep;
            List<Process> enqueueProcesses = _processes.FindAll(arrivalTimePredicate);
            _processes.RemoveAll(arrivalTimePredicate);
            foreach (Process process in enqueueProcesses)
            {
                EnqueueProcess(process);
            }
        }

        protected virtual void NextTimeStep()
        {
            if (_currentTimeStep < ulong.MaxValue)
            {
                _currentTimeStep++;
            }
            else
            {
                throw new OverflowException($"_currentTime counter has overflown. Max. time is ${ulong.MaxValue}.");
            }
            EnqueueProcessesFromList();
        }

        protected abstract bool IsFinished();

        protected abstract void Schedule();

        protected virtual void RunProcessFor(Process process, ushort time)
        {
            process.RunFor(time);
            InfoTracker.GiveCPUTime(process, time);
        }

        protected virtual void IdleCPUFor(ushort time)
        {
            InfoTracker.CPUIdle(time);
        }

        public virtual void ScheduleAll(List<Process> processes)
        {
            _processes.Clear();
            _processes.AddRange(processes);
            EnqueueProcessesFromList();
            while (!IsFinished())
            {
                Schedule();
            }
            IsFinished();
        }
    }
}
