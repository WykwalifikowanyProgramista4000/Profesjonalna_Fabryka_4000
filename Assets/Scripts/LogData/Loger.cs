using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Loger
{
    public Loger()
    {
        // Main Log folder
        if (!Directory.Exists(_logFolderPath))
        {
            Directory.CreateDirectory(_logFolderPath);
        }

        // Machine
        if(!Directory.Exists(_logFolderPath + _logAddres_machine))
        {
            Directory.CreateDirectory(_logFolderPath + _logAddres_machine);
        }
        _logAddres_machine = _logFolderPath + _logAddres_machine;

        // Storehouse
        if (!Directory.Exists(_logFolderPath + _logAddres_storehouse))
        {
            Directory.CreateDirectory(_logFolderPath + _logAddres_storehouse);
        }
        _logAddres_storehouse = _logFolderPath + _logAddres_storehouse;
    }

    private string _logFolderPath = @"C:\Users\Public\Factory4000Logs\";

    private string _logAddres_machine = @"MachineLogs\";
    private string _logName_machineQueueFill;
    private string _logName_machineBreakeChance;
    private string _logName_machineBroken;

    private string _logAddres_storehouse = @"StorehouseLogs\";
    private string _logName_storehouseQueueFill;
    private string _logName_storehouseFineParticles;
    private string _logName_storehouseDamagedParticles;

    private float _logStartTimeMachine;
    private float _logStartTimeStorehouse;

    #region Machine
    public void InitiateMachineDataLog(List<Machine> machines, string logName = "new_machine_log")
    {
        _logStartTimeMachine = Time.time;
        _logName_machineQueueFill = "log_machineQueueFill_" + logName + ".txt";
        _logName_machineBreakeChance = "log_machineBreakeChance_" + logName + ".txt";
        _logName_machineBroken = "log_machineBroken" + logName + ".txt";

        string header = "[time]";
        foreach (var machine in machines)
        {
            header += ";\t" + machine.name;
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(_logAddres_machine + _logName_machineQueueFill))
        {
            log_machine.WriteLine(header);

            log_machine.Close();
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(_logAddres_machine + _logName_machineBreakeChance))
        {
            log_machine.WriteLine(header);

            log_machine.Close();
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(_logAddres_machine + _logName_machineBroken))
        {
            log_machine.WriteLine(header);

            log_machine.Close();
        }
    }

    public void LogMachinesData(List<Machine> machines)
    {
        // Buffer Queue
        string line = (Time.time - _logStartTimeMachine).ToString();
        foreach (var machine in machines)
        {
            line += ";\t" + machine.pastaBufferQueue.Count;
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(_logAddres_machine + _logName_machineQueueFill, true))
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

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(_logAddres_machine + _logName_machineBreakeChance, true))
        {
            log_machine.WriteLine(line);

            log_machine.Close();
        }

        // Broken\NotBroken
        line = Time.time.ToString();
        foreach (var machine in machines)
        {
            line += ";\t" + machine.IsBroken;
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(_logAddres_machine + _logName_machineBroken, true))
        {
            log_machine.WriteLine(line);

            log_machine.Close();
        }
    }
    #endregion

    #region Storehouse
    public void InitiateStorehouseDataLog(List<Storehouse> storehouses, string logName = "new_storehouse_log")
    {
        _logStartTimeStorehouse = Time.time;
        _logName_storehouseQueueFill = "log_storehouseQueueFill" + logName + ".txt";           
        _logName_storehouseFineParticles = "log_storehouseFineParticles" + logName + ".txt";
        _logName_storehouseDamagedParticles = "log_storehouseDamagedParticles" + logName + ".txt";

        string header = "[time]";
        foreach (var storehouse in storehouses)
        {
            header += ";\t" + storehouse.name;
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(_logAddres_storehouse + _logName_storehouseQueueFill))
        {
            log_machine.WriteLine(header);

            log_machine.Close();
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(_logAddres_storehouse + _logName_storehouseFineParticles))
        {
            log_machine.WriteLine(header);

            log_machine.Close();
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(_logAddres_storehouse + _logName_storehouseDamagedParticles))
        {
            log_machine.WriteLine(header);

            log_machine.Close();
        }
    }

    public void LogStorehouseData(List<Storehouse> storehouses)
    {
        // Queue
        string line = (Time.time - _logStartTimeStorehouse).ToString();
        foreach (var storehouse in storehouses)
        {
            line += ";\t" + storehouse.storageQueue.Count;
        }

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(_logAddres_storehouse + _logName_storehouseQueueFill, true))
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

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(_logAddres_storehouse + _logName_storehouseFineParticles, true))
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

        using (System.IO.StreamWriter log_machine = new System.IO.StreamWriter(_logAddres_storehouse + _logName_storehouseDamagedParticles, true))
        {
            log_machine.WriteLine(line);

            log_machine.Close();
        }
    }
    #endregion
}

