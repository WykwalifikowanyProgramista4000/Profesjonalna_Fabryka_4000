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

    public List<GameObject> AssemblyLineMachinesRoute = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _delieveryTimer = new Timer(_delieveryInterval);
        _delieveryTimer.Enabled = true;
        _delieveryTimer.Start();

        _delieveryTimer.Elapsed += DelieverPastaParticle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DelieverPastaParticle(object sender, EventArgs e)
    {
        _delieveryTimer.Stop();

        Instantiate(_templatePastaParticle, transform.position, transform.parent.rotation);

        //temp.GetComponent<PastaParticleControler>().AssemblyLineMachinesRoute = AssemblyLineMachinesRoute;

        _delieveryTimer.Start();
    }

}
