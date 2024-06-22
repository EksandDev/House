using UnityEngine;
public class Bezie
{
    private Vector3 _point1;
    private Vector3 _point2;

    public Vector3 GetPoints(Vector3 p0, Vector3 p3, float t, Vector3 vector)
    {
        _point1 = p0 + vector / 2;
        _point2 = p3 + vector / 2;

        Vector3 p01 = Vector3.Lerp(p0, _point1, t);
        Vector3 p12 = Vector3.Lerp(_point1, _point2, t);
        Vector3 p23 = Vector3.Lerp(_point2, p3, t);

        Vector3 p012 = Vector3.Lerp(p01, p12, t);
        Vector3 p123 = Vector3.Lerp(p12, p23, t);

        Vector3 p0123 = Vector3.Lerp(p012, p123, t);
        return p0123;
    }
}
