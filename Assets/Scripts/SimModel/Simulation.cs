using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using UnityEngine;


public class Simulation : MonoBehaviour
{
    public TransportStationDelieveryScrollPanel delieverySettings;
    public TransportStationReceptionScrollPanel receptionSettings;

    public List<Machine> machines;
    public List<Spliter> spliters;
    public List<Storehouse> storehouses;
    public List<TransportStation> delieveryTransportStations;
    public List<TransportStation> receptionTransportStations;

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

