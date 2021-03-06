﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using UnityEngine;


public class Machine : MonoBehaviour
{
    #region Properties
    [SerializeField] private float _processingTime;
    [SerializeField] private int _throughput;
    [SerializeField] private bool _isWorking = false;
    [SerializeField] private bool _isBroken = false;
    [SerializeField] private float _currentBreakingChance = 0; 

    [Header("Buffor Queue settings")]
    [SerializeField] private Vector2 _bufferQueueStartOffset = new Vector2(0.12f, 0.21f);
    [SerializeField] private Vector2 _bufferQueueNextElementOffset = new Vector2(-0.08f, 0);
    [SerializeField] private Vector2 _bufferQueueNextRowOffset = new Vector2(0, 0.09f);
    [SerializeField] private int _bufferQueueElementsPerRow = 4;

    [Header("Processing Queue settings")]
    [SerializeField] private Vector2 processingQueueStartOffset = new Vector2();
    [SerializeField] private Vector2 processingQueueNextElementOffset = new Vector2();
    [SerializeField] private Vector2 processingQueueNextRowOffset = new Vector2();
    [SerializeField] private int _processingQueueElementsPerRow = 8;

    [Header("Targets")]
    [SerializeField] private List<Transform> targets = new List<Transform>();

    private Timer workTimer;
    private Stopwatch stopwatch;
    private ProgressBar progress_bar;

    public Queue<GameObject> pastaBufferQueue = new Queue<GameObject>();
    public Queue<GameObject> pastaProcessingQueue = new Queue<GameObject>();

    private bool _workFinished;
    private System.Random _random;
    private float _breakingTime;
    private float _restorationTime;

    private Color initialMachineColor = Color.white;

    public bool IsBroken
    {
        get { return _isBroken; }
    }

    public float ProcessingTime
    {
        get { return _processingTime / Settings.SimulationSpeed; }
        set { _processingTime = value; }
    }

    public int Throughput
    {
        get { return _throughput; }
        set { _throughput = value; }
    }

    public float CurrentBreakingChance
    {
        get { return _currentBreakingChance; }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Perfectly fine workaround. Nothing to look at. Just go about your buissness //
        _throughput = 1;

        // queues
        pastaBufferQueue.Clear();
        pastaProcessingQueue.Clear();

        // default flags and parameters
        _isWorking = false;
        _isBroken = false;
        _currentBreakingChance = 0;

        // Timer for progress bar and working queue
        progress_bar = GetComponentInChildren<ProgressBar>();
        workTimer = new Timer(ProcessingTime);
        workTimer.Enabled = true;
        workTimer.Stop();
        stopwatch = new Stopwatch();
        stopwatch.Reset();
        workTimer.Elapsed += OnProcessingTimeTimerElapsed;

        // rand generator
        _random = new System.Random();

        // breakage system
        GetComponent<SpriteRenderer>().color = initialMachineColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (_workFinished & pastaProcessingQueue.Count > 0)
        {
            _workFinished = false;
            SendToNextMachine();
        }

        if (_isWorking == false && pastaBufferQueue.Count > _throughput - 1)
        {
            _isWorking = true;
            StartProcessing();
            workTimer.Interval = ProcessingTime;
            workTimer.Start();
            stopwatch.Start();
        }
        //brrr.GetComponent<SpriteRenderer>().enabled = _isWorking;     // performance issues
        progress_bar.maximum = ProcessingTime;
        progress_bar.current = stopwatch.ElapsedMilliseconds;

        if (_isBroken)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            CalculateFixingChances();
        }
        else GetComponent<SpriteRenderer>().color = initialMachineColor;
    }

    #region Methods
    private void StartProcessing()
    {
        for (int i = 0; i < _throughput; i++)
        {
            GameObject dequeuedParticle = pastaBufferQueue.Dequeue();

            if (_isBroken)
            {
                dequeuedParticle.GetComponent<PastaParticle>().DamageParticle();
            }
            else
            {
                CalculateBreakingChances(dequeuedParticle);
                AddToPastaProcessingQueue(dequeuedParticle);
            }

        }
    }

    private void SendToNextMachine()
    {
        GameObject pastaParticle;
        while (pastaProcessingQueue.Count > 0)
        {
            pastaParticle = pastaProcessingQueue.Dequeue();
            pastaParticle.GetComponent<PastaParticle>().movementToggle = true;
        }
    }

    private void OnProcessingTimeTimerElapsed(object source, ElapsedEventArgs e)
    {
        workTimer.Stop();
        stopwatch.Reset();
        _workFinished = true;
        _isWorking = false;
    }

    private void ArrangeQueue(Queue<GameObject> enumerable, Vector2 startOffset, Vector2 nextElementOffset, Vector2 nextRowOffset, int elementsPerRow)
    {
        Vector2 baseOffset = startOffset + new Vector2(this.transform.position.x,
                                                       this.transform.position.y);
        int rowCount = 0;
        int elementCount = 0;
        foreach (GameObject pastaParticle in enumerable)
        {
            pastaParticle.transform.position = baseOffset +
                                               nextRowOffset * rowCount +
                                               nextElementOffset * elementCount;
            elementCount++;
            if(elementCount == elementsPerRow)
            {
                elementCount = 0;
                rowCount++;
            }
        }
    }

    public void AddToPastaBufferQueue(GameObject pastaParticle)
    {
        pastaBufferQueue.Enqueue(pastaParticle);
        ArrangeQueue(pastaBufferQueue,      // pasta buffer queue rearrangement
                _bufferQueueStartOffset,
                _bufferQueueNextElementOffset,
                _bufferQueueNextRowOffset,
                _bufferQueueElementsPerRow);
    }

    public void AddToPastaProcessingQueue(GameObject pastaParticle)
    {
        pastaProcessingQueue.Enqueue(pastaParticle);

        ArrangeQueue(pastaBufferQueue,      // pasta buffer queue rearrangement
                _bufferQueueStartOffset,
                _bufferQueueNextElementOffset,
                _bufferQueueNextRowOffset,
                _bufferQueueElementsPerRow);

        ArrangeQueue(pastaProcessingQueue,  // pasta processing queue rearrangement
                processingQueueStartOffset,
                processingQueueNextElementOffset,
                processingQueueNextRowOffset,
                _processingQueueElementsPerRow);

    }

    private double GetRandomGaussianNumebr(float mean, float std)
    {
        double u1 = 1.0 - _random.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0 - _random.NextDouble();
        double randStdNormal = System.Math.Sqrt(-2.0 * System.Math.Log(u1)) *
                     System.Math.Sin(2.0 * System.Math.PI * u2); //random normal(0,1)

        return mean + std * randStdNormal; //random normal(mean,stdDev^2)
    }
    private void CalculateBreakingChances(GameObject dequeuedParticle)
    {
        _currentBreakingChance += dequeuedParticle.GetComponent<PastaParticle>().isDamaged ? 0.01f : 0;
        _currentBreakingChance += pastaBufferQueue.Count / 40;
        //int particleBrokenRanodmizer = _random.Next(0, 100);

        double particleBrokenRanodmizer = GetRandomGaussianNumebr(0.5f, 0.11f);

        if (100 * particleBrokenRanodmizer < _currentBreakingChance)
        {
            _isBroken = true;
            _breakingTime = Time.time;
            _restorationTime = (float)GetRandomGaussianNumebr(0.5f, 0.11f)*10;
        }
        //UnityEngine.Debug.Log(100 * particleBrokenRanodmizer);
    }

    private void CalculateFixingChances()
    {
        float currentTime = Time.time;

        if (currentTime - _breakingTime > _restorationTime)
        {
            _currentBreakingChance = 0;
            _isBroken = false;
        }

    }

    public void Restart()
    {
        Start();
        //UnityEngine.Debug.Log(this.gameObject.name);
    }

    #endregion
}



