using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public float maximum;
    public long current;
    public Image fill;
    [SerializeField] private Vector3 _progressbarstartposition=new Vector3(0.4f, -0.3f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position = _progressbarstartposition;
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();           
    }

    void GetCurrentFill()
    {
        float fillAmount = (long)current / (float)maximum;
        fill.fillAmount = fillAmount;
        fill.color = Color.red;        
        if (fillAmount > 1.0f/3.0f && fillAmount <=2.0f/3.0f)
        {
            fill.color = Color.yellow;
        }
        if (fillAmount>2.0f/3.0f)
        {
            fill.color = Color.green;
        }       
    }
}
