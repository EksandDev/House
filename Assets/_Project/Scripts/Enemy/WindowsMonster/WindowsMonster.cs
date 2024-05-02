using System;
using UnityEngine;

public class WindowsMonster : MonsterSpot
{
    protected override float TimeToActivate { get; set; }
    [SerializeField] private GameObject _Mesh;
    [SerializeField] private Curtain _curtain;
    private Animator _animator;
    private TimeСounting _time = new();
    private Coroutine _spawnTime;
    private Coroutine _decontaminationTime;
    public Action EnemyIsActivated;

    private void Start()
    {
        ChangeEnemyVisibility(false);
        SubscribeToRespawn();
        _animator = GetComponent<Animator>();
    }

    protected override void SpawnMonster()
    {

    }
    public override void Activate()
    {
        ChangeEnemyVisibility(true);
        EnemyIsActivated?.Invoke();
        UnsubscribeFromRespawn();
    }
    public override void Deactivate()
    {
        ChangeEnemyVisibility(false);
        UnsubscribeFromDeactivateEnemy();
        SubscribeToRespawn();   //нужно поработать с кнопкой чтоб счетчик начинался когда шторы будут открыты
    }
    private void CheckTimeIsUp(bool TimeIsUp)
    {
        if (TimeIsUp)
        {
            Activate();
        }
    }
    private void CheckCurtainOpen(bool TimeIsUp)
    {
        if (!_curtain.Open && TimeIsUp)
        {
            Deactivate();
            return;
        }
        else if (_curtain.Open && !TimeIsUp)
        {
            UnsubscribeFromDeactivateEnemy();
            StopCoroutine(_decontaminationTime);
            _animator.Play("attack_shift");
        }
    }

    #region SubscribeAndUnsubscribeDeactivate
    public void SubscribeToDeactivateEnemy()
    {
        _time.TimeIsUp += CheckCurtainOpen;
        _decontaminationTime = StartCoroutine(_time.TimerCounting(3f));
    }
    private void UnsubscribeFromDeactivateEnemy()
    {
        _time.TimeIsUp -= CheckCurtainOpen;
    }
    #endregion

    #region SubscribeAndUnsubscribeRespawn
    private void SubscribeToRespawn()
    {
        _time.TimeIsUp += CheckTimeIsUp;
        _spawnTime = StartCoroutine(_time.TimerCounting(5f));
    }
    private void UnsubscribeFromRespawn()
    {
        StopCoroutine(_spawnTime);
        _time.TimeIsUp -= CheckTimeIsUp;
    }
    #endregion
    private void ChangeEnemyVisibility(bool state)
    {
        _Mesh.gameObject.SetActive(state);
    }
}
