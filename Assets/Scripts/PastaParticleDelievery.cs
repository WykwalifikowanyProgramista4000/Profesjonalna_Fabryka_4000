using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;



public class PastaParticleDelievery : MonoBehaviour
{
    [SerializeField] public GameObject _templatePastaParticle;
    [SerializeField] private float _delieveryInterval = 2000;
    

    private Timer _delieveryTimer;
    private bool _newPastaParticleAvailable = true;

    public List<GameObject> AssemblyLineMachinesRoute = new List<GameObject>();

    int cnt = 1;

    // Start is called before the first frame update
    void Start()
    {
        _delieveryTimer = new Timer(_delieveryInterval);
        _delieveryTimer.Enabled = true;
        _delieveryTimer.Start();

        _delieveryTimer.Elapsed += NewPastaParticleAvailable;
    }

    // Update is called once per frame
    void Update()
    {
        if (_newPastaParticleAvailable)
        {
            DelieverNewPastaParticle();
        }
    }

    private void DelieverNewPastaParticle()
    {
        Instantiate(_templatePastaParticle, transform.position, Quaternion.identity)
                    .GetComponent<PastaParticleControler>().AssemblyLineMachinesRoute = AssemblyLineMachinesRoute;

        _newPastaParticleAvailable = false;
        _delieveryTimer.Start();
    }

    private void NewPastaParticleAvailable(object sender, EventArgs e)
    {
        _delieveryTimer.Stop();

        _newPastaParticleAvailable = true;
    }

}
