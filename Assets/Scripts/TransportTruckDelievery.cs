using System.Collections.Generic;
using UnityEngine;


public class TransportTruckDelievery : TransportTruck
{
    private Queue<GameObject> _routeForPastaParticleToMagazine = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _currentLocation = Location.atTruckBase;
        this.transform.position = _designatedTransportStation.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_transportScheduled)
        {
            if (_currentLocation == Location.atTruckBase)
            {
                DriveToMagazine();
            }
            else if (pastaParticleCargo.Count > 0 & _currentLocation == Location.atStorehouse)
            {
                UnloadCargo();
            }
            else if (pastaParticleCargo.Count == 0 & _currentLocation == Location.atStorehouse)
            {
                DriveToBase();
            }
        }
    }

    public void UnloadCargo()
    {
        _routeForPastaParticleToMagazine.Enqueue(_designatedStorehouse);

        while (pastaParticleCargo.Count > 0)
        {
            GameObject pastaParticle = pastaParticleCargo.Dequeue();
            pastaParticle.transform.position = this.transform.position;
            pastaParticle.GetComponent<PastaParticle>().SetRoute(_routeForPastaParticleToMagazine);
            pastaParticle.GetComponent<PastaParticle>().movementToggle = true;
        }

        pastaParticleCargo.Clear();
        _routeForPastaParticleToMagazine.Clear();
    }

    public void ScheduleDelievery(GameObject designatedTransportStation, GameObject designatedStorehouse, Queue<GameObject> pastaParticleCargo)
    {
        _designatedTransportStation = designatedTransportStation;
        _designatedStorehouse = designatedStorehouse;

        base.pastaParticleCargo = pastaParticleCargo;

        _transportScheduled = true;
    }
}

