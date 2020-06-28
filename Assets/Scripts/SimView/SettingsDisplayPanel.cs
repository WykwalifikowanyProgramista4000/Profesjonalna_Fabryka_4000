using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SettingsDisplayPanel : MonoBehaviour
{
    public float inputField_SimSpeed;

    [SerializeField] private Simulation simulation;

    [SerializeField] private GameObject _simulationInProgressPanelTemplate;
    private GameObject _simulationInProgressPanelObject;

    [SerializeField] private InputField SimSpeed_inputField;
    [SerializeField] private InputField NumberOfRuns_inputField;
    [SerializeField] private InputField RunDuration_inputField;
    [SerializeField] private Button Start_button;
    [SerializeField] private Button Reset_button;

    // Start is called before the first frame update
    void Start()
    {
        inputField_SimSpeed = 1f;
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

        if (simulation.SimulationScenarioInProgress)
        {
            SimSpeed_inputField.interactable = false;
            NumberOfRuns_inputField.interactable = false;
            RunDuration_inputField.interactable = false;
            Start_button.interactable = false;
            //Reset_button.interactable = false;
        }
        else
        {
            SimSpeed_inputField.interactable = true;
            NumberOfRuns_inputField.interactable = true;
            RunDuration_inputField.interactable = true;
            Start_button.interactable = true;
            //Reset_button.interactable = true;
        }
    }

    public void OnEndEdit_SimSpeed(string value)
    {
        inputField_SimSpeed = float.Parse(value);
    }

    public void OnEndEdit_NumberOfRuns(string value)
    {
        simulation.NumberOfRuns = int.Parse(value);
    }

    public void OnEndEdit_RunDuration(string value)
    {
        simulation.RunDuration = float.Parse(value);
    }

    public void OnClick_Reset()
    {
        simulation.ResetSimulation();
    }

    public void OnClick_Start()
    {
        simulation.StartSimulation();
        _simulationInProgressPanelObject = Instantiate(_simulationInProgressPanelTemplate,this.transform);
        _simulationInProgressPanelObject.GetComponent<SimulationInProgressPanel>().simulation = simulation;
        //_simulationInProgressPanelObject.GetComponent<RectTransform>().position = new Vector3(0, -120, 0);
    }
}
