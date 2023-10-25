using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClick : MonoBehaviour
{
    //private
    Camera _mainCam;
    Plane _planeLD;
    Plane _planeConnexion;
    public Vector3 _worldPosition;
    public Vector3 _connexionPosition;

    void Start()
    {
        _mainCam = GetComponent<Camera>();

        //coordonné Y de la map
        _planeLD = new Plane(Vector3.down, 0f);
        //_planeConnexion = new Plane(Vector3.down, GameManager._instance._pointHeight);
    }


    void Update()
    {
        Vector3 _position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);


        Ray _ray = _mainCam.ScreenPointToRay(_position);

        //debug
        Debug.DrawRay(_ray.origin, _ray.direction * 100, Color.yellow);

        if (_planeLD.Raycast(_ray, out float _worldDistance))
        {
            _worldPosition = _ray.GetPoint(_worldDistance);
        }


        //hud prio
        RaycastHit[] _resultsHUD = new RaycastHit[1];
        int _hitHUD = Physics.RaycastNonAlloc(_ray, _resultsHUD, float.MaxValue, LayerMask.GetMask("HUD"));

        //selection de batiments
        RaycastHit[] _results = new RaycastHit[1];
        int _hits = Physics.RaycastNonAlloc(_ray, _results);


        
        if (_resultsHUD != null)
        {
            if (Input.GetMouseButtonDown(0))
                _results[0].collider.GetComponent<RotationSlotManager>().ButtonIsPress();

            if (Input.GetMouseButtonUp(0))
                _results[0].collider.GetComponent<RotationSlotManager>().ButtonIsRelease();

        }
        









        if (_results[0].collider != null)
        {
            //Debug.Log(_results[0].collider.name);

            //_planeLD = new Plane(Vector3.down,_results[0].collider.transform.position.y + GameManager._instance._pointHeight);
        }

    }

}
