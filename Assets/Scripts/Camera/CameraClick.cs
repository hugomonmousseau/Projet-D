using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClick : MonoBehaviour
{
    //private
    Camera _mainCam;
    Plane _planeLD;
    public Plane _HUDPlane;
    public Vector3 _worldPosition;

    [Header("tiles")]
    GameObject _lastTile;

    [Header("HUD")]
    GameObject _lastHUD;

    void Start()
    {
        _mainCam = GetComponent<Camera>();

        //coordonné Y de la map
        _planeLD = new Plane(Vector3.down, 0f);

        
    }


    void Update()
    {
        Vector3 _position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);


        Ray _ray = _mainCam.ScreenPointToRay(_position);

        //debug
        Debug.DrawRay(_ray.origin, _ray.direction * 100, Color.yellow);


        if (_planeLD.Raycast(_ray, out float _worldDistance))
            _worldPosition = _ray.GetPoint(_worldDistance);


        //hud prio
        RaycastHit[] _resultsHUD = new RaycastHit[1];
        int _hitHUD = Physics.RaycastNonAlloc(_ray, _resultsHUD, float.MaxValue, LayerMask.GetMask("HUD"));

        if (_resultsHUD[0].collider != null)
        {
            if (_lastHUD != _resultsHUD[0].collider.gameObject)
            {
                if (_lastHUD != null)
                    _lastHUD.GetComponent<HUDManager>().NotEventHighlight();
                _lastHUD = _resultsHUD[0].collider.gameObject;
                _resultsHUD[0].collider.GetComponent<HUDManager>().Highlight();
            }
        }
        else if (_lastHUD != null)
        {
            _lastHUD.GetComponent<HUDManager>().NotEventHighlight();
            _lastHUD = null;
        }




        RaycastHit[] _resultsTile = new RaycastHit[1];
        int _hitTile = Physics.RaycastNonAlloc(_ray, _resultsTile, float.MaxValue, LayerMask.GetMask("Tile"));

        if(_resultsTile[0].collider != null)
        {
            _planeLD = new Plane(Vector3.down, _resultsTile[0].collider.transform.position.y);


            if (_lastTile == null)
            {
                _lastTile = _resultsTile[0].collider.gameObject;
                _resultsTile[0].collider.GetComponent<TileInteraction>().Highlight();
            }
            if(_lastTile != _resultsTile[0].collider.gameObject)
            {
                _lastTile.GetComponent<TileInteraction>().NotEventHighlight();
                _lastTile = _resultsTile[0].collider.gameObject;
                _resultsTile[0].collider.GetComponent<TileInteraction>().Highlight();

            }
        }

        //selection de batiments
        RaycastHit[] _results = new RaycastHit[1];
        int _hits = Physics.RaycastNonAlloc(_ray, _results);









        if (_results[0].collider != null)
        {
            //Debug.Log(_results[0].collider.name);

            //_planeLD = new Plane(Vector3.down,_results[0].collider.transform.position.y + GameManager._instance._pointHeight);
        }

    }

}
