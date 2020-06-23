using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class DataDisplay : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_machines;
    private List<Machine> _machines = new List<Machine>();

    [SerializeField] private Text _brokenParticlesPerc;

    [SerializeField] private Text _machineBrakeChanceIDs;
    [SerializeField] private Text _machineBrakeChanceValues;

    private void Start()
    {
        string textNames = "";
        string textValues = "";
        for (int i= 0; i < m_machines.Count; i++)
        {
            _machines.Add(m_machines[i].GetComponent<Machine>());
            textNames += "Machine " + i + " :\n";
            textValues += Math.Round(_machines[i].CurrentBreakingChance,3) + "\n";
        }
        _machineBrakeChanceIDs.text = textNames;
        _machineBrakeChanceValues.text = textValues;
    }

    private void Update()
    {
        string textValues = "";
        foreach (var machine in _machines)
        {
            textValues += Math.Round(machine.CurrentBreakingChance,3) + "\n";
        }
        _machineBrakeChanceValues.text = textValues;

        if(Data.BrokenParticlesCount != 0)
        {
            _brokenParticlesPerc.text = Convert.ToString(Math.Round(Data.BrokenParticlesCount / Data.ParticlesCount, 3));
        }
        else
        {
            _brokenParticlesPerc.text = "0";
        }
    }
}

