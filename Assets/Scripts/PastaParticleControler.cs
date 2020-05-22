using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastaParticleControler : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private int currentTargetPointer = 1;
    [SerializeField] private int flawThreshold = 10; //ilość uszkodzonych w procentach
    [SerializeField] private bool isBroken = false;

    public bool movementToggle = true;
    public List<GameObject> AssemblyLineMachinesRoute = new List<GameObject>();

    void Start()
    {
        currentTargetPointer = 0;
        System.Random random = new System.Random();
        int particleBrokenRanodmizer = random.Next(0, 100);
        if (particleBrokenRanodmizer < flawThreshold) isBroken = true;
    }

    void Update()
    {
        if (movementToggle) GoToMachine();
        if (isBroken) GetComponent<SpriteRenderer>().color = Color.red; //wadliwe particle przedstawione na czerwono

    }

    private void GoToMachine()
    {
        if (Vector2.Distance(this.transform.position, AssemblyLineMachinesRoute[currentTargetPointer].transform.position) > 0.01f)
        {
            float step = speed * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(this.transform.position, AssemblyLineMachinesRoute[currentTargetPointer].transform.position, step);
        }
        else if(AssemblyLineMachinesRoute[currentTargetPointer].CompareTag("Destroyer"))
        {
            AssemblyLineMachinesRoute[currentTargetPointer].GetComponent<Destroyer>().DestroyPastaParticle(this.gameObject);
        }
        else if(AssemblyLineMachinesRoute[currentTargetPointer].CompareTag("Maszyna"))
        {
            //TODO dodać Brrr 

            AssemblyLineMachinesRoute[currentTargetPointer].GetComponent<Machine>().AddToPastaQueue(this.gameObject);
            currentTargetPointer++;
            movementToggle = false;
        }
    }

}
