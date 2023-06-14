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
        _planeConnexion = new Plane(Vector3.down, GameManager._instance._pointHeight);
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
            GameManager._instance._startSelectionConnexionCoordonnees = new Vector2(_connexionPosition.x, _connexionPosition.z);
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
        if (_results[0].collider != null && GameManager._instance._gameState == GameState.Default)
            GameManager._instance.HexagoneSelection(_results[0]);
        //Debug.Log(_results[0].collider.tag);
        //if (_results[0].collider != null && Input.GetMouseButtonDown(0))
        if (_results[0].collider != null && GameManager._instance._gameState == GameState.Default && !GameManager._instance._alreadyALine)
        {
            //Debug.Log(_results[0].collider.tag);
            if (_results[0].collider.tag == "Batiment")
            {
                //on clic sur un batiment
                //_results[0].collider.GetComponent<OnSelectedBatiment>()._selected = true;

                //on vérifie aussi que le batiment séléctionné ne l est pas deja
                if (GameManager._instance._lastSelection != _results[0].collider.gameObject)
                {
                    if (GameManager._instance._lastSelection != null)
                        GameManager._instance._lastSelection.GetComponent<OnSelectedBatiment>().ImNotSelected();
                    //Debug.Log(_results[0].collider.gameObject.name);
                    GameManager._instance.NewBatimentSelection(_results[0].collider.gameObject);
                }

                //on s assure des scripts présents dans le batiment
            }


        }

        //pour less tuiles

        else if (_results[0].collider != null && GameManager._instance._gameState == GameState.IsBuying)
        {
            if (_results[0].collider.tag == "Tile")
            {
                GameManager._instance._tileWeAreLooking._coordonnees = _results[0].collider.transform.position;
                GameManager._instance._tileWeAreLooking._isEmpty = _results[0].collider.GetComponent<TileID>()._tile._isEmpty;
                


                float _height = _results[0].collider.transform.position.y;
                Plane _planePrevisualisation = new Plane(Vector3.down, _height);
                Vector3 _exactPrevPos = new Vector3();
                if (_planePrevisualisation.Raycast(_ray, out float _previsualisationPosition))
                {
                     _exactPrevPos = _ray.GetPoint(_previsualisationPosition);
                }
                GameManager._instance._previsualisationPosition = _exactPrevPos;
            }
            //ca n arrive jamais

            else if (_results[0].collider == null && Input.GetMouseButtonDown(0))
            {
                if (GameManager._instance._lastSelection != null)
                    GameManager._instance._lastSelection.GetComponent<OnSelectedBatiment>().ImNotSelected();

                //qd on clic le vide
                GameManager._instance._lastSelection = null;
                Debug.Log("Urgent venir corrigé");
            }
        }
    }
}
