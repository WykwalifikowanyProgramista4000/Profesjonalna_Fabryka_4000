using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class TransportStationDelieveryScrollPanel : MonoBehaviour
{
    public GameObject simulation;

    [SerializeField] private GameObject _templatePanel;
    public List<TransportStationPanel> transportStationPanels;

    public void PopulateScrollList()
    {
        foreach (var delieveryStation in simulation.GetComponent<Simulation>().delieveryTransportStations)
        {
            GameObject delieveryStationPanelsObject = Instantiate(_templatePanel, this.transform);
            delieveryStationPanelsObject.GetComponent<TransportStationPanel>().TransportStation = delieveryStation;
            transportStationPanels.Add(delieveryStationPanelsObject.GetComponent<TransportStationPanel>());
        }
    }
}

