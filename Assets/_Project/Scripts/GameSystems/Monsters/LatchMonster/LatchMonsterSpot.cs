using System.Collections;
using UnityEngine;

public class LatchMonsterSpot : MonsterSpot
{
    [SerializeField] private MovableObject[] _latches = new MovableObject[3];

    private float _timeToActivate;

    protected override float TimeToActivate { get => _timeToActivate; set => _timeToActivate = value; }

    public override void Activate()
    {
        StartCoroutine(OpenLatches());
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    protected override void SpawnMonster()
    {
        Debug.Log("Mimi");
    }

    private void Start() => Activate();

    private IEnumerator OpenLatches()
    {
        while (true)
        {
            foreach (var latch in _latches)
            {
                if (latch.IsOpen)
                    continue;

                latch.IsOpen = true;

                break;
            }

            yield return new WaitForSeconds(3);
        }
    }
}