

using UnityEngine;
public class Bezie : MonoBehaviour
{
    static private Vector3 _point1;
    static private Vector3 _point2;

    public static Vector3 GetPoints(Vector3 p0, Vector3 p3, float t)
    {
        _point1 = p0 + Vector3.up;
        _point2 = p3 + Vector3.up;

        Vector3 p01 = Vector3.Lerp(p0, _point1, t);
        Vector3 p12 = Vector3.Lerp(_point1, _point2, t);
        Vector3 p23 = Vector3.Lerp(_point2, p3, t);

        Vector3 p012 = Vector3.Lerp(p01, p12, t);
        Vector3 p123 = Vector3.Lerp(p12, p23, t);

        Vector3 p0123 = Vector3.Lerp(p012, p123, t);
        return p0123;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (p1 - p0) +
            6f * oneMinusT * t * (p2 - p1) +
            3f * t * t * (p3 - p2);
    }
}
