using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class TransportTruckDelieverer : TransportTruck
{
    private Queue<GameObject> _routeForPastaParticleToMagazine;

    // Start is called before the first frame update
    void Start()
    {
        _currentLocation = Location.atTruckBase;
        this.transform.position = _designatedTruckBase.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_delieveryScheduled)
        {
            if (_currentLocation == Location.atTruckBase)
            {
                DriveToMagazine();
            }
            else if (_pastaParticleCargo.Count > 0 & _currentLocation == Location.atStorehouse)
            {
                UnloadCargo();
            }
            else if (_pastaParticleCargo.Count == 0 & _currentLocation == Location.atStorehouse)
            {
                DriveToBase();
            }
        }
    }

    public void UnloadCargo()
    {
        _routeForPastaParticleToMagazine.Enqueue(_designatedStorehouse);

        foreach (GameObject pastaParticle in _pastaParticleCargo)
        {
            pastaParticle.GetComponent<PastaParticle>().SetRoute(_routeForPastaParticleToMagazine);
            pastaParticle.GetComponent<PastaParticle>().movementToggle = true;
        }

        _pastaParticleCargo.Clear();
        _routeForPastaParticleToMagazine.Clear();
    }
}

