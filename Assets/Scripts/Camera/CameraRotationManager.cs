using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationManager : MonoBehaviour
{
    [SerializeField] float _maxX = 90f;
    [SerializeField] float _minX = 0f;
    [SerializeField] float _speed;
    float _rotationX;
    float _rotationY;

    Vector2 _lastMousePosition;

    void Start()
    {
        //comme dans l inspecteur
        _rotationX = transform.localEulerAngles.x;
        _rotationY = transform.localEulerAngles.y;
    }


    void Update()
    {
        //rotation
        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);

        //la souris
        Vector2 _mousePosition = Input.mousePosition;

        //au clic
        if (Input.GetMouseButtonDown(1))
        {
            //on recup la position de la souris qd clic droit
            _lastMousePosition = _mousePosition;
        }

        //tant qu on relache pas
        if (Input.GetMouseButton(1))
        {
            Vector2 _difference = _lastMousePosition - _mousePosition;
            _rotationX -= _difference.y * Time.deltaTime * _speed;
            _rotationY -= _difference.x * Time.deltaTime * _speed;
        }

        _lastMousePosition = _mousePosition;

        //Pour que la camera ne parte pas en couille
        if (_rotationX > _maxX)
            _rotationX = _maxX;
        if (_rotationX < _minX)
            _rotationX = _minX;
    }

}
