using Domain;
using System;
using System.Collections.Generic;

List<Process> processes = new()
{
    new Process("P1", 3, 0),
    new Process("P2", 7, 2),
    new Process("P3", 2, 5),
    new Process("P4", 5, 6),
    new Process("P5", 3, 9)
};
ISchedulerSimulator scheduler = new RRScheduler(3, processes);

scheduler.ScheduleAll();

Console.WriteLine(scheduler.InfoTracker.ToString());
