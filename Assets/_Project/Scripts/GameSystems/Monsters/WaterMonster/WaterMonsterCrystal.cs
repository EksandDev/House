using UnityEngine;

public class WaterMonsterCrystal : MonoBehaviour, IClickable
{
    [SerializeField] private WaterMonsterSpot _spot;

    public void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _spot.Deactivate();
            Destroy(gameObject);
        }
    }
}
