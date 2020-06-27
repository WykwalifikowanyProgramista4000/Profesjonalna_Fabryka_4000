using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using UnityEngine;


public class Simulation : MonoBehaviour
{
    private Loger _loger = new Loger();
    private int _logerCounter;
    private float _runDuration;
    private float _currentRunStartTime;
    private int _numberOfScheduledRuns;
    private int _runCounter;

    private bool _simulationScenarioInProgress;
    private bool _simulationRunInProgress;
    public bool logON;

    public TransportStationDelieveryScrollPanel delieverySettings;
    public TransportStationReceptionScrollPanel receptionSettings;
    public MachineScrollPanel machineSettings;
    public StorehouseScrollPanel storehouseSettings;

    public List<Machine> machines;
    public List<Spliter> spliters;
    public List<Storehouse> storehouses;
    public List<TransportStation> delieveryTransportStations;
    public List<TransportStation> receptionTransportStations;

    public float RunDuration
    {
        get { return _runDuration; }
        set { _runDuration = value; }
    }

    public int NumberOfRuns
    {
        get { return _numberOfScheduledRuns; }
        set { _numberOfScheduledRuns = (value < 1) ? 1 : value; }
    }

    public bool SimulationScenarioInProgress
    {
        get { return _simulationScenarioInProgress; }
    }

    void Start()
    {
        _runDuration = 300;
        _numberOfScheduledRuns = 0;
        _logerCounter = 0;
        _simulationScenarioInProgress = false;
        _simulationRunInProgress = false;
        logON = false;
        machines = new List<Machine>();
        spliters = new List<Spliter>();
        storehouses = new List<Storehouse>();
        delieveryTransportStations = new List<TransportStation>();
        receptionTransportStations = new List<TransportStation>();

        machines = GameObject.FindGameObjectsWithTag("Maszyna").Select(x => x.GetComponent<Machine>()).ToList();
        receptionTransportStations = GameObject.FindGameObjectsWithTag("TransportStationReception").Select(x => x.GetComponent<TransportStation>()).ToList();
        delieveryTransportStations = GameObject.FindGameObjectsWithTag("TransportStationDelievery").Select(x => x.GetComponent<TransportStation>()).ToList();
        storehouses = GameObject.FindGameObjectsWithTag("Storehouse").Select(x => x.GetComponent<Storehouse>()).ToList();

        machineSettings.PopulateScrollList();
        delieverySettings.PopulateScrollList();
        receptionSettings.PopulateScrollList();
        storehouseSettings.PopulateScrollList();
    }

    void Update()
    {
        if (_logerCounter % 15 == 0 && logON == true)
        {
            _loger.LogMachinesData(machines);
            _loger.LogStorehouseData(storehouses);
            _logerCounter = 0;
        }
        _logerCounter++;

        if (_simulationScenarioInProgress)
        {
            if (_numberOfScheduledRuns > _runCounter && _simulationRunInProgress == false)
            {
                StartRun();
            }
            else if(_simulationRunInProgress == true && Time.time - _currentRunStartTime >= _runDuration)
            {
                EndRun();
            }
            else if(_simulationRunInProgress == false && _numberOfScheduledRuns == _runCounter)
            {
                EndSimulation();
            }
            else
            {
                UnityEngine.Debug.Log("Run " + _runCounter + " in progress...");
            }
        }

    }

    public void ResetNodes()
    {
        // Clear particles
        GameObject[] pastaParticles = GameObject.FindGameObjectsWithTag("Pasta");
        foreach (var pastaParticle in pastaParticles)
        {
            Destroy(pastaParticle);
        }

        // Machines reset
        foreach (var machine in machines)
        {
            machine.Restart();
        }

        // Spriter reset
        foreach (var spliter in spliters)
        {
            spliter.Restart();
        }

        // Transport Stations reset
        foreach (var transportStation in delieveryTransportStations)
        {
            transportStation.Restart();
        }
        foreach (var transportStation in receptionTransportStations)
        {
            transportStation.Restart();
        }

        // Trucks reset
        GameObject[] trucks = GameObject.FindGameObjectsWithTag("Truck");
        foreach (var truck in trucks)
        {
            Destroy(truck);
        }

        // Delievery Settings Reset
        foreach (var transportStationPanel in delieverySettings.transportStationPanels)
        {
            transportStationPanel.Restart();
        }

        // Reception Settings Reset
        foreach (var transportStationPanel in receptionSettings.transportStationPanels)
        {
            transportStationPanel.Restart();
        }
    }

    public void ResetSimulation()
    {
        ResetNodes();
        logON = false;
    }

    public void StartSimulation()
    {
        _simulationScenarioInProgress = true;
    }

    public void StartRun()
    {
        _currentRunStartTime = Time.time;
        _simulationRunInProgress = true;
        _runCounter++;

        // enable transport stations
        foreach (var transportStationPanel in delieverySettings.transportStationPanels)
        {
            transportStationPanel.SimulationRunning = _simulationRunInProgress;
        }

        // enable reception stations
        foreach (var transportStationPanel in receptionSettings.transportStationPanels)
        {
            transportStationPanel.SimulationRunning = _simulationRunInProgress;
        }

        // enable loging
        _loger.InitiateMachineDataLog(machines, String.Format("run_{0:000}", _runCounter));
        _loger.InitiateStorehouseDataLog(storehouses, String.Format("run_{0:000}", _runCounter));
        logON = true;
    }

    private void EndRun()
    {
        _simulationRunInProgress = false;
        logON = false;
        ResetSimulation();
    }

    private void EndSimulation()
    {
        _simulationScenarioInProgress = false;
        _runCounter = 0;
        logON = false;
    }


}

