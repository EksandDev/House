using UnityEngine;
using Zenject;

public class KeyBoardInput : MonoBehaviour
{
    [SerializeField] private float _mouseSensetivity = 10;
    private PlayerMover _playerMover;
    private PlayerInput _input;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ОТ СЮДА ПОТОМ УБРАТЬ!!! В Setting
    }

    #region Zenject init
    [Inject]
    public void Inizialize(PlayerMover playerMover, PlayerInput playerInput)
    {
        _playerMover = playerMover;
        _input = playerInput;
    }
    #endregion
    private void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        _playerMover.Move(GetInputKeyBoard());
        _playerMover.RotatePlayer(GetInputMouse());
    }
    public Vector3 GetInputKeyBoard()
    {
        return _input.Player.Move.ReadValue<Vector3>().normalized;
    }
    public Vector2 GetInputMouse()
    {
        return _input.Player.MouseMove.ReadValue<Vector2>() * _mouseSensetivity * Time.deltaTime;
    }
    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }
}


