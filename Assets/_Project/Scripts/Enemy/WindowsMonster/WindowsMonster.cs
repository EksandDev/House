using System;
using UnityEngine;

[RequireComponent(typeof(WindowsMonsterAnimation))]
public class WindowsMonster : EnemyMain
{
    [SerializeField] private GameObject _Mesh;
    [SerializeField] private Curtain _curtain;
    private Coroutine _decontaminationTime;
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
        _timeCounting.TimeIsUp += CheckCurtainOpen;
        _decontaminationTime = StartCoroutine(_timeCounting.TimerCounting(TimeToActivate));
    }
    private void UnsubscribeFromDeactivateEnemy()
    {
        _timeCounting.TimeIsUp -= CheckCurtainOpen;
    }

    #endregion

    private void ChangeEnemyVisibility(bool state)
    {
        _Mesh.gameObject.SetActive(state);
    }
}
