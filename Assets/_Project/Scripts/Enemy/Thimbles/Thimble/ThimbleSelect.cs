using UnityEngine;

public class ThimbleSelect : MonoBehaviour
{
    private Thimble[] _thimbles = new Thimble[3];
    private ThimblesEnemy _thimblesGame;
    private void Start()
    {
        _thimbles = GetComponentsInChildren<Thimble>();
        _thimblesGame = GetComponent<ThimblesEnemy>();
    }
    public Thimble ThimbleSelection()
    {
        Thimble thimble = _thimbles[RandomNumber()];
        while (thimble == _thimblesGame.FirstThimble)
        {
            thimble = _thimbles[RandomNumber()];
        }
        return thimble;
    }

    private int RandomNumber()
    {
        int randomNumber = Random.Range(0, _thimbles.Length);
        return randomNumber;
    }
}
