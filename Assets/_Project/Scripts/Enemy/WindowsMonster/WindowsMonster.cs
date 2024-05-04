using System;
using UnityEngine;

[RequireComponent(typeof (WindowsMonsterAnimation))]
public class WindowsMonster : MonsterSpot
{
    [SerializeField] private GameObject _Mesh;
    [SerializeField] private Curtain _curtain;
    private Coroutine _spawnTime;
    private Coroutine _decontaminationTime;
    private TimeСounting _time = new();
    protected override float TimeToActivate { get; set; } = 10f;
    public Action EnemyIsActivated;

    private void Start()
    {
        ChangeEnemyVisibility(false);
        SubscribeToRespawn();
    }

    protected override void SpawnMonster()
    {
        Activate();
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
        SubscribeToRespawn();
    }

    private void CheckTimeIsUp(bool TimeIsUp)
    {
        if (TimeIsUp)
        {
            SpawnMonster();
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
            Debug.Log("Умер от оконного монстра!");
        }
    }

    #region SubscribeAndUnsubscribeDeactivate
    public void SubscribeToDeactivateEnemy()
    {
        _time.TimeIsUp += CheckCurtainOpen;
        _decontaminationTime = StartCoroutine(_time.TimerCounting(2f));
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
        _spawnTime = StartCoroutine(_time.TimerCounting(TimeToActivate));
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
