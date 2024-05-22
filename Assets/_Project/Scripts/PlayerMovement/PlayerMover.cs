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
    public bool IsGround { get; private set; }
    private void Start()
    {
        _headCam = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        CheckIsGround();
        Gravity();
    }

    private void CheckIsGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            IsGround = true;
            return;
        }
        IsGround = false;
    }
    private void Gravity()
    {
        if (IsGround)
        {
            _gravity.y = _gravityForce;
            return;
        }
        _gravity.y += _gravityForce * Time.deltaTime;
    }
    public void Move(Vector3 directionRaw)
    {
        Vector3 direction = transform.TransformDirection(directionRaw);
        _characterController.Move(_gravity * Time.deltaTime);
        _characterController.Move(direction * SpeedWalk() * Time.deltaTime);
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


