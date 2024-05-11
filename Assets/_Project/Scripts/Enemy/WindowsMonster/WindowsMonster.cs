using System;
using UnityEngine;

[RequireComponent(typeof(WindowsMonsterAnimation))]
public class WindowsMonster : Enemy
{
    [SerializeField] private GameObject _Mesh;
    [SerializeField] private Curtain _curtain;
    private void Start()
    {
        ChangeEnemyVisibility(_Mesh, false);
        SubscribeToRespawn();
    }

    public override void Activate()
    {
        ChangeEnemyVisibility(_Mesh, true);
        EnemyIsActivated?.Invoke();
    }

    public override void Deactivate()
    {
        ChangeEnemyVisibility(_Mesh, false);
        UnscribeCheckParametr();
        SubscribeToRespawn();
    }

    public override void CheckParameter(bool TimeIsUp)
    {
        if (!_curtain.Open && TimeIsUp)
        {
            Deactivate();
            return;
        }

        else if (_curtain.Open && !TimeIsUp)
        {
            UnscribeCheckParametr();
            Debug.Log("Умер от оконного монстра!");
        }
    }

}
