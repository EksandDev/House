using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class WaterMonster : Monster
{
    [SerializeField] private DecalProjector _traceProjector;
    [SerializeField] private float _timeToSpawnTrace = 1;
    [SerializeField] private bool _hasAI;

    private NavMeshAgent _agent;
    private List<DecalProjector> _decalProjectors;

    public void SetTarget(Transform target)
    {
        if (!_hasAI)
            return;

        Target = target;
        _agent.SetDestination(Target.position);
    }

    public IEnumerator Death()
    {
        _agent.enabled = false;
        float tweensDuration = 10;
        Tween tween = transform.DOMoveY(transform.position.y - 1, tweensDuration).SetLink(gameObject);
        transform.DOShakeRotation(tweensDuration, 20).SetLink(gameObject);

        yield return tween.WaitForCompletion();

        Destroy(gameObject);
    }

    protected override void Act()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        StartCoroutine(SpawnTrace());
    }

    private void OnDestroy()
    {
        foreach (var projector in _decalProjectors)
            Destroy(projector.gameObject);
    }

    private IEnumerator SpawnTrace()
    {
        _decalProjectors = new();

        while (true)
        {
            var projector = Instantiate
                (_traceProjector, transform.position, _traceProjector.transform.rotation);
            var randomSize = Random.Range(1.3f, 2);
            projector.size = new Vector3(randomSize, randomSize, projector.size.z);
            _decalProjectors.Add(projector);

            yield return new WaitForSeconds(_timeToSpawnTrace);
        }
    }
}