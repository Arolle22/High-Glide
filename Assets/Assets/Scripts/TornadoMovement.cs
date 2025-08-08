using UnityEngine;

public class TornadoMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 target;

    void Start()
    {
        if (pointA != null && pointB != null)
            target = pointB.position;
    }

    void Update()
    {
        if (pointA == null || pointB == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }
}
