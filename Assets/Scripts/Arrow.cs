using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

class Arrow : MonoBehaviour
{
    private Vector2 source;
    private List<Vector2> targetsList;
    private LineRenderer lineRenderer;
    private GameObject arrowObject;

    void Start()
    {
        arrowObject.AddComponent(Type.GetType("SpriteRenderer"));
        arrowObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/szczala");

        foreach (Vector2 target in targetsList)
        {
            DrawArrowFromSourceToTarget(source, target);
        }

    }

    void Update()
    {

    }

    private void DrawArrowFromSourceToTarget(Vector2 source, Vector2 target)
    {
        GameObject arrow = Instantiate(arrowObject, (source + target)/2, Quaternion.identity);

    }

    public Arrow(Vector2 source, List<Transform> targetsList)
    {
        this.source = source;
        this.targetsList = targetsList.Select(t => new Vector2(t.position.x, t.position.y)).ToList();
    }
} 

