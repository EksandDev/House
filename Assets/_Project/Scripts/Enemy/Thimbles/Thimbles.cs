using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Thimbles : MonoBehaviour
{
    private Plate[] _plates = new Plate[3];
    private Plate _firstPlate;
    private Plate _secondPlate;
    private Card _card;


    private void Start()
    {
        _card = GetComponentInChildren<Card>(true);
        _plates = GetComponentsInChildren<Plate>();
        Invoke(nameof(PlateSelection), 1f);
    }


    private void CheckPlate(Plate plate)
    {

    }
    private void PlateSelection()
    {
        _firstPlate = _plates[RandomNumber()];
        _secondPlate = _plates[RandomNumber()];
        _firstPlate.AnimationSelected();
        TeleportCard();
        _firstPlate.Animations += MotionPlate;
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
        AnimationPlate();
        _firstPlate.Animations -= MotionPlate;
    }

    private void TeleportCard()
    {
        _card.gameObject.SetActive(true);
        _card.transform.localPosition = _firstPlate.transform.localPosition + Vector3.down / 10;
    }

    private void AnimationPlate()
    {
        _firstPlate.CardAdd(_card);
        _firstPlate.transform.DOLocalMove(_secondPlate.transform.localPosition, 0.5f);
        _secondPlate.transform.DOLocalMove(_firstPlate.transform.localPosition, 0.5f);
    }


}
