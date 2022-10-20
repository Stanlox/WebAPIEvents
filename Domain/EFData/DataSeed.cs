using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EFData
{
    public class DataSeed
    {
        public static void Initial(ApplicationDbContent content)
        {
            if (!content.Event.Any())
            {
                content.AddRange(
                 new Event
                 {
                     Name = "Perfomance in .NET",
                     Manager = "DotNext",
                     Time = DateTime.Now.AddDays(7),
                 },
                 new Event
                 {
                     Name = "BenchmarkDotnet",
                     Manager = ".NETConf",
                     Time = DateTime.Now.AddDays(1),
                 },
                 new Event
                 {
                     Name = "Europe Stadium Tour",
                     Manager = "Rammstein",
                     Time = DateTime.Now.AddDays(120),
                 }
                );
            }

            content.SaveChanges();
        }
    }
}
