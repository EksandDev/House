
using System.Collections;
using System.Linq.Expressions;
using DG.Tweening;
using UnityEngine;

public class ShakerCam : MonoBehaviour
{
private bool _walking;
private Vector3 _startPos;
private Vector3 _endPos;
[SerializeField] private float _speedWalking = 0.5f;
[SerializeField] private float _testKf=0.01f;

private void Start() {
    _startPos = transform.position;
    _endPos = transform.position + Vector3.down/20;
}
    private void Update() {

        if(Input.GetKeyDown(KeyCode.W)&!_walking)
        {
           Debug.Log("ok");
           StartCoroutine(ShackerShaker());
        }
    }

    private IEnumerator ShackerShaker()
    {

        while(Input.GetKey(KeyCode.W)){
        _walking = true;
        // transform.DOMoveY(transform.position.y-0.1f,0.4f);
        StartCoroutine(MoveCam(Vector3.down/15));
        Debug.Log("1");
        yield return new WaitForSeconds(_speedWalking);
        StartCoroutine(MoveCam(Vector3.up/15));
        Debug.Log("2");
        // transform.Translate(Vector3.up*Time.deltaTime, Space.Self);
        // transform.DOMoveY(transform.position.y+0.1f,0.4f);
        yield return new WaitForSeconds(_speedWalking);
        }
        _walking = false;
    }

    private IEnumerator MoveCam(Vector3 targetPos){
        Vector3 MoveY = transform.localPosition + targetPos;
        while(transform.localPosition.y != MoveY.y){
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, MoveY, _testKf);
       
        yield return null;
        }
    }
}
