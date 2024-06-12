using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshPro _timerVisual;
    private float _timer;
    private bool _stateTimer;
    private ThimblesEnemy _thimblesEnemy;
    private void Start()
    {
        _thimblesEnemy = GetComponentInParent<ThimblesEnemy>();
        _timerVisual = GetComponent<TextMeshPro>();
    }
    private IEnumerator Timers(float time)
    {
        _timer = time;
        while (_timer >= 0)
        {
            if (!_stateTimer)
            {
                yield break;
            }
            _timerVisual.text = _timer.ToString();
            _timer--;
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Не УСПЕЛ АКТИВИРОВАТЬ!");
    }

    public void Activate(float time)
    {
        TimerState(true);
        StartCoroutine(Timers(time));
        _thimblesEnemy.ActivationsThimbles(true);
    }

    public void Deactivate()
    {
        TimerState(false);
        _timerVisual.text = "";
    }
    public void TimerState(bool state)
    {
        _stateTimer = state;
    }
}
