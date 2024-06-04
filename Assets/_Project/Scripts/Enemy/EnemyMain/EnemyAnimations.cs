using UnityEngine;

public abstract class EnemyAnimations : MonoBehaviour
{
    private Coroutine _agressiveStateTime;
    private TimeСounting _time = new();
    [SerializeField] protected float _timeAnimation = 2f;
    public abstract void AnimationUpdate(float time);

    protected void SubscribeToAnimation()
    {
        _agressiveStateTime = StartCoroutine(_time.TimerCounting(_timeAnimation));
        _time.TimeAnimation += AnimationUpdate;
    }

    protected void UnsubscribeFromAnimation()
    {
        StopCoroutine(_agressiveStateTime);
        _time.TimeAnimation -= AnimationUpdate;
    }


}
