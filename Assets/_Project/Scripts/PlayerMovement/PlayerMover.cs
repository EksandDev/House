using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour, IMoveable
{
    [SerializeField] private float _speed;
    private Camera _headCam;
    private CharacterController _characterController;
    private Vector3 _gravity;
    private float _rotateHeadAngle;
    private const float _gravityForce = -9.81f;
    private void Start()
    {
        _headCam = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        Gravity();
    }
    private void Gravity()
    {
        if (_characterController.isGrounded)
        {
            _gravity.y = _gravityForce;
            return;
        }
        _gravity.y += _gravityForce * Time.deltaTime;
    }
    public void Move(Vector3 directionRaw)
    {
        Vector3 direction = transform.TransformDirection(directionRaw);
        _characterController.Move(direction * SpeedWalk() * Time.deltaTime);
        _characterController.Move(_gravity * Time.deltaTime);
    }
    public void RotatePlayer(Vector2 MouseDirection)
    {
        RotateHead(MouseDirection.y);
        RotateBody(MouseDirection.x);
    }
    private void RotateHead(float MouseDirectionY)
    {
        _rotateHeadAngle += MouseDirectionY;
        _rotateHeadAngle = Mathf.Clamp(_rotateHeadAngle, -90, 90);
        _headCam.transform.localRotation = Quaternion.Euler(_rotateHeadAngle, 0, 0);
    }
    private void RotateBody(float MouseDirectionX)
    {
        transform.Rotate(MouseDirectionX * Vector3.up);
    }
    private float SpeedWalk()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return _speed * 2f;
        }
        return _speed;
    }
}


