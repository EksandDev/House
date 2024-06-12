using System.Collections;
using UnityEngine;

public class ThimblesMovement : MonoBehaviour
{
    private Vector3 _firstThimbleVector, _twoThimbleVector;
    private ThimblesEnemy _thimblesGame;
    private float _movementPercentage;
    private Bezie _bezie = new();
    private void Start()
    {
        _thimblesGame = GetComponent<ThimblesEnemy>();
    }
    public void Movements()
    {
        StartCoroutine(PlateMovements());
    }

    private IEnumerator PlateMovements()
    {
        for (int i = 0; i < 3; i++)
        {
            _thimblesGame.ThimbleActivateSecond();
            StartCoroutine(TrajectoryСalculation());
            yield return new WaitForSeconds(1.2f);
        }
        // _thimblesGame.ActivationsThimbles(false);
        _thimblesGame.ThimbleClick = true;
    }

    private IEnumerator TrajectoryСalculation()
    {
        _firstThimbleVector = _thimblesGame.FirstThimble.transform.position;
        _twoThimbleVector = _thimblesGame.SecondThimble.transform.position;
        while (_movementPercentage < 1)
        {
            _thimblesGame.FirstThimble.transform.position = _bezie.GetPoints(_firstThimbleVector, _twoThimbleVector, _movementPercentage, Vector3.right);
            _thimblesGame.SecondThimble.transform.position = _bezie.GetPoints(_twoThimbleVector, _firstThimbleVector, _movementPercentage, Vector3.left);
            _movementPercentage += Time.deltaTime;
            yield return null;
        }
        _movementPercentage = 0;
    }
}
