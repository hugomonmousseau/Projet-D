using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClick : MonoBehaviour
{
    //private
    Camera _mainCam;
    Plane _plane;

    public Vector3 _worldPosition;

    void Start()
    {
        _mainCam = GetComponent<Camera>();

        //coordonné Y de la map
        _plane = new Plane(Vector3.down, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);


        Ray _ray = _mainCam.ScreenPointToRay(_position);

        //debug
        Debug.DrawRay(_ray.origin, _ray.direction * 100, Color.yellow);

        if (_plane.Raycast(_ray, out float _distance))
        {
            _worldPosition = _ray.GetPoint(_distance);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(_worldPosition);
        }

    }
}
