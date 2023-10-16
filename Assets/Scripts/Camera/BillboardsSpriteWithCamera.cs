using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardsSpriteWithCamera : MonoBehaviour
{
    Camera _mainCamera;
    [SerializeField] float _addRotation;
    [SerializeField] bool _onlyY = false;
    void Start()
    {
        _mainCamera = Camera.main;
        //ou
        //_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        transform.LookAt(_mainCamera.transform);
        transform.Rotate(0, 180 + _addRotation, 0);
        if (_onlyY)
        {
            transform.LookAt(new Vector3(_mainCamera.transform.position.x, transform.position.y, _mainCamera.transform.position.z));
        }
    }
}
