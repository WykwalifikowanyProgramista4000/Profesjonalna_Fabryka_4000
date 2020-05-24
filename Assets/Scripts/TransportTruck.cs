using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class  TransportTruck : MonoBehaviour
{
    [SerializeField] protected float _speed = 1;
    [SerializeField] protected Location _currentLocation;
    [SerializeField] protected bool _delieveryScheduled;

    [SerializeField] protected GameObject _designatedTruckBase;
    [SerializeField] protected GameObject _designatedStorehouse;

    [SerializeField] protected Queue<GameObject> _pastaParticleCargo;

    protected void DriveToMagazine()
    { 
        if (Vector2.Distance(this.transform.position, _designatedStorehouse.transform.position) > 0.01f)
        {
            float step = _speed * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(this.transform.position, _designatedStorehouse.transform.position, step);
        }
        else if (_designatedStorehouse.CompareTag("Storehouse"))
        {
            _currentLocation = Location.atStorehouse;
        }
    }

    protected void DriveToBase()
    {
        if (Vector2.Distance(this.transform.position, _designatedTruckBase.transform.position) > 0.01f)
        {
            float step = _speed * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(this.transform.position, _designatedTruckBase.transform.position, step);
        }
        else if (_designatedTruckBase.CompareTag("TruckBase"))
        {
            _currentLocation = Location.atTruckBase;
        }
    }
}

