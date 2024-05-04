using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidChest : MonoBehaviour
{
    [SerializeField] private Vector3 _endPos;
    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }
    private void ChangePosition(Vector3 targePos)
    {
        transform.position = targePos;
        transform.rotation = Quaternion.identity;
    }
    public void StartPosition()
    {
        ChangePosition(_startPos);
    }
    public void EndPosition()
    {
        ChangePosition(_endPos);
    }
}
