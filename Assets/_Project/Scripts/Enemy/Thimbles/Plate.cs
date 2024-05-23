
using System;
using DG.Tweening;
using UnityEngine;

public class Plate : MonoBehaviour, IInteractable
{
    public Vector3 StartPos { get; private set; }
    private Card _card;
    public Action Animations;
    private void Start()
    {
        StartPos = transform.localPosition;
    }

    public void AnimationSelected()
    {
        DOTween.Sequence()
             .Append(transform.DOLocalMoveY(transform.localPosition.y + 0.3f, 0.5f))
             .AppendInterval(1f)
             .Append(transform.DOLocalMoveY(StartPos.y, 0.5f))
             .OnComplete(EndAnimation);
    }

    private void EndAnimation()
    {
        Animations?.Invoke();
    }

    public void CardAdd(Card card)
    {
        _card = card;
        _card.transform.SetParent(transform);
    }

}
