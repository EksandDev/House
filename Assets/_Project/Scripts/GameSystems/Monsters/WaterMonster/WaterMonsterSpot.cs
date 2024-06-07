using UnityEngine;

public class WaterMonsterSpot : MonsterSpot
{
    [SerializeField] private WaterMonster _waterMonsterPrefab;
    [SerializeField] private Transform _monsterSpawnPoint;
    [SerializeField] private Transform _target;
    [SerializeField] private WaterMonsterSpot[] _otherSpots;

    private WaterMonster _currentMonster;
    private float _timeToActivate;

    protected override float TimeToActivate { get => _timeToActivate; set => _timeToActivate = value; }

    public override void Activate()
    {
        SpawnMonster();
    }

    public override void Deactivate()
    {
        StartCoroutine(_currentMonster.Death());
    }

    protected override void SpawnMonster()
    {
        _currentMonster = Instantiate(_waterMonsterPrefab, _monsterSpawnPoint.position, Quaternion.identity);
        _currentMonster.SetTarget(_target);
    }

    private void Start()
    {
        Activate();
    }
}