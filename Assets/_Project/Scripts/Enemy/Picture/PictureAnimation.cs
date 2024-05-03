using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureAnimation : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    private Picture _PictureMonster;
    private Coroutine _agressiveStateTime;
    private TimeСounting _time = new();
    private void Start()
    {
        _PictureMonster = GetComponent<Picture>();
        _PictureMonster.EnemyIsActivated += SubscribeToAnimation;
    }
    private void AnimationUpdate(float timeAnimation)
    {
        if (!_PictureMonster._agressive)
        {
            _animator.SetFloat("TimeAnimation", 0);
            EnemyDeactivated();
            return;
        }

        if (_animator.GetFloat("TimeAnimation") > 0.99)
        {
            Debug.Log("Умер от картины!");
            return;
        }
        _animator.SetFloat("TimeAnimation", timeAnimation);
    }
    private void SubscribeToAnimation()
    {
        _agressiveStateTime = StartCoroutine(_time.TimerCounting(7f));
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
        _PictureMonster.Deactivate();
    }

}
