using UnityEngine;

public class Curtain : MonoBehaviour, IClickable
{
    private Animator _animator;
    public bool Open { get; private set; } = true;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnClick()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Open = false;
            _animator.SetBool("Open", false);
            return;
        }
        Open = true;
        _animator.SetBool("Open", true);
    }

}
