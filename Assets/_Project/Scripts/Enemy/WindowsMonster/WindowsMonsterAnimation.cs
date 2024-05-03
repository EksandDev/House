using System.Collections;
using UnityEngine;

public class WindowsMonsterAnimation : MonoBehaviour
{
    [SerializeField] private Curtain _curtain;//нужно потом поменять на некий общий тригер ну накинуть интерфейс и сделать прокидывание
    private WindowsMonster _windowsMonster;
    private Coroutine _agressiveStateTime;
    private TimeСounting _time = new();
    private Animator _animator;
    ///в кода присутствуют магические числа на данный момент, но это только пока
    private void Start()
    {
        _windowsMonster = GetComponent<WindowsMonster>();
        _animator = GetComponent<Animator>();
        _windowsMonster.EnemyIsActivated += SubscribeToAnimation;
    }

    private IEnumerator ReverseAnimationPlay()
    {
        _windowsMonster.SubscribeToDeactivateEnemy();
        float tims = _animator.GetFloat("TimeAnimation");
        while (tims > 0)
        {
            tims -= Time.deltaTime / 2;
            _animator.SetFloat("TimeAnimation", tims);
            yield return null;
        }
        yield break;
    }
    private void AnimationUpdate(float timeAnimation)
    {
        if (!_curtain.Open)
        {
            StartCoroutine(ReverseAnimationPlay());
            EnemyDeactivated();
            return;
        }
        _animator.SetFloat("TimeAnimation", timeAnimation);
    }
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
    private void EnemyDeactivated()
    {
        UnsubscribeFromAnimation();
    }
}
