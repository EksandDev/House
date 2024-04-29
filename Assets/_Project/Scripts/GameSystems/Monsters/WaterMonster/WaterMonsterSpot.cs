using UnityEngine;

public class WaterMonsterSpot : MonsterSpot
{
    [SerializeField] private WaterMonster _waterMonsterPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private WaterMonsterSpot[] _otherSpots;

    private float _timeToActivate;

    protected override float TimeToActivate { get => _timeToActivate; set => _timeToActivate = value; }

    public override void Activate()
    {
        _waterMonsterPrefab.SetTarget(_target);
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    protected override void SpawnMonster()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        Activate();
    }
}