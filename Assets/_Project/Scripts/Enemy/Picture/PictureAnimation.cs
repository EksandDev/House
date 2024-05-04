using UnityEngine;

public class PictureAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Picture _pictureMonster;
    private Coroutine _agressiveStateTime;
    private TimeСounting _time = new();

    private void Start()
    {
        _pictureMonster = GetComponent<Picture>();
        Activate();
    }

    private void AnimationUpdate(float timeAnimation)
    {

        if (!_pictureMonster._agressive)
        {
            _animator.SetFloat("TimeAnimation", 0);
            Deactivated();
            return;
        }

        if (_animator.GetFloat("TimeAnimation") > 0.99)
        {
            Debug.Log("Умер от картины!");
             _pictureMonster.EnemyIsActivated -= SubscribeToAnimation; 
            UnsubscribeFromAnimation();
            return;
        }

        _animator.SetFloat("TimeAnimation", timeAnimation);
    }


    #region SubscribeAndUnsubscribeAnimation
    private void SubscribeToAnimation()
    {
        _agressiveStateTime = StartCoroutine(_time.TimerCounting(15f));
        _time.TimeAnimation += AnimationUpdate;
    }
    private void UnsubscribeFromAnimation()
    {
        StopCoroutine(_agressiveStateTime);
        _time.TimeAnimation -= AnimationUpdate;
    }
    #endregion

    private void Activate()
    {
       _pictureMonster.EnemyIsActivated += SubscribeToAnimation; 
    }

    private void Deactivated()
    {
        UnsubscribeFromAnimation();
        _pictureMonster.Deactivate();
    }

}
