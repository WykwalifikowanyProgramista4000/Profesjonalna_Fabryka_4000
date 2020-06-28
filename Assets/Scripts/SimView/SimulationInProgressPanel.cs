using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SimulationInProgressPanel : MonoBehaviour
{
    private int _refreshLoadingDotsCnt;
    private int _loadingDotsCnt;

    private string headerText = "Simulation In Progress";
    private string runOfRunsText = "Run {0} of {1}";

    public Simulation simulation;

    [SerializeField] private Text headerTextObject;
    [SerializeField] private Text runOfRunsTextObject;

    void Start()
    {
        _refreshLoadingDotsCnt = 0;
        headerTextObject.text = headerText;
    }

    void Update()
    {
        if (simulation.SimulationScenarioInProgress)
        {
            UpdateHeader();
            UpdateRunOfRuns();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void UpdateRunOfRuns()
    {
        runOfRunsTextObject.text = String.Format(runOfRunsText, simulation.RunCounter, simulation.NumberOfRuns);
    }

    private void UpdateHeader()
    {
        if (_refreshLoadingDotsCnt == 30)
        {
            if (_loadingDotsCnt >= 3)
            {
                _loadingDotsCnt = 0;
            }
            _loadingDotsCnt++;
            _refreshLoadingDotsCnt = 0;
        }

        headerTextObject.text = headerText + String.Concat(Enumerable.Repeat(".", _loadingDotsCnt));

        _refreshLoadingDotsCnt++;
    }
}
