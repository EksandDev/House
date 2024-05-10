using UnityEngine;
using Zenject;

public class KeyBoardInput : MonoBehaviour
{
    [SerializeField] private float _mouseSensetivity = 0.1f;
    private PlayerMover _playerMover;
    private PlayerInput _input;
    public bool AbilityMoveBody
    {
        get { return _abilityMoveBody; }
        set { _abilityMoveBody = value; }
    }
    public bool AbilityMoveHead
    {
        get { return _abilityMoveHead; }
        set { _abilityMoveHead = value; }
    }
    private bool _abilityMoveBody = true;
    private bool _abilityMoveHead = true;
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
        if (!_abilityMoveBody)
        {
            return Vector3.zero;
        }
        return _input.Player.Move.ReadValue<Vector3>().normalized;
    }
    public Vector2 GetInputMouse()
    {
        if (!_abilityMoveHead)
        {
            return Vector3.zero;
        }
        return _input.Player.MouseMove.ReadValue<Vector2>() * _mouseSensetivity;
    }
    public void AbilityToMove(bool value)
    {
        _abilityMoveBody = value;
        _abilityMoveHead = value;
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


