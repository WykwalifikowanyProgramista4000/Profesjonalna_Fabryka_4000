using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SettingsDisplay : MonoBehaviour
{
    public float inputField_SimSpeed = 1f;

    [SerializeField] private GameObject simulation;

    // Start is called before the first frame update
    void Start()
    {
        Settings.SimulationSpeed = inputField_SimSpeed;

        Time.timeScale = Settings.SimulationSpeed;
        Time.fixedDeltaTime = Settings.SimulationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Settings.SimulationSpeed = inputField_SimSpeed;

        if (Time.timeScale != Settings.SimulationSpeed)
        {
            Time.timeScale = Settings.SimulationSpeed;
            Time.fixedDeltaTime = Settings.SimulationSpeed;
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

    public void OnClick_Pause()
    {
        inputField_SimSpeed = 0;
    }

    public void OnClick_Start()
    {

    }
}
