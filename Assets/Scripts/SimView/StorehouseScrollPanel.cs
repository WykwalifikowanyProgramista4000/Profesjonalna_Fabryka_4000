using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorehouseScrollPanel : MonoBehaviour
{
    public GameObject simulation;

    [SerializeField] private GameObject _templatePanel;
    public List<StorehousePanel> storehousePanels;

    void Start()
    {
        foreach (var storehouse in simulation.GetComponent<Simulation>().storehouses)
        {
            GameObject storehousePanelsObject = Instantiate(_templatePanel, this.transform);
            storehousePanelsObject.GetComponent<StorehousePanel>().Storehouse = storehouse;
            storehousePanels.Add(storehousePanelsObject.GetComponent<StorehousePanel>());
        }
    }

    public void Restart()
    {
        Start();
    }
}
