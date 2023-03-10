using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardsSpriteWithCamera : MonoBehaviour
{
    Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
        //ou
        //_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        transform.LookAt(_mainCamera.transform);
        transform.Rotate(0, 180, 0);
    }
}
