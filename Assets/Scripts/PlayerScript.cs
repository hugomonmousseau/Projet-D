using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("info")]
    public Camp _camp;
    public GameState _state;
    public GameObject _inHand;
    public int _golds;

    [Header("things")]
    public GameObject _actualPrev;
    private Vector3 _velocity = Vector3.zero;
    [SerializeField] float _previsualisationSpeed = .15f;

    [Header("Selector")]
    public GameObject _hexagon;

    void Start()
    {
        if (GameManager._instance._host == null)
            GameManager._instance._host = gameObject;
        else
            GameManager._instance._client = gameObject;
    }

    void Update()
    {
        HexSelectionManager();

        if (_state == GameState.IsBuying)
            Shopping();
    }

    public void NowShopping(GameObject _go)
    {
        _state = GameState.IsBuying;
        _inHand = _go;
    }

    public void NotShoppingAnymore()
    {
        _state = GameState.Default;
        if(_actualPrev != null)
        {
            if (GetComponentInChildren<CameraClick>()._actualTile == null)
            {
                _actualPrev.transform.position = GetComponentInChildren<CameraClick>()._lastTile.transform.position;
                GetComponentInChildren<CameraClick>()._lastTile.GetComponent<TileID>()._tile._isEmpty = false;
                _actualPrev.GetComponent<Previsualisations>().Deathrattle(GetComponentInChildren<CameraClick>()._lastTile.GetComponent<TileID>()._type);
            }
            else
            {
                _actualPrev.transform.position = GetComponentInChildren<CameraClick>()._actualTile.transform.position;
                GetComponentInChildren<CameraClick>()._actualTile.GetComponent<TileID>()._tile._isEmpty = false;
                _actualPrev.GetComponent<Previsualisations>().Deathrattle(GetComponentInChildren<CameraClick>()._actualTile.GetComponent<TileID>()._type);
            }
            _actualPrev = null;

        }

    }

    public void Shopping()
    {

        if(GetComponentInChildren<CameraClick>()._actualTile != null)
        {
            if (_actualPrev == null)
                _actualPrev = Instantiate(_inHand, GetComponentInChildren<CameraClick>()._actualTile.transform.position, Quaternion.Euler(0,60 * Random.Range(0,6),0));
            else
                _actualPrev.transform.position = Vector3.SmoothDamp(_actualPrev.transform.position, GetComponentInChildren<CameraClick>()._actualTile.transform.position, ref _velocity, _previsualisationSpeed);
        }
        if(_actualPrev != null && GetComponentInChildren<CameraClick>()._actualTile != null)
        {
            if (_actualPrev.GetComponent<Previsualisations>()._wrong && GetComponentInChildren<CameraClick>()._actualTile.GetComponent<TileID>()._tile._isEmpty && ((_actualPrev.GetComponent<Previsualisations>()._bat.GetComponent<BuildingManager>()._building == Building.PirateArtisanat && GetComponentInChildren<CameraClick>()._actualTile.GetComponent<TileID>()._isNextToWater)|| _actualPrev.GetComponent<Previsualisations>()._bat.GetComponent<BuildingManager>()._building != Building.PirateArtisanat))
                _actualPrev.GetComponent<Previsualisations>().NowPrev();
            else if (!_actualPrev.GetComponent<Previsualisations>()._wrong && (!GetComponentInChildren<CameraClick>()._actualTile.GetComponent<TileID>()._tile._isEmpty || (_actualPrev.GetComponent<Previsualisations>()._bat.GetComponent<BuildingManager>()._building == Building.PirateArtisanat && !GetComponentInChildren<CameraClick>()._actualTile.GetComponent<TileID>()._isNextToWater)))
                _actualPrev.GetComponent<Previsualisations>().NowWrongPrev();

        }

        if (Input.GetMouseButtonUp(0))
            NotShoppingAnymore();
    }
    
    private void HexSelectionManager()
    {
        if(GetComponentInChildren<CameraClick>()._actualTile != null)
        {
            if (!_hexagon.activeInHierarchy)
            {
                _hexagon.SetActive(true);
                Debug.Log(_hexagon.name);
            }
            Debug.Log(_hexagon.transform.position + " " + _hexagon.activeInHierarchy);
            _hexagon.transform.position = GetComponentInChildren<CameraClick>()._actualTile.transform.position;
        }
        else if (_hexagon.activeInHierarchy)
        {
            _hexagon.SetActive(false);
        }
    }
}
