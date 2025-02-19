namespace Domain
{
    public class RRScheduler : BaseScheduler
    {
        private readonly Queue<Process> _readyQueue;
        private readonly ushort _timeQuantum;

        public RRScheduler(ushort timeQuantum) : base()
        {
            _readyQueue = new();
            _timeQuantum = timeQuantum;
        }

        override protected void EnqueueProcess(Process process)
        {
            _readyQueue.Enqueue(process);
        }

        override protected void Schedule()
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
                    NextTimeStep();
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

        override protected bool IsFinished()
        {
            return _processes.Count == 0 && _readyQueue.Count == 0;
        }
    }
}
