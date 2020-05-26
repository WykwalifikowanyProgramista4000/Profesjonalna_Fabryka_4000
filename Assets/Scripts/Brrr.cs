using UnityEngine;

public class Brrr : MonoBehaviour
{
    public float speedX = 20f;
    public float speedY = 60f;

    public float amountX = 0.01f;
    public float amountY = 0.01f;


    [SerializeField] private Vector3 _brrStartPosition;
    private Vector3 _previousPoint;

    public float traceLifetime = 10;

    // Start is called before the first frame update
    void Start()
    {
        _brrStartPosition = this.transform.position;
        _previousPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // brrr
        MachineGoesBrrr();
        //DrawBrrrTrace(); //odkomentować dla dobrej zabawy :V (i pogmerać w speed i amount ;))
    }

    private void MachineGoesBrrr()
    {
        this.transform.position = new Vector2((Mathf.Sin(Time.time * speedX) * amountX) + _brrStartPosition.x,
                                              (Mathf.Cos(Time.time * speedY) * amountY) + _brrStartPosition.y);
    }

    private void DrawBrrrTrace()
    {
        Debug.DrawLine(_previousPoint, this.transform.position, Color.magenta, traceLifetime);
        _previousPoint = this.transform.position;
    }
}
