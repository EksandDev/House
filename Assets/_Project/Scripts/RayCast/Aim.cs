using UnityEngine;

public class Aim : MonoBehaviour
{
    private Camera _mainCamera;
    Vector3 Ray_start_position;
    RaycastHit hit;
    Ray _ray = new();

    private void Start()
    {
        RaySetSize();
        _mainCamera = GetComponentInChildren<Camera>();
    }
    private void Update()
    {

        RayCast();
    }

    private void RaySetSize()
    {
        Ray_start_position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }
    private void RayCast()
    {
        _ray = _mainCamera.ScreenPointToRay(Ray_start_position);
        if (Physics.Raycast(_ray, out hit))
        {
            if (hit.collider.TryGetComponent<Curtain>(out Curtain curtain))
            {
                if (Input.GetKey(KeyCode.E))
                {
                    curtain.CloseCurtain();
                    return;
                }
                else
                {
                    curtain.OpenCurtain();
                }
            }
        }

    }
}


