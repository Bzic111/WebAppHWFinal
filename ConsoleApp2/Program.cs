using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Diagnostics;

//PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
//Console.WriteLine(PerformanceCounterCategory.CounterExists("Memory", "% Available"));
//Console.WriteLine(cpuCounter.CounterName);
//Console.WriteLine(cpuCounter.CategoryName);

//for (int i = 0; i < 10; i++)
//{
//    Console.WriteLine(cpuCounter.NextValue());
//}

var pcc = PerformanceCounterCategory.GetCategories();
for (int i = 0; i < pcc.Length; i++)
{
    if (pcc[i].CategoryName == "Память")
    {
        Console.WriteLine(pcc[i].CategoryName);
        foreach (var item in pcc[i].GetCounters())
        {
            Console.WriteLine(item.CounterName + "\t" + item.NextValue());
        }
    }
    if (pcc[i].CategoryName == "Процессор")
    {
        Console.WriteLine(pcc[i].CategoryName);
        //var cpuCounter = new PerformanceCounter(pcc[i].CategoryName, "% загруженности процессора", "_Total");
        var names = new PerformanceCounterCategory(pcc[i].CategoryName,".");
        foreach (var item in names.GetInstanceNames())
        {
            var counterNew = new PerformanceCounter(pcc[i].CategoryName, "% загруженности процессора",item);
            double d = counterNew.NextValue();
            Console.WriteLine(counterNew.InstanceName + "\t" + d.ToString("0.0000"));
        }
        //Console.WriteLine(cpuCounter.NextValue());
            
        //foreach (var item in pcc[i].GetCounters())
        //{
        //    Console.WriteLine(item.CounterName + "\t" + item.NextValue());
        //}
    }
}
