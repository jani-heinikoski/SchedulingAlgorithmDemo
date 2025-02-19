namespace Domain
{
    public class Process
    {

        private readonly uint _pid;
        public uint Pid => _pid;

        private ushort _burstTime;
        public ushort BurstTime => _burstTime;

        private ushort _remainingBurstTime;
        public ushort RemainingBurstTime => _remainingBurstTime;

        private readonly ushort _arrivalTime;
        public ushort ArrivalTime => _arrivalTime;

        public Process(uint pid, ushort burstTime, ushort arrivalTime)
        {
            _pid = pid;
            _burstTime = burstTime;
            _arrivalTime = arrivalTime;
            _remainingBurstTime = burstTime;
        }

        public void RunFor(ushort time)
        {
            if (_remainingBurstTime > time)
            {
                _remainingBurstTime -= time;
            }
            else
            {
                _remainingBurstTime = 0;
            }
        }

        public bool IsCompleted()
        {
            return _remainingBurstTime == 0;
        }
    }
}
