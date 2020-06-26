using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using UnityEngine;


public class Spliter : MonoBehaviour
{
    [Header("Splitter Settings")]
    [SerializeField] private bool _autoCalculateDistribution;
    [SerializeField] private string _manualDistribution;

    [Header("Available Routes")]
    [SerializeField] private List<AssemblyLineElementList> m_templateRoutesForPastaParticle = new List<AssemblyLineElementList>();
    private List<Queue<GameObject>> _templateRoutesForPastaParticle = new List<Queue<GameObject>>();
    
    private float[] _autoPastaDistribution;
    private int[] _transferedParticles;
    private int _totalTransferedParticles;

    [Header("Targets")]
    [SerializeField] private List<Transform> targets = new List<Transform>();

    private System.Random random = new System.Random();

    private void Start()
    {
        _transferedParticles = new int[m_templateRoutesForPastaParticle.Count];
        _totalTransferedParticles = 0;

        CalculateDistribution();
        CreateQueues();
    }

    private void Update()
    {
        if (_totalTransferedParticles % 100 == 0) // IQ 5 opcja tutaj nie chce mi się stawiać eventu żeby odświeżać dystrybucję na spliterze 
        {
            CalculateDistribution();
        }
    }

    public void SplitPastaParticleToRoute(GameObject pastaParticle)
    {
        for (int i=0; i<_autoPastaDistribution.Length; i++)
        {
            if(_transferedParticles[i] == 0)
            {
                _transferedParticles[i]++; 
                _totalTransferedParticles++;
                pastaParticle.GetComponent<PastaParticle>().SetRoute(_templateRoutesForPastaParticle[i]);
                return;
            }
            else if(_autoPastaDistribution[i] / 100 >= ((float)_transferedParticles[i]) / _totalTransferedParticles)
            {
                _transferedParticles[i]++;
                _totalTransferedParticles++;
                pastaParticle.GetComponent<PastaParticle>().SetRoute(_templateRoutesForPastaParticle[i]);
                return;
            }
        }

        _transferedParticles[0]++;
        _totalTransferedParticles++;
        pastaParticle.GetComponent<PastaParticle>().SetRoute(_templateRoutesForPastaParticle[0]);

        if(_totalTransferedParticles >= 1000)
        {
            _totalTransferedParticles = 0;
            _transferedParticles = new int[_templateRoutesForPastaParticle.Count];
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

    private void CalculateDistribution()
    {
        _autoPastaDistribution = new float[m_templateRoutesForPastaParticle.Count];

        float total = 0;
        for (int i = 0; i < m_templateRoutesForPastaParticle.Count; i++)
        {
            _autoPastaDistribution[i] = 1/m_templateRoutesForPastaParticle[i].list[0].GetComponent<Machine>().ProcessingTime;
            total += _autoPastaDistribution[i];
        }

        for (int i = 0; i < _autoPastaDistribution.Length; i++)
        {
            _autoPastaDistribution[i] *= 100 / total;
        }

        _transferedParticles = new int[m_templateRoutesForPastaParticle.Count];
        _totalTransferedParticles = 0;
    }


    public void Restart()
    {
        Start();
    }
}

