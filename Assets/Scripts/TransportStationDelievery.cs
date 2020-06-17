using System.Collections.Generic;
using UnityEngine;

class TransportStationDelievery : TransportStation
{
    [SerializeField] private GameObject _templatePastaParticle;

    protected override void ScheduleNewTruck(int magazineID, int particleQuantity)
    {
        if (!_truckScheduled)
        {
            GameObject newTruck = Instantiate(_templateTruck, this.transform.position, Quaternion.identity);

            GameObject pastaParticle;
            Queue<GameObject> particleCargo = new Queue<GameObject>();

            for (int i = 0; i < particleQuantity; i++)
            {
                pastaParticle = Instantiate(_templatePastaParticle, this.transform.position, Quaternion.identity);
                particleCargo.Enqueue(pastaParticle);
            }

            newTruck.GetComponent<TransportTruckDelievery>().ScheduleDelievery(this.gameObject, _storehouses[magazineID], particleCargo);
            _truckScheduled = true;
        }
    }
}

