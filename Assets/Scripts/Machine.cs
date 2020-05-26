using System.Collections.Generic;
using System.Timers;
using UnityEngine;


public class Machine : MonoBehaviour
{
    #region Properties
    [SerializeField] private float _processingTime = 4000;
    [SerializeField] private int _throughput = 3;
    [SerializeField] private bool _isWorking = false;

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

    private Timer workTimer;
    private Brrr brrr;

    public Queue<GameObject> pastaBufferQueue = new Queue<GameObject>();
    public Queue<GameObject> pastaProcessingQueue = new Queue<GameObject>();

    private bool _workFinished;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        brrr = GetComponentInChildren<Brrr>();

        workTimer = new Timer(_processingTime);
        workTimer.Enabled = true;
        workTimer.Stop();

        workTimer.Elapsed += OnProcessingTimeTimerElapsed;
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
            workTimer.Start();
        }
        brrr.GetComponent<SpriteRenderer>().enabled = _isWorking;
    }

    #region Methods
    private void StartProcessing()
    {
        for (int i = 0; i < _throughput; i++)
        {
            AddToPastaProcessingQueue(pastaBufferQueue.Dequeue());
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

    #endregion
}



