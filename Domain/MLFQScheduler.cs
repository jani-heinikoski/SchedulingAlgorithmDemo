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
        private readonly IScheduler[] schedulers;
        private readonly Queue<Process>[] queues;


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
            this.schedulers = schedulers;
            this.queues = new Queue<Process>[schedulers.Length];
            for (int i = 0; i < this.queues.Length; i++)
            {
                this.queues[i] = new Queue<Process>();
            }
        }

        public void OnProcessArrived(Process process)
        {
            this.queues[0].Enqueue(process);
        }

        public void ProcessOneTimeStep()
        {
            throw new NotImplementedException();
        }
    }
}
