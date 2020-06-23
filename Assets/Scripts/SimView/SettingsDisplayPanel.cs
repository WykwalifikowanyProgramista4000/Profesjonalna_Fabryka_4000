using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SettingsDisplayPanel : MonoBehaviour
{
    public float inputField_SimSpeed;
    private bool _startFlag;

    [SerializeField] private GameObject simulation;

    // Start is called before the first frame update
    void Start()
    {
        _startFlag = false;
        inputField_SimSpeed = 1f;
        Settings.SimulationSpeed = inputField_SimSpeed;

        Time.timeScale = Settings.SimulationSpeed;
        Time.fixedDeltaTime = Settings.SimulationSpeed;

        // setting to default value with normal time
        inputField_SimSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_startFlag)
        {
            Settings.SimulationSpeed = inputField_SimSpeed;

            if (Time.timeScale != Settings.SimulationSpeed)
            {
                Time.timeScale = Settings.SimulationSpeed;
                Time.fixedDeltaTime = Settings.SimulationSpeed;
            }
        }
    }

    public void OnEndEdit_SimSpeed(string value)
    {
        inputField_SimSpeed = float.Parse(value);
    }

    public void OnClick_Reset()
    {
        simulation.GetComponent<Simulation>().ResetNodes();
        
    }

    public void OnClick_Start()
    {
        _startFlag = true;
    }
}
