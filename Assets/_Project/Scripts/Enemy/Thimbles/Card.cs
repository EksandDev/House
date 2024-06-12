using UnityEngine;

public class Card : MonoBehaviour
{
    public void ChangingVisibility(bool visibility)
    {
        if (gameObject.activeSelf != visibility)
        {
            gameObject.SetActive(visibility);
        }
    }

    public void TeleportCard(Vector3 target)
    {
        gameObject.SetActive(true);
        transform.position = target + Vector3.down / 10;
    }
}
