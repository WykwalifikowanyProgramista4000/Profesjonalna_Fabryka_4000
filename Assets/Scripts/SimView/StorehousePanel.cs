using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class StorehousePanel : MonoBehaviour
{
    public Storehouse Storehouse;

    public GameObject Name;
    public GameObject MaxStorage;
    public GameObject Throughput;

    // Start is called before the first frame update
    void Start()
    {
        Name.GetComponent<Text>().text = Storehouse.name;
        MaxStorage.GetComponentInChildren<Text>().text = Storehouse.MaxStorageCapacity.ToString();
        Throughput.GetComponentInChildren<Text>().text = Storehouse.Throughput.ToString();
    }

    public void OnEndEdit_MaxStorage(string value)
    {
        Storehouse.MaxStorageCapacity = int.Parse(value);
    }

    public void OnEndEdit_Throughput(string value)
    {
        Storehouse.Throughput = int.Parse(value);
    }
}
