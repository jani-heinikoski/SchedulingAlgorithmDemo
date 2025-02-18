using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MLFQScheduler : IScheduler
    {
        private readonly IScheduler[] _schedulers;
        private readonly Queue<Process>[] _queues;

        public MLFQScheduler(IScheduler[] schedulers)
        {
            if (schedulers == null)
            {
                throw new ArgumentNullException(nameof(schedulers));
            }
            if (schedulers.Length <= 0)
            {
                throw new ArgumentException("At least one scheduler must be provided", nameof(schedulers));
            }
            _schedulers = schedulers;
            _queues = new Queue<Process>[schedulers.Length];
            for (int i = 0; i < _queues.Length; i++)
            {
                _queues[i] = new Queue<Process>();
            }
        }

        public void OnProcessArrived(Process process)
        {
            _queues[0].Enqueue(process);
        }

        public void ProcessOneTimeStep()
        {
            throw new NotImplementedException();
        }
    }
}
