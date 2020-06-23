using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MachineScrollPanel : MonoBehaviour
{
    public GameObject simulation;

    [SerializeField] private GameObject _templatePanel;
    public List<MachinePanel> machinePanels;

    // Start is called before the first frame update
    void Start()
    {
        //Transform[] panels = GetComponentsInChildren<Transform>();
        //foreach (var panel in panels)
        //{
        //    Destroy(panel.gameObject);
        //}

        foreach (var machine in simulation.GetComponent<Simulation>().machines)
        {
            GameObject machinePanelObject = Instantiate(_templatePanel, this.transform);
            machinePanelObject.GetComponent<MachinePanel>().Machine = machine;
            machinePanels.Add(machinePanelObject.GetComponent<MachinePanel>());
        }
    }

    public void Restart()
    {
        Start();
    }
}
