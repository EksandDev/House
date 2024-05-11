using UnityEngine;

public class Wall : Enemy
{

    [SerializeField] private GameObject _wall;

    private void Start()
    {
        SubscribeToRespawn();
    }
    public override void Activate()
    {
        ChangeEnemyVisibility(true);
        SubscribeCheckParametr();
    }

    public override void Deactivate()
    {
        ChangeEnemyVisibility(false);
        UnscribeCheckParametr();
    }

    private void ChangeEnemyVisibility(bool state)
    {
        _wall.gameObject.SetActive(state);
    }

    public override void CheckParameter(bool TimeIsUp)
    {
        if (TimeIsUp)
        {
            Deactivate();
            SubscribeToRespawn();
        }
    }
}
