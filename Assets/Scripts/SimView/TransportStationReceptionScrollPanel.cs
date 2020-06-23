using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class TransportStationReceptionScrollPanel : MonoBehaviour
{
    public GameObject simulation;

    [SerializeField] private GameObject _templatePanel;
    public List<TransportStationPanel> transportStationPanels;

    void Start()
    {
        foreach (var receptionStation in simulation.GetComponent<Simulation>().receptionTransportStations)
        {
            GameObject receptionStationPanelsObject = Instantiate(_templatePanel, this.transform);
            receptionStationPanelsObject.GetComponent<TransportStationPanel>().TransportStation = receptionStation;
            transportStationPanels.Add(receptionStationPanelsObject.GetComponent<TransportStationPanel>());
        }
    }

    public void Restart()
    {
        Start();
    }
}

