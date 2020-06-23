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
    [SerializeField] private List<Transform> targetsList;

    [SerializeField] private GameObject arrowObject;

    void Start()
    {        
        foreach (Transform target in targetsList)
        {
            DrawArrowFromSourceToTarget(this.transform.position, target.position);
        }

    }

    void Update()
    {

    }

    private void DrawArrowFromSourceToTarget(Vector2 source, Vector2 target)
    {
        GameObject arrow = Instantiate(arrowObject, (source + target)/2, Quaternion.identity);

        var angle = Quaternion.FromToRotation(new UnityEngine.Vector3(1,0,0), target - source);

        arrow.transform.rotation = angle;
    }

} 

