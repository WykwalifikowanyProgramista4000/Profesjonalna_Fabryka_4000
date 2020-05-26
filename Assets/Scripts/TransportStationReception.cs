using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class TransportStationReception : TransportStation
{
    protected override void ScheduleNewTruck(int magazineID, int particleQuantity)
    {
        GameObject newTruck = Instantiate(_templateTruck, this.transform.position, Quaternion.identity);
        newTruck.GetComponent<TransportTruckReception>().ScheduleReception(this.gameObject, _storehouses[magazineID], particleQuantity);
    }
}
