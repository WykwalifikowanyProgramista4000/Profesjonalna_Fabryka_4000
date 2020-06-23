using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Loger
{
    private string logAddres_machineQueueFill = @"C:\Users\Public\log_machineQueueFill.txt";
    private string logAddres_machineBreakeChance = @"C:\Users\Public\log_machineBreakeChance.txt";

    private string logAddres_storehouseQueueFill = @"C:\Users\Public\log_storehouseQueueFill.txt";
    private string logAddres_storehouseFineParticles = @"C:\Users\Public\log_storehouseFineParticles.txt";
    private string logAddres_storehouseDamagedParticles = @"C:\Users\Public\log_storehouseDamagedParticles.txt";

    #region Machine
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

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(logAddres_machineBreakeChance))
        {
            log_machine.WriteLine(header);

            log_machine.Close();
        }
    }

    public void LogMachinesData(List<Machine> machines)
    {
        // Buffer Queue
        string line = Time.time.ToString();
        foreach (var machine in machines)
        {
            line += ";\t" + machine.pastaBufferQueue.Count;
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(logAddres_machineQueueFill, true))
        {
            log_machine.WriteLine(line);

            log_machine.Close();
        }

        // Breake Chance
        line = Time.time.ToString();
        foreach (var machine in machines)
        {
            line += ";\t" + Math.Round(machine.CurrentBreakingChance, 3);
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(logAddres_machineBreakeChance, true))
        {
            log_machine.WriteLine(line);

            log_machine.Close();
        }
    }
    #endregion

    #region Storehouse
    public void InitiateStorehouseDataLog(List<Storehouse> storehouses)
    {
        string header = "[time]";
        foreach (var storehouse in storehouses)
        {
            header += ";\t" + storehouse.name;
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(logAddres_storehouseQueueFill))
        {
            log_machine.WriteLine(header);

            log_machine.Close();
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(logAddres_storehouseFineParticles))
        {
            log_machine.WriteLine(header);

            log_machine.Close();
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(logAddres_storehouseDamagedParticles))
        {
            log_machine.WriteLine(header);

            log_machine.Close();
        }
    }

    public void LogStorehouseData(List<Storehouse> storehouses)
    {
        // Queue
        string line = Time.time.ToString();
        foreach (var storehouse in storehouses)
        {
            line += ";\t" + storehouse.storageQueue.Count;
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(logAddres_storehouseQueueFill, true))
        {
            log_machine.WriteLine(line);

            log_machine.Close();
        }

        // fine pasta particles
        line = Time.time.ToString();
        foreach (var storehouse in storehouses)
        {
            line += ";\t" + storehouse.fineParticlesCounter;
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(logAddres_storehouseFineParticles, true))
        {
            log_machine.WriteLine(line);

            log_machine.Close();
        }

        // damaged pasta particles
        line = Time.time.ToString();
        foreach (var storehouse in storehouses)
        {
            line += ";\t" + storehouse.damagedParticlesCounter;
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(logAddres_storehouseDamagedParticles, true))
        {
            log_machine.WriteLine(line);

            log_machine.Close();
        }
    }
    #endregion
}

