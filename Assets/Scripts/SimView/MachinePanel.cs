using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachinePanel : MonoBehaviour
{
    public Machine Machine;

    public GameObject machineID;
    public GameObject processingTime;
    public GameObject throughput;

    public GameObject breakeChance;
    private Text _breakeChance;

    void Start()
    {
        _breakeChance = breakeChance.GetComponent<Text>();

        machineID.GetComponent<Text>().text = Machine.name;
        processingTime.GetComponentInChildren<Text>().text = Machine.ProcessingTime.ToString();
        throughput.GetComponentInChildren<Text>().text = Machine.Throughput.ToString();
    }

    private void Update()
    {
        _breakeChance.text = System.Math.Round(Machine.CurrentBreakingChance).ToString();
    }

    public void OnEndEdit_ProcessingTime(string value)
    {
        Machine.ProcessingTime = float.Parse(value);
    }

    public void OnEndEdit_Throughput(string value)
    {
        Machine.Throughput = int.Parse(value);
    }
}
