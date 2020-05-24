using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;



public class PastaParticleDelievery : MonoBehaviour
{
    [SerializeField] public GameObject _templatePastaParticle;
    [SerializeField] private float _delieveryInterval = 2000;

    [SerializeField] private List<GameObject> _assemblyLineMachinesRouteTemplate = new List<GameObject>();
    public Queue<GameObject> _assemblyLineMachinesRoute = new Queue<GameObject>();

    private Timer _delieveryTimer;
    private bool _newPastaParticleAvailable = true;


    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject routeElement in _assemblyLineMachinesRouteTemplate)
        {
            _assemblyLineMachinesRoute.Enqueue(routeElement);
        }

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
        Instantiate(_templatePastaParticle, this.transform.position, Quaternion.identity)
                    .GetComponent<PastaParticle>().SetRoute(_assemblyLineMachinesRoute);

        _newPastaParticleAvailable = false;
        _delieveryTimer.Start();
    }

    private void NewPastaParticleAvailable(object sender, EventArgs e)
    {
        _delieveryTimer.Stop();

        _newPastaParticleAvailable = true;
    }

}
