using System.Collections;
using UnityEngine;

public class WindowsMonsterAnimation : MonoBehaviour
{
    [SerializeField] private Curtain _curtain;
    private WindowsMonster _windowsMonster;
    private Coroutine _agressiveStateTime;
    private TimeСounting _time = new();
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

    private void AnimationUpdate(float time)
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



    #region SubscribeAndUnsubscribeAnimation
    private void SubscribeToAnimation()
    {
        _agressiveStateTime = StartCoroutine(_time.TimerCounting(10f));
        _time.TimeAnimation += AnimationUpdate;
    }

    private void UnsubscribeFromAnimation()
    {
        StopCoroutine(_agressiveStateTime);
        _time.TimeAnimation -= AnimationUpdate;
    }
    #endregion


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
