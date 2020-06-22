using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class TransportStationPanel : MonoBehaviour
{
    public TransportStation TransportStation;

    public GameObject TransportStationName;
    public GameObject DelieveryInterval;
    public GameObject Quantity;
    public GameObject TargetMagazineID;

    public GameObject shcedulingEnabled;

    void Start()
    {
        TransportStationName.GetComponent<Text>().text = TransportStation.name;
        DelieveryInterval.GetComponentInChildren<Text>().text = TransportStation.SendTruckInterval.ToString();
        Quantity.GetComponentInChildren<Text>().text = TransportStation.AutoParticleQuantity.ToString();
        TargetMagazineID.GetComponentInChildren<Text>().text = TransportStation.AutoMagazineID.ToString();
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
}

