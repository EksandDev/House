
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Thimbles : MonoBehaviour
{
    private Plate[] _plates = new Plate[3];
    private Plate _firstPlate;
    private Plate _secondPlate;
    private Card _card;
    private float _procent;
    private Vector3 _one;
    private Vector3 _two;
    private Bezie _bezie = new();
    public bool ThimbleSelection { get; private set; } = true;

    private void Start()
    {
        _card = GetComponentInChildren<Card>(true);
        _plates = GetComponentsInChildren<Plate>();
    }

    public void Activate()
    {
        if (ThimbleSelection)
        {
            PlateSelectionFirst();
            PlateSelectionSecond();
            ThimbleSelection = false;
        }
    }

    private void PlateSelectionFirst()
    {
        _firstPlate = _plates[RandomNumber()];
        TeleportCard();
        _firstPlate.AnimationSelected();
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
        _card.transform.position = _firstPlate.transform.position + Vector3.down / 10;
    }
    private IEnumerator PlateMovements()
    {
        _firstPlate.AddCardAndParent(_card);
        for (int i = 0; i < 3; i++)
        {
            PlateSelectionSecond();
            StartCoroutine(TrajectoryСalculation());
            yield return new WaitForSeconds(1.2f);
        }
    }
    private IEnumerator TrajectoryСalculation()
    {
        _one = _firstPlate.transform.position;
        _two = _secondPlate.transform.position;
        while (_procent < 1)
        {
            _firstPlate.transform.position = _bezie.GetPoints(_one, _two, _procent, Vector3.right);
            _secondPlate.transform.position = _bezie.GetPoints(_two, _one, _procent, Vector3.left);
            _procent += Time.deltaTime;
            yield return null;
        }
        _procent = 0;
    }

    public void SelectedThimbles(bool bol)
    {
        ThimbleSelection = bol;
    }






}
