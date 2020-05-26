using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// === Workaround aby wyświetlać listy list w edytorze unity === //

[System.Serializable]
public class AssemblyLineElement
{
    public List<GameObject> list;
}

[System.Serializable]
public class AssemblyLineElementList
{
    public List<GameObject> list;
}

// === //

public class Storehouse : MonoBehaviour
{

    [SerializeField] private int maxStorageCapacity = 100;
    [SerializeField] private int throughput = 10;
    [SerializeField] private int fineParticlesCounter = 0;
    [SerializeField] private int damagedParticlesCounter = 0;
    [SerializeField] private bool releaseParticle = false;

    [SerializeField] private bool _endStorehouse;

    [SerializeField] private List<AssemblyLineElementList> m_templateRoutesForPastaParticle = new List<AssemblyLineElementList>();
    private List<Queue<GameObject>> _templateRoutesForPastaParticle = new List<Queue<GameObject>>();

    private Queue<GameObject> storageQueue = new Queue<GameObject>();
    private Vector2 storagePlacingOffset = new Vector2();

    void Start()
    {
        CreateQueues();
    }

    void Update()
    {
        if (!_endStorehouse)
        {
            if (storageQueue.Count > throughput) ReleaseParticlesToAssemblyLine(0, throughput);
            if (releaseParticle) ReleaseParticlesToAssemblyLine(0);
        }
    }

    public void AddParticleToStorehouse(GameObject particle)
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
        ArrangeParticleInStorehouse(particle);
    }

    public void ReleaseParticlesToAssemblyLine(int routeId, int particleQuantity = 1)
    {
        releaseParticle = false;
        if(particleQuantity <= storageQueue.Count)
        {
            for (int i = 0; i < particleQuantity; i++)
            {
                GameObject releasedParticle = storageQueue.Dequeue();

                    releasedParticle.GetComponent<PastaParticle>().SetRoute(_templateRoutesForPastaParticle[routeId]);
                    releasedParticle.GetComponent<PastaParticle>().movementToggle = true;
            }
        }
        else
        {
            Debug.LogWarning("Magazyn: Panie, nie mam tyle ("+ particleQuantity.ToString() +"), moge dać maksymalnie: " + storageQueue.Count.ToString());
        }
    }

    public void ReleaseParticlesToTransportTruck(int particleQuantity, GameObject transportTruck)
    {
        Queue<GameObject> routeToTransportTruck = new Queue<GameObject>();
        routeToTransportTruck.Enqueue(transportTruck);

        if (particleQuantity <= storageQueue.Count)
        {
            for (int i = 0; i < particleQuantity; i++)
            {
                GameObject releasedParticle = storageQueue.Dequeue();

                releasedParticle.transform.position = transportTruck.transform.position;
                transportTruck.GetComponent<TransportTruckReception>().pastaParticleCargo.Enqueue(releasedParticle);

                //releasedParticle.GetComponent<PastaParticle>().SetRoute(routeToTransportTruck);
                //releasedParticle.GetComponent<PastaParticle>().movementToggle = true;
            }
        }
        else
        {
            Debug.LogWarning("Magazyn: Panie, nie mam tyle (" + particleQuantity.ToString() + "), moge dać maksymalnie: " + storageQueue.Count.ToString());
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

    private void ArrangeParticleInStorehouse(GameObject particle)
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

    private void CreateQueues()
    {
        foreach (AssemblyLineElementList list in m_templateRoutesForPastaParticle)
        {
            _templateRoutesForPastaParticle.Add(new Queue<GameObject>());

            foreach (GameObject gObject in list.list)
            {
                _templateRoutesForPastaParticle[_templateRoutesForPastaParticle.Count - 1].Enqueue(gObject);
            }
        }
    }
}
