
using System;
using DG.Tweening;
using UnityEngine;

public class Thimble : MonoBehaviour, IClickable
{
    private Vector3 _startPos;
    private Card _card;
    public Action Animations;
    private ThimblesEnemy _thimbles;

    private void Start()
    {
        _thimbles = GetComponentInParent<ThimblesEnemy>();
    }

    public void OnClick()
    {
        if (Input.GetKeyDown(KeyCode.E) && _thimbles.ThimbleClick)
        {
            AnimationCheckTheCard();
            CardDelParent();
            _thimbles.ThimbleClick = false;
        }
    }

    private void CheckTheCard()
    {
        // _thimbles.ActivationsThimbles(true);
        if (_card != null)
        {
            _card.ChangingVisibility(false);
            _card = null;
            _thimbles.Deactivate();
            Debug.Log("Наперстки обезврежены");
            return;
        }
        _thimbles.Baff();
    }


    public void AnimationSelected()
    {
        _startPos = transform.localPosition;
        DOTween.Sequence()
             .Append(transform.DOLocalMoveY(transform.localPosition.y + 0.3f, 0.5f))
             .AppendInterval(1f)
             .Append(transform.DOLocalMoveY(_startPos.y, 0.5f))
             .OnComplete(EndAnimation);
        return;
    }

    private void AnimationCheckTheCard()
    {
        _startPos = transform.localPosition;
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
