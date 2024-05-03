using UnityEngine;
using UnityEngine.AI;

public class WaterMonster : Monster
{
    [SerializeField] private bool _hasAI;

    private NavMeshAgent _agent;

    public void SetTarget(Transform target)
    {
        if (!_hasAI)
            return;

        Target = target;
        _agent.SetDestination(Target.position);
    }

    protected override void Act()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
}