using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using UnityEngine;


public class Simulation : MonoBehaviour
{
    public List<Machine> machines;
    public List<Spliter> spliters;
    public List<Storehouse> storehouses;
    public List<TransportStation> transportStations;

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
        foreach (var transportStation in transportStations)
        {
            transportStation.Restart();
        }
    }
}

