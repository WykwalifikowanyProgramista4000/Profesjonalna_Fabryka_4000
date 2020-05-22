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
    [SerializeField] private bool _isWorking = false;
    [SerializeField] private Vector2 queueBegginingOffset = new Vector2(0, 0.25f);
    [SerializeField] private Vector2 nextQueuedParticleOffset = new Vector2(0, 0.25f);

    private Timer workTimer;
    private Brrr brrr;
    
    public Queue<GameObject> pastaQueue = new Queue<GameObject>();
    public PastaParticleControler currentryWorkedOnPastaParticle1;
    public PastaParticleControler currentryWorkedOnPastaParticle2;

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
        if (_isWorking == false && pastaQueue.Count > 1)
        {
            DoWork();
        }

        brrr.GetComponent<SpriteRenderer>().enabled = _isWorking;
    }

    #region Methods
    private void DoWork()
    {
        _isWorking = true;
        currentryWorkedOnPastaParticle1 = GetFromPastaQueue().GetComponent<PastaParticleControler>();
        currentryWorkedOnPastaParticle2 = GetFromPastaQueue().GetComponent<PastaParticleControler>();

        currentryWorkedOnPastaParticle1.transform.position = this.transform.position + new Vector3(0.1f,0,0);
        currentryWorkedOnPastaParticle2.transform.position = this.transform.position + new Vector3(-0.1f, 0, 0); ;

        workTimer.Start();
    }

    private void SendToNextMachine(object source, ElapsedEventArgs e)
    {
        workTimer.Stop();
        currentryWorkedOnPastaParticle1.movementToggle = true;
        currentryWorkedOnPastaParticle2.movementToggle = true;
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
    #endregion
}



