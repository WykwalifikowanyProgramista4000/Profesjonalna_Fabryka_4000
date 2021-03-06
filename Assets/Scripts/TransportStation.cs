﻿using System;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;



public abstract class TransportStation : MonoBehaviour
{
    protected bool _sendTruckAuto;
    private Timer _autoSchedulerTimer;

    [SerializeField] protected GameObject _templateTruck;
    [SerializeField] protected bool _truckScheduled = false;
    [SerializeField] protected List<GameObject> _storehouses = new List<GameObject>();


    [Header("Manual scheduling")]
    [SerializeField] protected bool _sendTruckManually;
    public int _manualMagazineID;
    public int _manualParticleQuantity;

    [Header("Auto scheduling")]
    [SerializeField] protected bool _enableAutoScheduler;
    [SerializeField] protected float _sendTruckInterval = 2000;
    public int AutoMagazineID;
    public int AutoParticleQuantity;

    public float SendTruckInterval
    {
        get { return _sendTruckInterval / Settings.SimulationSpeed; }
        set { _sendTruckInterval = value; }
    }

    public bool EnableAutoScheduler
    {
        get { return _enableAutoScheduler; }
        set { _enableAutoScheduler = value; }
    }

    void Start()
    {
        _sendTruckAuto = false;
        _truckScheduled = false;

        _enableAutoScheduler = false;
        _sendTruckManually = false;
        _sendTruckAuto = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_sendTruckManually)
        {
            _sendTruckManually = false;
            ScheduleNewTruck(_manualMagazineID, _manualParticleQuantity);
        }

        if (_sendTruckAuto)
        {
            _sendTruckAuto = false;
            ScheduleNewTruck(AutoMagazineID, AutoParticleQuantity);
        }

        if (_enableAutoScheduler)
        {
            AutoScheduler();
        }
        else
        {
            if (_autoSchedulerTimer != null) _autoSchedulerTimer = null;
        }
    }


    protected abstract void ScheduleNewTruck(int magazineID, int particleQuantity);


    protected void AutoScheduler()
    {
        if (_autoSchedulerTimer == null)
        {
            _autoSchedulerTimer = new Timer(SendTruckInterval);
            _autoSchedulerTimer.Enabled = true;
            _autoSchedulerTimer.Start();

            _autoSchedulerTimer.Elapsed += OnAutoDelieveryTimerIntervalElapsed;
        }
    }


    protected void OnAutoDelieveryTimerIntervalElapsed(object sender, EventArgs e)
    {
        _autoSchedulerTimer.Stop();
        _sendTruckAuto = true;
        _autoSchedulerTimer.Interval = SendTruckInterval;
        _autoSchedulerTimer.Start();
    }

    public void TruckReturned()
    {
        _truckScheduled = false;
    }

    public void Restart()
    {
        Start();
    }
}
