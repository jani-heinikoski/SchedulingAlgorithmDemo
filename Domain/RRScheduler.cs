namespace Domain
{
    public class RRScheduler : ISchedulerSimulator
    {
        private readonly Queue<Process> _readyQueue;
        private readonly ushort _timeQuantum;
        private readonly List<Process> _processes;
        private ulong _currentTime = 0;

        private readonly InformationTracker _infoTracker;
        public InformationTracker InfoTracker => _infoTracker;

        public List<Process> Processes => _processes;

        public RRScheduler(ushort timeQuantum, List<Process> processes)
        {
            _readyQueue = new();
            _timeQuantum = timeQuantum;
            _infoTracker = new();
            _processes = processes;
        }

        private void EnqueueProcessesFromList()
        {
            Predicate<Process> arrivalTimePredicate = process => process.ArrivalTime == _currentTime;
            List<Process> enqueueProcesses = _processes.FindAll(arrivalTimePredicate);
            _processes.RemoveAll(arrivalTimePredicate);
            foreach (Process process in enqueueProcesses)
            {
                _readyQueue.Enqueue(process);
            }
        }

        private void IncreaseCurrentTime()
        {
            // Fail safe to prevent infinite loop
            if (_currentTime < ulong.MaxValue)
            {
                _currentTime++;
            }
            else
            {
                throw new InvalidOperationException("Current time has reached the maximum value");
            }
        }

        private protected void Schedule()
        {
            EnqueueProcessesFromList();
            // Check if there are processes to be scheduled
            if (_readyQueue.Count > 0)
            {
                Process process = _readyQueue.Dequeue();
                for (ushort u = 0; u < _timeQuantum; ++u)
                {
                    process.RunFor(1);
                    InfoTracker.GiveCPUTime(process, 1);
                    IncreaseCurrentTime();
                    EnqueueProcessesFromList();
                    // Check if process has completed
                    if (process.IsCompleted())
                    {
                        break;
                    }
                }
                // Check if process has not completed
                if (!process.IsCompleted())
                {
                    _readyQueue.Enqueue(process);
                }
            }
            else
            {
                // CPU idles for 1 time unit because no processes to schedule in the ready queue
                InfoTracker.CPUIdle(1);
            }
        }

        public void ScheduleAll()
        {
            while (_processes.Count > 0 || _readyQueue.Count > 0)
            {
                Schedule();
            }
        }
    }
}
