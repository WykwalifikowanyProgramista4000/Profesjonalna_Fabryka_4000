using UnityEngine;


class TransportTruckReception : TransportTruck
{
    private int _numberOfParticlesToBeLoaded;
    //private Queue<GameObject> _routeforPastaParticleToTruck = new Queue<GameObject>();

    void Start()
    {
        _currentLocation = Location.atTruckBase;
        this.transform.position = _designatedTransportStation.transform.position;
    }

    void Update()
    {
        if (_transportScheduled)
        {
            if (_currentLocation == Location.atTruckBase)
            {
                DriveToMagazine();
            }
            else if (pastaParticleCargo.Count == 0 & _currentLocation == Location.atStorehouse)
            {
                LoadCargo();
            }
            else if (pastaParticleCargo.Count > 0 & _currentLocation == Location.atStorehouse)
            {
                DriveToBase();
            }
        }
    }

    private void LoadCargo()
    {
        //_routeforPastaParticleToTruck.Enqueue(this.gameObject);   //TODO dodać jakas animacje ładowania ciężarówy 

        _designatedStorehouse.GetComponent<Storehouse>().ReleaseParticlesToTransportTruck(_numberOfParticlesToBeLoaded, this.gameObject);

        //_pastaParticleCargo.Clear();
        //_routeforPastaParticleToTruck.Clear();
    }

    public void ScheduleReception(GameObject designatedTransportStation, GameObject designatedStorehouse, int particleQuantity)
    {
        _designatedTransportStation = designatedTransportStation;
        _designatedStorehouse = designatedStorehouse;

        _numberOfParticlesToBeLoaded = particleQuantity;

        _transportScheduled = true;
    }
}

