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
    
    public Queue<GameObject> pastaQueue = new Queue<GameObject>();
    public Queue<GameObject> pasta_in_machine_Queue = new Queue<GameObject>();
    public PastaParticleControler currentryWorkedOnPastaParticle;
    //public PastaParticleControler currentryWorkedOnPastaParticle2;

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
        if (_isWorking == false && pastaQueue.Count > _throughput - 1)
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
            currentryWorkedOnPastaParticle = GetFromPastaQueue().GetComponent<PastaParticleControler>();
            AddToPastaInMachineQueue(currentryWorkedOnPastaParticle.gameObject);

            //currentryWorkedOnPastaParticle.transform.position = this.transform.position;
        }

        /*for (int i = 0; i < _throughput; i++)
        {
            currentryWorkedOnPastaParticle = GetFromPastaInMachineQueue().GetComponent<PastaParticleControler>();
            //currentryWorkedOnPastaParticle.transform.position = this.transform.position+new Vector3(-0.1f, 0, 0);
        }

        //currentryWorkedOnPastaParticle2.transform.position = this.transform.position + new Vector3(-0.1f, 0, 0); ;*/
        workTimer.Start();
    }

    private void SendToNextMachine(object source, ElapsedEventArgs e)
    {
        workTimer.Stop();
        //currentryWorkedOnPastaParticle.movementToggle = true;
        //currentryWorkedOnPastaParticle2.movementToggle = true;
        for(int i=0;i<_throughput;i++)
        {
            currentryWorkedOnPastaParticle = GetFromPastaInMachineQueue().GetComponent<PastaParticleControler>();
            UnityEngine.Debug.Log(currentryWorkedOnPastaParticle);
            currentryWorkedOnPastaParticle.movementToggle = true;

        }
        _isWorking = false;
    }

    private GameObject GetFromPastaQueue()
    {
        GameObject temp = pastaQueue.Dequeue();
        ArrangeQueue();
        return temp;
    }

    private void ArrangeQueue()
    {
        Vector2 machinePosition = this.transform.position;
        machinePosition += queueBegginingOffset;

        Vector2 offset = new Vector2();

        foreach(GameObject pastaParticle in pastaQueue)
        {
            pastaParticle.transform.position = machinePosition + offset;
            offset += nextQueuedParticleOffset;
        }

    }

    public void AddToPastaQueue(GameObject pastaParticle)
    {
        pastaQueue.Enqueue(pastaParticle);
        ArrangeQueue();
    }

    private GameObject GetFromPastaInMachineQueue()
    {
        GameObject temp = pasta_in_machine_Queue.Dequeue();
        ArrangeQueueInMachine();
        return temp;
    }

    private void ArrangeQueueInMachine()
    {
        Vector2 machinePosition = this.transform.position;
        machinePosition += machinequeueBegginingOffset;

        Vector2 offset = new Vector2();

        foreach (GameObject pastaParticle in pasta_in_machine_Queue)
        {
            pastaParticle.transform.position = machinePosition + offset;
            offset += machinenextQueuedParticleOffset;
        }

    }

    public void AddToPastaInMachineQueue(GameObject pastaParticle)
    {
        pasta_in_machine_Queue.Enqueue(pastaParticle);
        ArrangeQueueInMachine();
    }

    #endregion
}



