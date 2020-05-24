using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using TMPro;
using UnityEngine;


public class Machine : MonoBehaviour
{
    #region Properties
    [SerializeField] private float _processingTime = 4000;
    [SerializeField] private int _throughput = 3;
    [SerializeField] private bool _isWorking = false;
    [SerializeField] private Vector2 queueBegginingOffset = new Vector2(0, 0.25f);
    [SerializeField] private Vector2 nextQueuedParticleOffset = new Vector2(0, 0.25f);
    [SerializeField] private Vector2 machinequeueBegginingOffset = new Vector2(-0.1f, 0);
    [SerializeField] private Vector2 machinenextQueuedParticleOffset = new Vector2(0.05f, 0);

    private Timer workTimer;
    private Brrr brrr;
    
    public Queue<GameObject> pastaBufferQueue = new Queue<GameObject>();
    public Queue<GameObject> pastaProcessingQueue = new Queue<GameObject>();
    

    #endregion

    #region Get/Set
    public float ProcessingTime
    {
        get
        {
            return _processingTime;
        }
        set
        {
            _processingTime = value * 1000;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        brrr = GetComponentInChildren<Brrr>();

        workTimer = new Timer(_processingTime);
        workTimer.Enabled = true;
        workTimer.Stop();

        workTimer.Elapsed += SendToNextMachine;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isWorking == false && pastaBufferQueue.Count > _throughput - 1)
        {
            DoWork();
        }

        brrr.GetComponent<SpriteRenderer>().enabled = _isWorking;
    }

    #region Methods
    private void DoWork()
    {
        _isWorking = true;
        for(int i=0;i< _throughput;i++)
        {
            AddToPastaInMachineQueue(GetFrompastaBufferQueue());
            UnityEngine.Debug.Log(pastaProcessingQueue);
        }
        workTimer.Start();
    }

    private void SendToNextMachine(object source, ElapsedEventArgs e)
    {
        workTimer.Stop();
        for(int i=0;i<_throughput;i++)
        {
            GetFromPastaInMachineQueue().GetComponent<PastaParticleControler>().movementToggle=true;
            UnityEngine.Debug.Log(pastaProcessingQueue);
        }
        _isWorking = false;
    }

    private GameObject GetFrompastaBufferQueue()
    {
        GameObject temp = pastaBufferQueue.Dequeue();
        ArrangeQueue();
        return temp;
    }

    private void ArrangeQueue()
    {
        Vector2 machinePosition = this.transform.position;
        machinePosition += queueBegginingOffset;

        Vector2 offset = new Vector2();

        foreach(GameObject pastaParticle in pastaBufferQueue)
        {
            pastaParticle.transform.position = machinePosition + offset;
            offset += nextQueuedParticleOffset;
        }

    }

    public void AddTopastaBufferQueue(GameObject pastaParticle)
    {
        pastaBufferQueue.Enqueue(pastaParticle);
        ArrangeQueue();
    }

    private GameObject GetFromPastaInMachineQueue()
    {
        GameObject temp = pastaProcessingQueue.Dequeue();
        return temp;
    }

    private void ArrangeQueueInMachine()
    {
        Vector2 machinePosition = this.transform.position;
        machinePosition += machinequeueBegginingOffset;

        Vector2 offset = new Vector2();

        foreach (GameObject pastaParticle in pastaProcessingQueue)
        {
            pastaParticle.transform.position = machinePosition + offset;
            offset += machinenextQueuedParticleOffset;
        }

    }

    public void AddToPastaInMachineQueue(GameObject pastaParticle)
    {
        pastaProcessingQueue.Enqueue(pastaParticle);
        ArrangeQueueInMachine();
    }

    #endregion
}



