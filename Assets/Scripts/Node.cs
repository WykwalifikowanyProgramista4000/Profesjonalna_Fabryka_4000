using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Node : MonoBehaviour
{
    protected Vector2 coordinates { get; set; }
    [SerializeField] protected List<Transform> targetsList { get; set; }

    private Arrow arrow;

    void Start()
    {
        arrow = new Arrow(coordinates, targetsList);   
    }
}