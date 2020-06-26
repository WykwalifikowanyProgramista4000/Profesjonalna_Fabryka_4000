using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachinePanel : MonoBehaviour
{
    public Machine Machine;

    public GameObject machineID;
    public GameObject processingTime;

    public GameObject breakeChance;
    private Text _breakeChance;

    void Start()
    {
        _breakeChance = breakeChance.GetComponent<Text>();

        machineID.GetComponent<Text>().text = Machine.name;
        processingTime.GetComponentInChildren<Text>().text = Machine.ProcessingTime.ToString();
    }

    private void Update()
    {
        _breakeChance.text = System.Math.Round(Machine.CurrentBreakingChance, 3).ToString();
    }

    public void OnEndEdit_ProcessingTime(string value)
    {
        Machine.ProcessingTime = float.Parse(value);
    }
}
