using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationManager : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] float _maxX = 90f;
    [SerializeField] float _minX = 0f;
    [SerializeField] float _roationSpeed;
    float _rotationX;
    float _rotationY;

    [Space]
    [Header("Zoom")]
    [SerializeField] float _maxZoom;
    [SerializeField] float _minZoom;
    [SerializeField] Transform _camera;
    [SerializeField] float _zoomSpeed;
    float _distance;

    Vector2 _lastMousePosition;

    void Start()
    {
        //comme dans l inspecteur
        _rotationX = transform.localEulerAngles.x;
        _rotationY = transform.localEulerAngles.y;
        _distance = _camera.transform.localPosition.y;
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
            GameManager._instance._userState = UserState.MovingTheCamera;
        }
        //au relachement
        if (Input.GetMouseButtonUp(1))
        {
            GameManager._instance._userState = UserState.Default;
        }
        //tant qu on relache pas
        if (Input.GetMouseButton(1))
        {
            Vector2 _difference = _lastMousePosition - _mousePosition;
            _rotationX -= _difference.y * Time.deltaTime * _roationSpeed;
            _rotationY -= _difference.x * Time.deltaTime * _roationSpeed;
        }

        _lastMousePosition = _mousePosition;

        //Pour que la camera ne parte pas en couille
        if (_rotationX > _maxX)
            _rotationX = _maxX;
        if (_rotationX < _minX)
            _rotationX = _minX;

        
        //zoom
        _camera.transform.localPosition = new Vector3(0,_distance,0);

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput > 0)
        {
            _distance += _zoomSpeed * _distance;
            if (_distance > _maxZoom)
                _distance = _maxZoom;
        }
        if (scrollInput < 0)
        {
            _distance -= _zoomSpeed * _distance;
            if (_distance < _minZoom)
                _distance = _minZoom;
        }

        
    }

}
