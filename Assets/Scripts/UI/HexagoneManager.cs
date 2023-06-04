using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagoneManager : MonoBehaviour
{

    private Vector3 _velocity = Vector3.zero;
    [SerializeField] float _speed;
    Animator _anim;
    Vector3 _objPosition;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }


    public void MoveTo(Vector3 _newPosition)
    {
        _objPosition = _newPosition;
    }

    void Update()
    {
        if(!GameManager._instance._alreadyALine )
            transform.position = Vector3.SmoothDamp(transform.position, _objPosition, ref _velocity, _speed);
    }

    /*
    IEnumerator HexagoneMovement(Vector3 _newPosition)
    {
        float _delay = 0;
        while(_delay < _speed)
        {
            _delay += Time.deltaTime;
            transform.position = Vector3.SmoothDamp(transform.position, _newPosition, ref _velocity, _speed);
            Debug.Log("actual pos : " + transform.position + " obj pos : " + _newPosition);
            yield return null;
        }
    }
    */
}
