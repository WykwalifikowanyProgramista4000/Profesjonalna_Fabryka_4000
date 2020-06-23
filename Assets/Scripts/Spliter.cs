using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using UnityEngine;


public class Spliter : Node
{
    [Header("Splitter Settings")]
    [SerializeField] private int defaultRouteID = 0;

    [Header("Available Routes")]
    [SerializeField] private List<AssemblyLineElementList> m_templateRoutesForPastaParticle = new List<AssemblyLineElementList>();
    private List<Queue<GameObject>> _templateRoutesForPastaParticle = new List<Queue<GameObject>>();

    [Header("Targets")]
    [SerializeField] private List<Transform> targets = new List<Transform>();

    private System.Random random = new System.Random();

    private void Start()
    {
        /* pola klasy Node */
        coordinates = this.transform.position;
        targetsList = targets;

        CreateQueues();
    }

    public void SplitPastaParticleToRoute(GameObject pastaParticle, int routeID = -1)
    {
        if(routeID < 0 || _templateRoutesForPastaParticle.Count < routeID)
        {
            //ta, tymczasowo dla braku podania route id poprostu robimy random żeby ładnie wyglądało
            pastaParticle.GetComponent<PastaParticle>().SetRoute(_templateRoutesForPastaParticle[random.Next(0, _templateRoutesForPastaParticle.Count)]);
        }
        else
        {
            pastaParticle.GetComponent<PastaParticle>().SetRoute(_templateRoutesForPastaParticle[routeID]);
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

