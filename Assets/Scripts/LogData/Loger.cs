using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Loger
{
    private string logAddres_machineQueueFill =     @"C:\Users\Public\log_machineQueueFill_1.txt";
    private string logAddres_machineBreakeChance =  @"C:\Users\Public\log_machineBreakeChance.txt";

    public void InitiateMachineDataLog(List<Machine> machines)
    {
        string header = "[time]";
        foreach (var machine in machines)
        {
            header += ";\t" + machine.name;
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(logAddres_machineQueueFill))
        {
            log_machine.WriteLine(header);

            log_machine.Close();
        }
    }

    public void LogMachinesData(List<Machine> machines)
    {
        string queueLine = Time.time.ToString();
        foreach (var machine in machines)
        {
            queueLine += ";\t" + machine.pastaBufferQueue.Count;
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(logAddres_machineQueueFill, true))
        {
            log_machine.WriteLine(queueLine);

            log_machine.Close();
        }

        //using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(logAddres_machineBreakeChance, true))
        //{
        //    log_machine.WriteLine(breakeChanceData);

        //    log_machine.Close();
        //}
    }
}

