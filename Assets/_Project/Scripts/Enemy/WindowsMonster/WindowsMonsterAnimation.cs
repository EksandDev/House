using System.Collections;
using UnityEngine;

public class WindowsMonsterAnimation : EnemyAnimations
{
    [SerializeField] private Curtain _curtain;
    private WindowsMonster _windowsMonster;
    private Animator _animator;
    private float currentTimeAniamation;
    private void Start()
    {
        _windowsMonster = GetComponent<WindowsMonster>();
        _animator = GetComponent<Animator>();
        _windowsMonster.EnemyIsActivated += SubscribeToAnimation;
    }

    private IEnumerator ReverseAnimationPlay()
    {
        ResetTimeAnimation();
        _windowsMonster.SubscribeCheckParametr();
        float tims = _animator.GetFloat("TimeAnimation");
        while (tims > 0)
        {
            tims -= Time.deltaTime / 2;
            _animator.SetFloat("TimeAnimation", tims);
            yield return null;
        }
        yield break;
    }

    public override void AnimationUpdate(float time)
    {
        float TimeAnimation = currentTimeAniamation + time;
        if (!_curtain.Open)
        {
            StartCoroutine(ReverseAnimationPlay());
            EnemyDeactivated();
            return;
        }
        if (time > 0.99)
        {
            Debug.Log("Умер от оконного монстра!");
            EnemyDeactivated();
        }
        _animator.SetFloat("TimeAnimation", TimeAnimation);
    }

    public void SetTimeAnimation(float time)
    {
        currentTimeAniamation = time;
        SubscribeToAnimation();
    }

    public void ResetTimeAnimation()
    {
        currentTimeAniamation = 0;
    }
    private void EnemyDeactivated()
    {
        UnsubscribeFromAnimation();
    }
}
