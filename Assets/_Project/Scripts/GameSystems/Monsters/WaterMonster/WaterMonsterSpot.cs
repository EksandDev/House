using System.Collections;
using UnityEngine;

public class WaterMonsterSpot : MonsterSpot
{
    [SerializeField] private WaterMonster _waterMonsterPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private WaterMonsterSpot[] _otherSpots;
    [SerializeField] private HingeJoint _hingeJointPrefab;
    [SerializeField] private Rigidbody _previousBody;

    private float _timeToActivate;

    protected override float TimeToActivate { get => _timeToActivate; set => _timeToActivate = value; }

    public override void Activate()
    {
        _waterMonsterPrefab.SetTarget(_target);
        StartCoroutine(RopeSpawn());
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

    private IEnumerator RopeSpawn()
    {
        float minusMass = 0.03f;

        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            Vector3 spawnPoint = _previousBody.transform.GetChild(0).position;
            HingeJoint joint = Instantiate(_hingeJointPrefab, spawnPoint, _previousBody.transform.rotation);
            Rigidbody jointRB = joint.GetComponent<Rigidbody>();

            if (jointRB.mass > 1)
            {
                minusMass += 0.03f;
                jointRB.mass -= minusMass;
            }

            joint.connectedBody = _previousBody;
            _previousBody = joint.GetComponent<Rigidbody>();
        }
    }
}