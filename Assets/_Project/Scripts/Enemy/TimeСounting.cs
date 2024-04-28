using System;
using System.Collections;
using UnityEngine;
public class TimeСounting
{
    public Action<bool> TimeIsUp;
    public Action<float> TimeAnimation;

    public IEnumerator TimerCounting(float delay)
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            TimeAnimation?.Invoke(timer / delay);
            TimeIsUp?.Invoke(false);
            if (timer >= delay)
            {
                TimeIsUp?.Invoke(true);
                yield break;
            }
            yield return null;
        }
    }

}
