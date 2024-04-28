using System.Collections;
using UnityEngine;

public class LatchMonster : Monster
{
    [SerializeField] private MovableObject[] _latches = new MovableObject[3];

    private float _timeToRespawn;

    protected override float TimeToRespawn { get => _timeToRespawn; set => _timeToRespawn = value; }

    public override void Activate()
    {
        foreach (var latch in  _latches)
        {
            if (latch.IsOpen)
                continue;

            latch.IsOpen = true;
            return;
        }

        Act();
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    protected override void Act()
    {
        Debug.Log("Sdoh kak loh");
    }

    private void Start()
    {
        StartCoroutine(GoActivate());
    }

    private IEnumerator GoActivate()
    {
        while (true)
        {
            Activate();

            yield return new WaitForSeconds(3);
        }
    }
}
