using UnityEngine;

public class Curtain : MonoBehaviour
{
    private Animator _animator;
    public bool Open { get; private set; } = true;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void OpenCurtain()
    {
        Open = true;
        _animator.SetBool("Open", true);
    }
    public void CloseCurtain()
    {
        Open = false;
        _animator.SetBool("Open", false);
    }

}
