using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStorage : MonoBehaviour
{

    [SerializeField] private int maxStorageCapacity = 100;
    [SerializeField] private int throughput = 5;
    [SerializeField] private bool releaseParticles = false;
    private Queue<GameObject> storageQueue = new Queue<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        if (releaseParticles) ReleaseGivenAmountOfParticles(throughput);

    }

    public void AddParticleToStorage(GameObject particle)
    {
        particle.GetComponent<PastaParticleControler>().movementToggle = false;

        if(storageQueue.Count < maxStorageCapacity)
        {
            storageQueue.Enqueue(particle);
        }
        else
        {
            particle.GetComponent<PastaParticleControler>().DamageParticle();
            storageQueue.Enqueue(particle);
        }
    }

    public void ReleaseGivenAmountOfParticles(int amount = 1)
    {
        releaseParticles = false;
        if(amount <= storageQueue.Count)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject releasedParticle = storageQueue.Dequeue();
                releasedParticle.GetComponent<PastaParticleControler>().movementToggle = true;
            }
        }
        else
        {
            Debug.LogWarning("Panie, nie mam tyle, moge dać maksymalnie: " + storageQueue.Count.ToString());
        }
    }
}
