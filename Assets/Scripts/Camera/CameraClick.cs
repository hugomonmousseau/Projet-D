using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClick : MonoBehaviour
{
    //private
    Camera _mainCam;
    Plane _planeLD;
    Plane _planeConnexion;
    public Plane _HUDPlane;
    [SerializeField] GameObject _planeHUDgo;
    public Vector3 _worldPosition;
    public Vector3 _HUDposition;

    [SerializeField] GameObject _t;

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


        Vector3 normal = _planeHUDgo.transform.up;
        // Calcule la distance du plane par rapport à l'origine en utilisant le produit scalaire
        //float distance = Vector3.Dot(normal, _planeHUDgo.transform.position);
        // Crée le plane avec la normale et la distance calculées
        _HUDPlane = new Plane(normal, -31.6f);


        if (_HUDPlane.Raycast(_ray, out float _HUDdistance))
            _HUDposition = _ray.GetPoint(_HUDdistance);
        //hud prio
        RaycastHit[] _resultsHUD = new RaycastHit[1];
        int _hitHUD = Physics.RaycastNonAlloc(_ray, _resultsHUD, float.MaxValue, LayerMask.GetMask("HUD"));

        //selection de batiments
        RaycastHit[] _results = new RaycastHit[1];
        int _hits = Physics.RaycastNonAlloc(_ray, _results);


        
        if (_resultsHUD[0].collider != null)
        {
            if (Input.GetMouseButtonDown(0))
                _results[0].collider.GetComponent<RotationSlotManager>().ButtonIsPress();

            if (Input.GetMouseButtonUp(0))
                _results[0].collider.GetComponent<RotationSlotManager>().ButtonIsRelease();
        }

        _t.transform.position = _HUDposition;








        if (_results[0].collider != null)
        {
            //Debug.Log(_results[0].collider.name);

            //_planeLD = new Plane(Vector3.down,_results[0].collider.transform.position.y + GameManager._instance._pointHeight);
        }

    }

}
