﻿using UnityEngine;

class TransportStationReception : TransportStation
{
    protected override void ScheduleNewTruck(int magazineID, int particleQuantity)
    {
        if (!_truckScheduled)
        {
            GameObject newTruck = Instantiate(_templateTruck, this.transform.position, Quaternion.identity);
            newTruck.GetComponent<TransportTruckReception>().ScheduleReception(this.gameObject, _storehouses[magazineID], particleQuantity);
            _truckScheduled = true;
        }
    }
}
