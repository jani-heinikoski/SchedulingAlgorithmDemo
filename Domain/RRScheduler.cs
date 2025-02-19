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
            // Check if there are processes to be scheduled
            if (_readyQueue.Count > 0)
            {
                // Select the process at the front of the queue
                Process process = _readyQueue.Dequeue();
                // Give the process CPU time for the time quantum
                for (ushort u = 0; u < _timeQuantum; ++u)
                {
                    RunProcessFor(process, 1);
                    NextTimeStep();
                    // Stop if the process has completed
                    if (process.IsCompleted())
                    {
                        return;
                    }
                }
                EnqueueProcess(process);
            }
            else
            {
                // CPU idles for 1 time unit if no processes in the ready queue
                IdleCPUFor(1);
                NextTimeStep();
            }
        }

        override protected bool IsFinished()
        {
            return _processes.Count == 0 && _readyQueue.Count == 0;
        }
    }
}
