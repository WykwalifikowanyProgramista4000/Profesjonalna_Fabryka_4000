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
        foreach (var machine in machines)
        {
            machine.Restart();
        }
    }
}

