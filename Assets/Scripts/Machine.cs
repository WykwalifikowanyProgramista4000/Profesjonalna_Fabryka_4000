using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using UnityEngine;


public class Machine : MonoBehaviour
{
    #region Properties
    [SerializeField] private float _processingTime;
    [SerializeField] private bool _isWorking = false;
    [SerializeField] private bool _isBroken = false;
    [SerializeField] private float _currentBreakingChance = 0; 

    [Header("Buffor Queue settings")]
    [SerializeField] private Vector2 _bufferQueueStartOffset = new Vector2(0.12f, 0.21f);
    [SerializeField] private Vector2 _bufferQueueNextElementOffset = new Vector2(-0.08f, 0);
    [SerializeField] private Vector2 _bufferQueueNextRowOffset = new Vector2(0, 0.09f);
    [SerializeField] private int _bufferQueueElementsPerRow = 4;

    [Header("Processing Queue settings")]
    [SerializeField] private int _processingQueueElementsPerRow = 8;

    [Header("Targets")]

    private Timer workTimer;
    private Stopwatch stopwatch;
    private Brrr brrr;
    private ProgressBar progress_bar;

    public Queue<GameObject> pastaBufferQueue;
    public PastaParticle pastaProcessing;

    private bool _workFinished;
    private System.Random _random;

    private Color initialMachineColor;

    public float ProcessingTime
    {
        get { return _processingTime / Settings.SimulationSpeed; }
        set { _processingTime = value; }
    }
  
    public float CurrentBreakingChance
    {
        get { return _currentBreakingChance; }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // queues
        pastaBufferQueue = new Queue<GameObject>();
        
        // default flags and parameters
        _isWorking = false;
        _isBroken = false;
        _currentBreakingChance = 0;

        // brr
        brrr = GetComponentInChildren<Brrr>();

        // Timer for progress bar and working queue
        progress_bar = GetComponentInChildren<ProgressBar>();
        workTimer = new Timer(ProcessingTime);
        workTimer.Enabled = true;
        workTimer.Stop();
        stopwatch = new Stopwatch();
        stopwatch.Reset();
        workTimer.Elapsed += OnProcessingTimeTimerElapsed;

        initialMachineColor = GetComponent<SpriteRenderer>().color;

        // rand generator
        _random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (_workFinished)
        {
            _workFinished = false;
            SendToNextMachine();
        }

        if (_isWorking == false && pastaBufferQueue.Count > 0)
        {
            _isWorking = true;
            StartProcessing();
            workTimer.Interval = ProcessingTime;
            workTimer.Start();
            stopwatch.Start();
        }
        brrr.GetComponent<SpriteRenderer>().enabled = _isWorking;
        progress_bar.maximum = ProcessingTime;
        progress_bar.current = stopwatch.ElapsedMilliseconds;

        if (_isBroken) GetComponent<SpriteRenderer>().color = Color.red;
        else GetComponent<SpriteRenderer>().color = initialMachineColor;
    }

    #region Methods
    private void StartProcessing()
    {
        GameObject dequeuedParticle = pastaBufferQueue.Dequeue();

        CalculateBreakingChances(dequeuedParticle);

        if (_isBroken)
        {
            dequeuedParticle.GetComponent<PastaParticle>().DamageParticle();
        }
        else
        {
            pastaProcessing = dequeuedParticle.GetComponent<PastaParticle>();
            pastaProcessing.transform.position = this.transform.position;
        }
    }

    private void SendToNextMachine()
    {
        pastaProcessing.movementToggle = true;
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

    private void CalculateBreakingChances(GameObject dequeuedParticle)
    {
        _currentBreakingChance += dequeuedParticle.GetComponent<PastaParticle>().isDamaged ? 0.005f : 0;
        _currentBreakingChance += pastaBufferQueue.Count / 1000;
        int particleBrokenRanodmizer = _random.Next(51, 100);
        if (particleBrokenRanodmizer < _currentBreakingChance) _isBroken = true;
    }

    public void Restart()
    {
        Start();
        UnityEngine.Debug.Log(this.gameObject.name);
    }

    #endregion
}



