using Domain;
using System;
using System.Collections.Generic;

List<Process> processes = new()
{
    new Process(1, 3, 0),
    new Process(2, 7, 12),
    new Process(3, 2, 5),
    new Process(4, 5, 6),
    new Process(5, 3, 9)
};
ISchedulerSimulator scheduler = new RRScheduler(3);

scheduler.ScheduleAll(processes);

Console.WriteLine(scheduler.InfoTracker.ToString());
