using System.Collections.Generic;
using UnityEngine;

public class PastaParticle : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private int currentTargetPointer = 1;
    [SerializeField] private int flawThreshold = 1; //ilość uszkodzonych w promilach

    [SerializeField] private Queue<GameObject> _route = new Queue<GameObject>();
    [SerializeField] private GameObject _currentTarget;

    private System.Random _random;

    public bool isDamaged;
    public bool movementToggle;

    void Start()
    {
        _random = new System.Random();
        isDamaged = false;
        movementToggle = false;

        int particleBrokenRanodmizer = _random.Next(0, 1000);
        if (particleBrokenRanodmizer < flawThreshold) DamageParticle();
        Data.ParticlesCount++;
    }

    void Update()
    {
        if (movementToggle) GoToMachine();
        if (isDamaged) GetComponent<SpriteRenderer>().color = Color.red; //wadliwe particle przedstawione na czerwono

    }

    private void GoToMachine()
    {
        if (Vector2.Distance(this.transform.position, _currentTarget.transform.position) > 0.01f)
        {
            float step = speed * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(this.transform.position, _currentTarget.transform.position, step);
        }
        else if (_currentTarget.CompareTag("Maszyna"))
        {
            _currentTarget.GetComponent<Machine>().AddToPastaBufferQueue(this.gameObject);

            if (_route.Count > 0)
            {
                _currentTarget = _route.Dequeue();
            }

            movementToggle = false;
        }
        else if (_currentTarget.CompareTag("Storehouse"))
        {
            _currentTarget.GetComponent<Storehouse>().AddParticleToStorehouse(this.gameObject);
            movementToggle = false;
        }
        else if (_currentTarget.CompareTag("Spliter"))
        {
            _currentTarget.GetComponent<Spliter>().SplitPastaParticleToRoute(this.gameObject);
        }
    }

    public void SetRoute(Queue<GameObject> newRoute)
    {
        _route = new Queue<GameObject>(newRoute);
        _currentTarget = _route.Dequeue();
    }

    public void DamageParticle()
    {
        if(isDamaged == false)
        {
            isDamaged = true;
            Data.BrokenParticlesCount++;
        }
    }

}
