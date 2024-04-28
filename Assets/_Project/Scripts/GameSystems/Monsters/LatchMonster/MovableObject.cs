using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MovableObject : MonoBehaviour, IPhysicMovable
{
    [SerializeField] private Transform _openPoint;
    [SerializeField] private Transform _closePoint;
    [SerializeField] private float _speedModifier;

    private float _mouseAxis;
    private bool _isOpen = false;
    private bool _isCapturing = false;

    public bool IsOpen
    {
        get => _isOpen;
        set
        {
            _isOpen = value;

            if (!IsOpen)
            {
                IsCapturing = false;
                transform.DOMove(_closePoint.position, 1);
            }

            else
                transform.DOMove(_openPoint.position, 1);
        }
    }

    private bool IsCapturing
    {
        get => _isCapturing;
        set
        {
            _isCapturing = value;

            if (_isCapturing)
                StartCoroutine(MoveWhileCapturing());
        }
    }

    public void PhysicalMove()
    {
        if (Input.GetMouseButtonDown(0) && _isOpen)
        {
            IsCapturing = true;
        }
    }

    private IEnumerator MoveWhileCapturing()
    {
        while (_isCapturing)
        {
            _mouseAxis = Input.GetAxis("Mouse X") * Time.deltaTime;

            if (Input.GetMouseButtonDown(1))
                IsCapturing = false;

            if (_mouseAxis < 0 && transform.localPosition.x <= _openPoint.localPosition.x)
                transform.position = _openPoint.position;

            transform.localPosition += new Vector3(_mouseAxis * _speedModifier, 0, 0);

            yield return null;
        }
    }
}