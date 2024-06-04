
using System;
using DG.Tweening;
using UnityEngine;

public class Plate : MonoBehaviour, IClickable
{
    private Vector3 _startPos;
    private Card _card;
    public Action Animations;
    private Thimbles _thimbles;

    private void Start()
    {
        _thimbles = GetComponentInParent<Thimbles>();
    }

    public void OnClick()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _startPos = transform.localPosition;
            AnimationCheckTheCard();
            CardDelParent();
        }
    }

    private void CheckTheCard()
    {
        _thimbles.SelectedThimbles(true);
        if (_card != null)
        {
            _card.ChangingVisibility(false);
            _card = null;
            Debug.Log("Наперстки обезврежены");
            return;
        }
        Debug.Log("Неверный наперсток!! Лови аплиуху!");
    }


    public void AnimationSelected()
    {
        DOTween.Sequence()
             .Append(transform.DOLocalMoveY(transform.localPosition.y + 0.3f, 0.5f))
             .AppendInterval(1f)
             .Append(transform.DOLocalMoveY(_startPos.y, 0.5f))
             .OnComplete(EndAnimation);
        return;
    }

    private void AnimationCheckTheCard()
    {
        DOTween.Sequence()
                    .Append(transform.DOLocalMoveY(transform.localPosition.y + 0.3f, 0.5f))
                    .AppendInterval(1f)
                    .Append(transform.DOLocalMoveY(_startPos.y, 0.5f))
                    .OnComplete(CheckTheCard);
        return;
    }

    private void EndAnimation()
    {
        Animations?.Invoke();
    }

    public void AddCardAndParent(Card card)
    {
        _card = card;
        AddParentCard(transform);
    }

    public void CardDelParent()
    {
        if (_card != null)
        {
            AddParentCard(null);
        }
    }

    private void AddParentCard(Transform parent)
    {
        _card.transform.SetParent(parent);
    }

}
