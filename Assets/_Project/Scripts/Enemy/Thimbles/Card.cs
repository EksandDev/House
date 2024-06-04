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

    public void SetTheParent(Transform parent)
    {
        transform.SetParent(parent);
    }
}
