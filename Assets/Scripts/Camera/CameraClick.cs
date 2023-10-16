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

        if (_planeConnexion.Raycast(_ray, out float _connexionDistance))
        {
            _connexionPosition = _ray.GetPoint(_connexionDistance);
        }

        //selection de batiments
        RaycastHit[] _results = new RaycastHit[1];
        int _hits = Physics.RaycastNonAlloc(_ray, _results);

        RaycastHit[] _resultDiceFace = new RaycastHit[1];
        int _hitDiceFace = Physics.RaycastNonAlloc(_ray, _resultDiceFace, float.MaxValue, LayerMask.GetMask("Dice HUD"));




        if (_results[0].collider != null && GameManager._instance._gameState == GameState.Default)
        {
            if (_results[0].collider.tag == "Batiment")
                //GameManager._instance.HexagoneSelection(_results[0]);

                if (_results[0].collider.tag == "DiceFace")
                {
                    //Debug.Log(_results[0].collider.name);
                }
        }





        //hud passe en prio

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(_worldPosition);
            //Debug.Log(_connexionPosition);
            //GameManager._instance._startSelectionConnexionCoordonnees = new Vector3(_connexionPosition.x, _connexionPosition.y, _connexionPosition.z);

            //on test l huden prio

            if (_resultDiceFace[0].collider != null)
            {
                //Debug.Log(_results[0].collider.name);
                //if (_resultDiceFace[0].collider.tag == "DiceFace")
                   // _results[0].collider.GetComponent<DiceHUD>()._bat.GetComponent<Animator>().SetBool("HUD", !_results[0].collider.GetComponent<DiceHUD>()._bat.GetComponent<Animator>().GetBool("HUD"));
            }

            if (_results[0].collider != null)
            {
                //Debug.Log(_results[0].collider.tag);
                //if (_results[0].collider.tag != "DiceFace")
                    //GameManager._instance.NewConnexion();
            }
            else
            {
                //GameManager._instance.NewConnexion();

            }




        }

        /*
        if (Input.GetMouseButton(0))
        {
            GameManager._instance._selectionConnexionCoordonnees = new Vector3(_connexionPosition.x, _connexionPosition.y, _connexionPosition.z);
            GameManager._instance._selectionWorldCoordonnees = new Vector3(_connexionPosition.x, _connexionPosition.y, _connexionPosition.z);
        }
        GameManager._instance._actualSelectionConnexionCoordonnees = new Vector3(_connexionPosition.x, _connexionPosition.y, _connexionPosition.z);

        */




        if (_results[0].collider != null)
        {
            //Debug.Log(_results[0].collider.name);

            //_planeConnexion = new Plane(Vector3.down,_results[0].collider.transform.position.y + GameManager._instance._pointHeight);
        }

    }

}
