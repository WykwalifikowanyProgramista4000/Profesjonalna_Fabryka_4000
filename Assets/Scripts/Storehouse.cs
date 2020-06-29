using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] public int fineParticlesCounter = 0;
    [SerializeField] public int damagedParticlesCounter = 0;
    [SerializeField] private bool releaseParticle = false;
    [SerializeField] private bool _endStorehouse;

    [Header("Buffor Queue settings")]
    [SerializeField] private Vector2 _storageQueueStartOffset = new Vector2(0.12f, 0.21f);
    [SerializeField] private Vector2 _storageQueueNextElementOffset = new Vector2(-0.08f, 0);
    [SerializeField] private Vector2 _storageQueueNextRowOffset = new Vector2(0, 0.09f);
    [SerializeField] private int _storageQueueElementsPerRow = 4;

    [Header("Targets")]
    [SerializeField] private List<Transform> targets = new List<Transform>();

    [Header("   ")]
    [SerializeField] private List<AssemblyLineElementList> m_templateRoutesForPastaParticle = new List<AssemblyLineElementList>();
    private List<Queue<GameObject>> _templateRoutesForPastaParticle = new List<Queue<GameObject>>();

    public Queue<GameObject> storageQueue = new Queue<GameObject>();
    private Vector2 storagePlacingOffset = new Vector2();

    public int MaxStorageCapacity
    {
        get { return maxStorageCapacity; }
        set { maxStorageCapacity = value; }
    }

    public int Throughput
    {
        get { return throughput; }
        set { throughput = value; }
    }

    void Start()
    {
        storageQueue.Clear();
        CreateRoutes();
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

        if (storageQueue.Count < maxStorageCapacity)
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
        //ArrangeParticleInStorehouse(particle);
    }

    public void ReleaseParticlesToAssemblyLine(int routeId, int particleQuantity = 1)
    {
        releaseParticle = false;
        if (particleQuantity <= storageQueue.Count)
        {
            for (int i = 0; i < particleQuantity; i++)
            {
                GameObject releasedParticle = storageQueue.Dequeue();

                if(releasedParticle != null)
                {
                    releasedParticle.transform.position = (Vector2)this.transform.position + new Vector2(0.12f + 0.02f*i, 0);
                    releasedParticle.GetComponent<PastaParticle>().SetRoute(_templateRoutesForPastaParticle[routeId]);
                    releasedParticle.GetComponent<PastaParticle>().movementToggle = true;
                }
            }
            //ArrangeQueue(storageQueue,      // pasta storage queue rearrangement
            //        _storageQueueStartOffset,
            //        _storageQueueNextElementOffset,
            //        _storageQueueNextRowOffset,
            //        _storageQueueElementsPerRow);
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
            //ArrangeQueue(storageQueue,      // pasta storage queue rearrangement
            //        _storageQueueStartOffset,
            //        _storageQueueNextElementOffset,
            //        _storageQueueNextRowOffset,
            //        4);
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

    private void ArrangeQueue(Queue<GameObject> enumerable, Vector2 startOffset, Vector2 nextElementOffset, Vector2 nextRowOffset, int elementsPerRow)
    {
        Vector2 baseOffset = startOffset + new Vector2(this.transform.position.x,
                                                       this.transform.position.y);
        int rowCount = 0;
        int elementCount = 0;
        foreach (GameObject pastaParticle in enumerable)
        {
            pastaParticle.transform.position = baseOffset +
                                               nextRowOffset * rowCount +
                                               nextElementOffset * elementCount;
            elementCount++;
            if (elementCount == elementsPerRow)
            {
                elementCount = 0;
                rowCount++;
            }
        }
    }

    private void CreateRoutes()
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

    public void Restart()
    {
        storageQueue.Clear();
    }
}
