using UnityEngine;
public class BezierTest : MonoBehaviour
{
    public Transform P0;
    // public Transform P1;
    // public Transform P2;
    public Transform P3;
    [Range(0, 1)] public float t;

    // void Update()
    // {
    //     t = Mathf.PingPong(Time.time, 1);
    //     transform.position = Bezie.GetPoints(P0.position, P3.position, t);
    //     // transform.rotation = Quaternion.LookRotation(Bezie.GetFirstDerivative(P0.position, P1.position, P2.position, P3.position, t));
    // }


    // private void OnDrawGizmos()
    // {

    //     int sigmentsNumber = 20;
    //     Vector3 preveousePoint = P0.position;

    //     for (int i = 0; i < sigmentsNumber + 1; i++)
    //     {
    //         float paremeter = (float)i / sigmentsNumber;
    //         Vector3 point = Bezie.GetPoints(P0.position, P3.position, paremeter);
    //         Gizmos.DrawLine(preveousePoint, point);
    //         preveousePoint = point;
    //     }

    // }
}
