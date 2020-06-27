using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TransportStationPanel : MonoBehaviour
{
    public TransportStation TransportStation;

    public GameObject TransportStationName;
    public GameObject TransportInterval;
    public GameObject Quantity;
    public GameObject TargetMagazineID;

    public GameObject shcedulingEnabled;
    private bool _enableAutoScheduler;

    public bool SimulationRunning;

    void Start()
    {
        //_enableAutoScheduler = false;
        SimulationRunning = false;

        TransportStationName.GetComponent<Text>().text = TransportStation.name;
        TransportInterval.GetComponentInChildren<Text>().text = TransportStation.SendTruckInterval.ToString();
        Quantity.GetComponentInChildren<Text>().text = TransportStation.AutoParticleQuantity.ToString();
        TargetMagazineID.GetComponentInChildren<Text>().text = TransportStation.AutoMagazineID.ToString();
        //shcedulingEnabled.GetComponent<Toggle>().isOn = false;
    }

    private void Update()
    {
        if (SimulationRunning)
        {
            TransportStation.EnableAutoScheduler = _enableAutoScheduler;
        }
    }

    public void Restart()
    {
        Start();
    }

    public void OnEndEdit_DelieveryInterval(string value)
    {
        TransportStation.SendTruckInterval = float.Parse(value);
    }

    public void OnEndEdit_Quantity(string value)
    {
        TransportStation.AutoParticleQuantity = int.Parse(value);
    }

    public void OnEndEdit_TargetMagazineID(string value)
    {
        TransportStation.AutoMagazineID = int.Parse(value);
    }

    public void OnValueChangeed_EnableScheduling(bool value)
    {
        _enableAutoScheduler = value;
    }
}

