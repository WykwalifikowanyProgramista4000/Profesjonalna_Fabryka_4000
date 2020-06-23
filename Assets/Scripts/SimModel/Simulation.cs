using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using UnityEngine;


public class Simulation : MonoBehaviour
{
    private Loger _loger = new Loger();
    private int _logerCounter;
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

    void Start()
    {
        _logerCounter = 0;
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



        _loger.InitiateMachineDataLog(machines);
        _loger.InitiateStorehouseDataLog(storehouses);
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


}

