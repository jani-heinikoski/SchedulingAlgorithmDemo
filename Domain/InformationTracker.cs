using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InformationTracker
    {

        private readonly List<Tuple<Process?, ushort>> _info;

        public InformationTracker()
        {
            _info = [];
        }

        public void GiveCPUTime(Process process, ushort time)
        {
            if (process == null)
            {
                throw new ArgumentNullException(nameof(process));
            }
            Tuple<Process?, ushort> tuple = new(process, time);
            _info.Add(tuple);
        }

        public void CPUIdle(ushort time)
        {
            if (time <= 0)
            {
                throw new ArgumentException("Time must be greater than 0", nameof(time));
            }
            Tuple<Process?, ushort> tuple = new(null, time);
            _info.Add(tuple);
        }

        override public string ToString()
        {
            StringBuilder sb = new();
            sb.Append($"| {"ID/T",-3}| ");
            for (uint i = 0; i < _info.Count; ++i)
            {
                sb.Append($"{i,-3} | ");
            }
            sb.Append("\n");
            var uniqueProcesses = _info
                .Where(t => t.Item1 != null)
                .DistinctBy(t => t.Item1!.Pid)
                .Select(t => t.Item1!)
                .ToList();
            if (uniqueProcesses != null)
            {
                foreach (Process p in uniqueProcesses)
                {
                    sb.Append($"| {p.Pid,-3} | ");
                    foreach (Tuple<Process?, ushort> tuple in _info)
                    {
                        if (tuple.Item1 == p)
                        {
                            sb.Append($"{"E",-3} | ");
                        }
                        else
                        {
                            sb.Append($"{"", -3} | ");
                        }
                    }
                    sb.Append("\n");
                }
            }
            
            return sb.ToString();
        }

        public string ToString2()
        {
            StringBuilder sb = new();
            foreach (Tuple<Process?, ushort> tuple in _info)
            {
                if (tuple.Item1 == null)
                {
                    sb.Append($"CPU idled for {tuple.Item2} time units\n");
                }
                else
                {
                    sb.Append($"Process {tuple.Item1.Pid} ran for {tuple.Item2} time units\n");
                }
            }
            return sb.ToString();
        }
    }
}
