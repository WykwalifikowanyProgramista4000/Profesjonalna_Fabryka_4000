using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public float simSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Config.SimulationSpeed = simSpeed;

        Time.timeScale = Config.SimulationSpeed;
        Time.fixedDeltaTime = Config.SimulationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Config.SimulationSpeed = simSpeed;

        if (Time.timeScale != Config.SimulationSpeed)
        {
            Time.timeScale = Config.SimulationSpeed;
            Time.fixedDeltaTime = Config.SimulationSpeed;
        }
    }

    public void OnEndEdit_SetSimSpeed(string value)
    {
        simSpeed = float.Parse(value);
    }
}
