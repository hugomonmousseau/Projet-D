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
        _planeConnexion = new Plane(Vector3.down, 0.3f);
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

        if (_planeConnexion.Raycast(_ray, out float _connexionDistance))
        {
            _connexionPosition = _ray.GetPoint(_connexionDistance);
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(_worldPosition);
            //Debug.Log(_connexionPosition);
            GameManager._instance._startSelectionConnexionCoordonnees = new Vector2(_connexionPosition.x,_connexionPosition.z);
            GameManager._instance.NewConnexion();
        }
        if (Input.GetMouseButton(0))
        {
            GameManager._instance._selectionConnexionCoordonnees = new Vector2(_connexionPosition.x, _connexionPosition.z);
            GameManager._instance._selectionWorldCoordonnees = new Vector2(_connexionPosition.x, _connexionPosition.z);
        }
        GameManager._instance._actualSelectionConnexionCoordonnees = new Vector2(_connexionPosition.x, _connexionPosition.z);

        //selection de batiments
        RaycastHit[] _results = new RaycastHit[1];
        int _hits = Physics.RaycastNonAlloc(_ray, _results);
        //Debug.Log(_results[0].collider.tag);
        if (_results[0].collider != null && Input.GetMouseButtonDown(0))
        {
            Debug.Log(_results[0].collider.tag);
            if (_results[0].collider.tag == "Batiment")
            {
                //on clic sur un batiment
                //_results[0].collider.GetComponent<OnSelectedBatiment>()._selected = true;
                if (GameManager._instance._lastSelection != null)
                    GameManager._instance._lastSelection.GetComponent<OnSelectedBatiment>().ImNotSelected();
                GameManager._instance.NewBatimentSelection(_results[0].collider.gameObject);
                //on s assure des scripts présents dans le batiment
            }

        }
        else if (_results[0].collider == null && Input.GetMouseButtonDown(0))
        {
            if (GameManager._instance._lastSelection != null)
                GameManager._instance._lastSelection.GetComponent<OnSelectedBatiment>().ImNotSelected();

            //qd on clic le vide
            GameManager._instance._lastSelection = null;
        }
    }
}
