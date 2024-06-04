using UnityEngine;

public class PictureAnimation : EnemyAnimations
{
    [SerializeField] private Animator _animator;
    private Picture _pictureMonster;

    private void Start()
    {
        _pictureMonster = GetComponent<Picture>();
        Activate();
    }

    public override void AnimationUpdate(float timeAnimation)
    {

        if (!_pictureMonster.Agressive)
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
