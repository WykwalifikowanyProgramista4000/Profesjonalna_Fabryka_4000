using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastaParticleControler : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private int currentTargetPointer = 1;
    [SerializeField] private bool _movementToggle = true;

    public List<GameObject> AssemblyLineMachinesRoute = new List<GameObject>();

    void Start()
    {
        //this.transform.position = AssemblyLineMachinesRoute[0].transform.position;
        currentTargetPointer = 0;
    }

    void Update()
    {
        if (_movementToggle) GoToMachine();
    }

    public void LetsFuckingGoBaby()
    {
        _movementToggle = true;
    }

    public void LetsFuckingStopBaby()
    {
        _movementToggle = false;
    }

    private void GoToMachine()
    {
        if(Vector2.Distance(this.transform.position, AssemblyLineMachinesRoute[currentTargetPointer].transform.position) > 0.01f)
        {
            float step = speed * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(this.transform.position, AssemblyLineMachinesRoute[currentTargetPointer].transform.position, step);
        }
        else
        {
            //TODO jakieś pracki na maszynie czekanie pierdoły
            //TODO dodać Brrr 

            AssemblyLineMachinesRoute[currentTargetPointer].GetComponent<Machine>().AddToPastaQueue(this.gameObject);
            currentTargetPointer++;
            _movementToggle = false;
        }
    }

}
