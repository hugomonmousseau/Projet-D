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

    [Header("Line")]
    [SerializeField] GameObject _line;
    List<GameObject> _usingTiles = new List<GameObject>();
    List<GameObject> _usedTiles = new List<GameObject>();
    List<GameObject> _waitingTiles = new List<GameObject>();
    public List<GameObject> _actualsLines = new List<GameObject>();
    public List<TilesGroupe> _groupes = new List<TilesGroupe>();

    void Start()
    {
        if (GameManager._instance._host == null)
            GameManager._instance._host = gameObject;
        else
            GameManager._instance._client = gameObject;
    }

    void Update()
    {
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

    [ContextMenu("SpawnLinePrev")]
    public void SpawnLinePrevisualisation()
    {
        DefineTilesGroupes();
    }

    private void DefineTilesGroupes()
    {
        //reset
        
        _usingTiles = new List<GameObject>();
        _usedTiles = new List<GameObject>();
        _waitingTiles = new List<GameObject>();
        _actualsLines = new List<GameObject>();
        _groupes = new List<TilesGroupe>();
        
        foreach(GameObject _tile in GameManager._instance._tiles)
        {
            //si on l utilise pas deja et que la tuile est vide et qu elles sont dans notre équipe
            if(_tile.GetComponent<TileID>()._tile._isEmpty && !_usingTiles.Contains(_tile) && _tile.GetComponent<TileInteraction>()._camp == _camp)
            {
                //on le rajoute
                _usingTiles.Add(_tile);
            }
        }
        //on prend les tuiles qui seront utilisées une par une
        foreach(GameObject _usingTile in _usingTiles)
        {
            //si la tuile utilisable n ai pas deja utilisée
            if (!_usedTiles.Contains(_usingTile))
            {

                TilesGroupe _actualGroup = new TilesGroupe();

                TileChecker(_usingTile, _actualGroup);

                while (_waitingTiles.Count > 0)
                {
                    TileChecker(_waitingTiles[0], _actualGroup);
                    _waitingTiles.Remove(_waitingTiles[0]);
                }
                _groupes.Add(_actualGroup);

            }
        }

    }

    private void TileChecker(GameObject _tile, TilesGroupe _actualGroup)
    {
        for(int _checker = 0; _checker < _tile.GetComponentInChildren<TileChecker>()._checkers.Count; _checker++)
        {
            //si l objet est nouveau
            if(_tile.GetComponentInChildren<TileChecker>().Checker(_checker) != null && !_waitingTiles.Contains(_tile.GetComponentInChildren<TileChecker>().Checker(_checker)) && _tile.GetComponentInChildren<TileChecker>().Checker(_checker).GetComponent<TileID>()._tile._isEmpty && !_usedTiles.Contains(_tile.GetComponentInChildren<TileChecker>().Checker(_checker)))
            {
                _waitingTiles.Add(_tile.GetComponentInChildren<TileChecker>().Checker(_checker));
            }
        }

        _usedTiles.Add(_tile);
        _actualGroup._tiles.Add(_tile);
    }
}
