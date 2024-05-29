
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Thimbles : MonoBehaviour
{
    private Plate[] _plates = new Plate[3];
    private Plate _firstPlate;
    private Plate _secondPlate;
    private Card _card;
    [SerializeField] private Vector3 _one;
    [SerializeField] private Vector3 _two;

    private Bezie bezie = new();
    private void Start()
    {
        _card = GetComponentInChildren<Card>(true);
        _plates = GetComponentsInChildren<Plate>();
        PlateSelectionFirst();
        PlateSelectionSecond();
    }

    private void PlateSelectionFirst()
    {
        _firstPlate = _plates[RandomNumber()];
        _firstPlate.AnimationSelected();
        TeleportCard();
        _firstPlate.Animations += MotionPlate;
    }
    private void PlateSelectionSecond()
    {
        _secondPlate = _plates[RandomNumber()];
    }

    private int RandomNumber()
    {
        int randomNumber = Random.Range(0, _plates.Length);
        while (_plates[randomNumber] == _firstPlate)
        {
            randomNumber = Random.Range(0, 2);
        }
        return randomNumber;
    }

    private void MotionPlate()
    {
        StartCoroutine(PlateMovements());
        _firstPlate.Animations -= MotionPlate;
    }

    private void TeleportCard()
    {
        _card.gameObject.SetActive(true);
        _card.transform.localPosition = _firstPlate.transform.localPosition + Vector3.down / 10;
    }

    // private void AnimationPlate()
    // {
    //     DOTween.Sequence()
    //     .Append(_firstPlate.transform.DOLocalMove(_secondPlate.transform.localPosition, 0.5f))
    //     .Append(_secondPlate.transform.DOLocalMove(_firstPlate.transform.localPosition, 0.5f));
    // }

    private IEnumerator PlateMovements()
    {
        _firstPlate.CardAdd(_card);
        for (int i = 0; i < 3; i++)
        {
            // PlateSelectionSecond();
            // AnimationPlate();
            yield return new WaitForSeconds(1f);
        }
    }






}
