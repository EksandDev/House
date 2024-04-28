using System;
using UnityEngine;

public class WindowsMonster : MonoBehaviour
{
    [SerializeField] private GameObject _Mesh;
    [SerializeField] private Curtain _curtain;
    private Animator _animator;
    private TimeСounting _time = new();
    private Coroutine _spawnTime;
    private Coroutine _decontaminationTime;
    public Action EnemyIsActivated;

    private void Start()
    {
        SwitchEnemyVisibility(false);
        SubscribeToRespawn();
        _animator = GetComponent<Animator>();
    }
    private void EnemyActivated(bool TimeIsUp)
    {
        if (TimeIsUp)
        {
            SwitchEnemyVisibility(true);
            EnemyIsActivated?.Invoke();
            UnsubscribeFromRespawn();
        }
    }

    private void DecontaminationEnemy(bool TimeIsUp)
    {
        if (!_curtain.Open && TimeIsUp)
        {
            SwitchEnemyVisibility(false);
            UnsubscribeFromDecontaminationEnemy();
            SubscribeToRespawn();   //нужно поработать с кнопкой чтоб счетчик начинался когда шторы будут открыты
            return;
        }
        else if (_curtain.Open && !TimeIsUp)
        {
            UnsubscribeFromDecontaminationEnemy();
            StopCoroutine(_decontaminationTime);
            _animator.Play("attack_shift");
        }
    }

    #region SubscribeAndUnsubscribeDecontamination
    public void SubscribeToDecontaminationEnemy()
    {
        _time.TimeIsUp += DecontaminationEnemy;
        _decontaminationTime = StartCoroutine(_time.TimerCounting(3f));
    }
    private void UnsubscribeFromDecontaminationEnemy()
    {
        _time.TimeIsUp -= DecontaminationEnemy;
    }
    #endregion

    #region SubscribeAndUnsubscribeRespawn
    private void SubscribeToRespawn()
    {
        _time.TimeIsUp += EnemyActivated;
        _spawnTime = StartCoroutine(_time.TimerCounting(5f));
    }
    private void UnsubscribeFromRespawn()
    {
        StopCoroutine(_spawnTime);
        _time.TimeIsUp -= EnemyActivated;
    }
    #endregion
    private void SwitchEnemyVisibility(bool state)
    {
        _Mesh.gameObject.SetActive(state);
    }
}
