using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParticleStorage : MonoBehaviour
{

    [SerializeField] private int maxStorageCapacity = 100;
    [SerializeField] private int throughput = 10;
    [SerializeField] private int fineParticlesCounter = 0;
    [SerializeField] private int damagedParticlesCounter = 0;
    [SerializeField] private bool releaseParticle = false;

    private Queue<GameObject> storageQueue = new Queue<GameObject>();
    private Vector2 storagePlacingOffset = new Vector2();

    void Start()
    {

    }

    void Update()
    {
        if (storageQueue.Count == throughput) ReleaseGivenAmountOfParticles(throughput);
        if (releaseParticle) ReleaseGivenAmountOfParticles();

    }

    public void AddParticleToStorage(GameObject particle)
    {
        particle.GetComponent<PastaParticle>().movementToggle = false;

        if(storageQueue.Count < maxStorageCapacity)
        {
            storageQueue.Enqueue(particle);
            IncrementParticlesCounter(particle);
        }
        else
        {
            particle.GetComponent<PastaParticle>().DamageParticle();
            storageQueue.Enqueue(particle);
            IncrementParticlesCounter(particle);
        }
        ArrangeParticleInStorage(particle);
    }

    public void ReleaseGivenAmountOfParticles(int amount = 1)
    {
        releaseParticle = false;
        if(amount <= storageQueue.Count)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject releasedParticle = storageQueue.Dequeue();
                releasedParticle.GetComponent<PastaParticle>().movementToggle = true;
            }
        }
        else
        {
            Debug.LogWarning("Magazyn: Panie, nie mam tyle ("+ amount.ToString() +"), moge dać maksymalnie: " + storageQueue.Count.ToString());
        }
    }

    private void IncrementParticlesCounter(GameObject particle)
    {
        if (particle.GetComponent<PastaParticle>().isDamaged)
        {
            damagedParticlesCounter++;
        }
        else
        {
            fineParticlesCounter++;
        }
    }

    private void ArrangeParticleInStorage(GameObject particle)
    {
        Vector2 storageRightTopEdgePosition = new Vector2(0.1f, 0.25f) + (Vector2)this.transform.position;
        Vector2 horizontalOffset = new Vector2(0.05f, 0f);
        Vector2 verticalOffset = new Vector2(0f, 0.1f);

        particle.transform.position = storageRightTopEdgePosition - storagePlacingOffset;
        storagePlacingOffset += horizontalOffset;

        if (storageQueue.Count % 5 == 0)
        {
            storagePlacingOffset.x = 0;
            storagePlacingOffset -= verticalOffset;
        }
        if (storageQueue.Count == throughput)
        {
            storagePlacingOffset.y = 0;
        }
    }
}
