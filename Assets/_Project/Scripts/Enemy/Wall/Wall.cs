using UnityEngine;

public class Wall : Enemy
{

    [SerializeField] private GameObject _wall;
    private Coroutine _decontaminationTime;

    private void Start()
    {
        SubscribeToRespawn();
    }
    public override void Activate()
    {
        ChangeEnemyVisibility(true);
        SubscribeToDeactivateEnemy();
    }

    public override void Deactivate()
    {
        ChangeEnemyVisibility(false);
        UnsubscribeFromDeactivateEnemy();
    }

    private void ChangeEnemyVisibility(bool state)
    {
        _wall.gameObject.SetActive(state);
    }

    #region SubscribeAndUnsubscribeDeactivate
    public void SubscribeToDeactivateEnemy()
    {
        _timeCounting.TimeIsUp += CheckTimer;
        _decontaminationTime = StartCoroutine(_timeCounting.TimerCounting(5f));
    }
    private void UnsubscribeFromDeactivateEnemy()
    {
        StopCoroutine(_decontaminationTime);
        _timeCounting.TimeIsUp -= CheckTimer;
    }
    #endregion

    private void CheckTimer(bool TimeIsUp)
    {
        if (TimeIsUp)
        {
            Deactivate();
            SubscribeToRespawn();
        }
    }
}
