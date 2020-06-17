using System.Collections.Generic;
using UnityEngine;

public abstract class TransportTruck : MonoBehaviour
{
    [SerializeField] protected float _speed = 1;
    [SerializeField] protected Location _currentLocation;
    [SerializeField] protected bool _transportScheduled;

    [SerializeField] protected GameObject _designatedTransportStation;
    [SerializeField] protected GameObject _designatedStorehouse;

    public Queue<GameObject> pastaParticleCargo = new Queue<GameObject>();

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
        if (Vector2.Distance(this.transform.position, _designatedTransportStation.transform.position) > 0.01f)
        {
            float step = _speed * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(this.transform.position, _designatedTransportStation.transform.position, step);
        }
        else if (_designatedTransportStation.CompareTag("TransportStation"))
        {
            _currentLocation = Location.atTruckBase;

            while (pastaParticleCargo.Count > 0)    //TODO narazie rozpieralamy odebrane przez cieżarówkę rzeczy 
            {
                Destroy(pastaParticleCargo.Dequeue());
            }

            _designatedTransportStation.GetComponent<TransportStation>().TruckReturned();
            Destroy(this.gameObject);
        }
    }
}

