using DG.Tweening;
using UnityEngine;

public class ButtonThimbles : MonoBehaviour, IClickable
{
    private Vector3 _startPos;
    private ThimblesEnemy _thimbles;
    private void Start()
    {
        _thimbles = GetComponentInParent<ThimblesEnemy>();
        _startPos = transform.localPosition;
    }

    public void OnClick()
    {
        if (Input.GetKeyDown(KeyCode.E) && _thimbles.ActivationThimbles)
        {
            StartGame();
            DOTween.Sequence()
            .Append(transform.DOLocalMoveY(transform.localPosition.y - 0.05f, 0.1f))
            .AppendInterval(0.1f)
            .Append(transform.DOLocalMoveY(_startPos.y, 0.2f));
        }
    }

    private void StartGame()
    {
        _thimbles.ActivateGame();
    }
}
