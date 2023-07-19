using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;
    public GameState _gameState = GameState.Default;
    public List<Point> _allPoints;
    public List<GameObject> _allPointsGO;
    public int _numberOfLine;
    public List<LigneCo> _allLines;
    public List<GameObject> _allLinesGO;
    [HideInInspector] public bool _alreadyALine;
    [Space]
    [Header("Connexion")]
    //connexion
    public Vector2 _selectionConnexionCoordonnees;
    public Vector2 _startSelectionConnexionCoordonnees;
    public Vector2 _actualSelectionConnexionCoordonnees;
    public List<MainBatimentsGOList> _connexionList;
    public List<BatimentsGOList> _waitingCoBatiments;
    public BatimentsGOList _tempSimpleBat;
    public MainBatimentsGOList _tempMainBat;
    [Space]
    [Header("World")]
    //world
    public Vector2 _selectionWorldCoordonnees;
    public Vector2 _startSelectionWorldCoordonnees;
    public float _pointHeight;


    [Space]
    [Header("Prefabs")]
    //[SerializeField] GameObject _lineConnexionFromDice;
    //[SerializeField] GameObject _lineConnexionFromBat;
    //[SerializeField] GameObject _lineConnexionFromTower;
    //[SerializeField] GameObject _lineConnexionForTower;
    //List<GameObject> _lineList = new List<GameObject>();
    [SerializeField] GameObject _line;
    [SerializeField] Vector2 _taillePoint = new Vector2(.25f,.25f);

    [Space]
    [Header("Ressources")]
    public int _or;
    public int _magie;
    public int _sciences;

    [Space]
    [Header("Inventory")]
    public List<Inventaire> _inventaire;
    public GameObject _inHand;
    public GameObject _lastSelection;

    [Space]
    [Header("Prévisualtisations")]
    public List<int> _visiblesPointsDuringLine;
    public List<int> _visiblesPointsSelected;
    public GameObject _hexagonSelection;
    [HideInInspector] public Tile _tileWeAreLooking;
    public Vector3 _previsualisationPosition = new Vector3(-100,0,-100);
    
    private void Awake()
    {
        _instance = this;

    }
    void Start()
    {
        /*
        _lineList.Add(_lineConnexionFromDice);
        _lineList.Add(_lineConnexionFromBat);
        _lineList.Add(_lineConnexionFromTower);
        _lineList.Add(_lineConnexionForTower);
        */

        _hexagonSelection = Instantiate(_hexagonSelection);
    }

    void Update()
    {
        ShowLinesNeeded();
    }

    public void NewConnexion()
    {
        for(int _loop = 0; _loop < _allPoints.Count; _loop++)
        {
            //Debug.Log(_loop);
            //si tu touches un point pas connecté
            if(!_alreadyALine && _allPoints[_loop]._state == PointState.Visible && (_allPoints[_loop]._coordonnees.x > _startSelectionConnexionCoordonnees.x -_taillePoint.x && _allPoints[_loop]._coordonnees.x < _startSelectionConnexionCoordonnees.x + _taillePoint.x && _allPoints[_loop]._coordonnees.y > _startSelectionConnexionCoordonnees.y - _taillePoint.y && _allPoints[_loop]._coordonnees.y < _startSelectionConnexionCoordonnees.y + _taillePoint.y && (!_allPoints[_loop]._connecte || _allPoints[_loop]._type == Type.Tourelle)))
            {

                GameObject _newLine = Instantiate(_line);
                _newLine.GetComponent<Line>()._startPosition = _allPoints[_loop]._coordonnees;
                _newLine.GetComponent<Line>()._startPoint = _allPoints[_loop];

                _newLine.GetComponent<Line>()._GOPointA = _allPointsGO[_loop].GetComponent<PointToBat>()._bat;

                _alreadyALine = true;
                //Debug.Log(_allPointsGO[_loop].GetComponent<ColorPointChanger>()._colorName);
                _newLine.GetComponent<LineColorisation>().StartColor(_allPointsGO[_loop].GetComponent<ColorPointChanger>()._colorName);
            }
        }
    }

    public int IdPointLocalisation()
    {
        for (int _loop = 0; _loop < _allPoints.Count; _loop++)
        {
            if (_allPoints[_loop]._coordonnees.x > _selectionConnexionCoordonnees.x - _taillePoint.x && _allPoints[_loop]._coordonnees.x < _selectionConnexionCoordonnees.x + _taillePoint.x && _allPoints[_loop]._coordonnees.y > _selectionConnexionCoordonnees.y - _taillePoint.y && _allPoints[_loop]._coordonnees.y < _selectionConnexionCoordonnees.y + _taillePoint.y && !_allPoints[_loop]._connecte)
            {
                //Debug.Log(_loop);
                return _loop;
            }
        }
        return -1;
    }

    public int IdPointMousePosition()
    {
        for (int _loop = 0; _loop < _allPoints.Count; _loop++)
        {
            if (_allPoints[_loop]._coordonnees.x > _actualSelectionConnexionCoordonnees.x - _taillePoint.x && _allPoints[_loop]._coordonnees.x < _actualSelectionConnexionCoordonnees.x + _taillePoint.x && _allPoints[_loop]._coordonnees.y > _actualSelectionConnexionCoordonnees.y - _taillePoint.y && _allPoints[_loop]._coordonnees.y < _actualSelectionConnexionCoordonnees.y + _taillePoint.y)
            {
                return _loop;
            }

        }
        return -1;
    }

    public void NewBatimentSelection(GameObject _batiment)
    {
        if(_batiment != _lastSelection)
        {
            _lastSelection = _batiment;
            _lastSelection.GetComponent<OnSelectedBatiment>().ImSelected();
        }
    }

    //void appelée pour afficher les points pendant qu on trace une ligne
    public void PrevisualisationPointDuringLine(Point _point)
    {
        _visiblesPointsDuringLine = new List<int>();
        for (int _loop = 0; _loop < _allPoints.Count; _loop++)
        {
            if(BonneCombinaison( _point, _allPoints[_loop]) && !_allPoints[_loop]._connecte)
            {
                _visiblesPointsDuringLine.Add(_allPoints[_loop]._intID);
                _allPointsGO[_loop].GetComponent<PointID>().OnePointAppear();
            }
        }
    }

    


    public void HideAllPointsExceptSelected()
    {
        for(int _loop = 0; _loop < _allPoints.Count; _loop++)
        {
            if (!_visiblesPointsSelected.Contains(_allPoints[_loop]._intID))
            {
                _allPointsGO[_loop].GetComponent<PointID>().OnePointDisappear();
            }
            //_allPointsGO[_visiblesPointsSelected[_loop]].GetComponent<PointID>().OnePointDisappear();
        }
    }


    public void ShowLinesNeeded()
    {

        
        for(int _loop = 0; _loop < _allLines.Count; _loop++)
        {
            //si la ligne n a pas de point visible
            if(!_visiblesPointsSelected.Contains(_allLines[_loop]._pointA._intID) && !_visiblesPointsSelected.Contains(_allLines[_loop]._pointB._intID))
            {
                //si elle n est pas deja cachée
                if (_allLines[_loop]._visible)
                {
                    _allLinesGO[_loop].GetComponent<LineAnim>().LineDisappear();
                    _allLines[_loop]._visible = false;
                    //Debug.Log("hello");
                }
            }
            else if (!_allLines[_loop]._visible)
            {
                _allLinesGO[_loop].GetComponent<LineAnim>().LineAppear();
                _allLines[_loop]._visible = true;
                //Debug.Log("good bye");

            }
            //Debug.Log("loop numero " + _loop + " visibility : " + _allLines[_loop]._visible);
            
        }
        
    }
    public bool BonneCombinaison(Point _pointA, Point _pointB)
    {
        //dé et dé pour bat
        if ((_pointA._type == Type.De || _pointB._type == Type.De) && (_pointA._type == Type.DePourBatiment || _pointB._type == Type.DePourBatiment))
            return true;


        //bat et tourelle
        if ((_pointA._type == Type.Tourelle || _pointB._type == Type.Tourelle) && (_pointA._type == Type.BatimentsPourTourelle || _pointB._type == Type.BatimentsPourTourelle))
            return true;
        return false;

    }

    public void HexagoneSelection(RaycastHit _raycast)
    {
        //_hexagonSelection.transform.position = _raycast.collider.transform.position;
        _hexagonSelection.GetComponent<HexagoneManager>().MoveTo(_raycast.collider.transform.position);
        //Debug.Log("transmission : " + _raycast.collider.transform.position);
    }



    public void WhoIsConnectedTry1(GameObject _mainBat, GameObject _diceBat)
    {

        //tant que on est pas            
        //le dé ne peut pas etre deja connecté

        for (int _loop = 0; _loop < _connexionList.Count; _loop++)
        {

            //si il est deja dans la liste                
            if (_connexionList[_loop]._mainBatiment == _mainBat)
            {
                _connexionList[_loop]._coDice = _diceBat;
            }
        }
    }
    public void WhoIsConnectedTry2(GameObject _mainBat, GameObject _simpleBat)
    {
        //1) soit le bat est dans la waiting list
        //2) soit il ne l est pas
        _tempSimpleBat = new BatimentsGOList();
        _tempSimpleBat._batiment = _simpleBat;
        for (int _loop = 0; _loop < _waitingCoBatiments.Count; _loop++)
        {
            if (_waitingCoBatiments[_loop]._batiment == _simpleBat)
            {
                _tempSimpleBat._coDice = _waitingCoBatiments[_loop]._coDice;

                _waitingCoBatiments.RemoveAt(_loop);

                //on coleur leur ligne aussi
                for(int _line = 0; _line < _allLinesGO.Count; _line++)
                {
                    if(_allLinesGO[_line].GetComponent<Line>()._GOPointA == _simpleBat || _allLinesGO[_line].GetComponent<Line>()._GOPointB == _simpleBat)
                    {
                        _allLinesGO[_line].GetComponent<LineColorisation>().LineNewColor(_simpleBat.GetComponent<Colorisation>()._color);
                    }
                }
            }
        }

        for (int _loop = 0; _loop < _connexionList.Count; _loop++)
        {
            if (_connexionList[_loop]._mainBatiment == _mainBat)
            {
                if(_connexionList[_loop]._coBatiments == null)
                    _connexionList[_loop]._coBatiments = new List<BatimentsGOList>();
                _connexionList[_loop]._coBatiments.Add(_tempSimpleBat);
            }
        }

    }
    public void WhoIsConnectedTry3(GameObject _dice, GameObject _simpleBat)
    {
        //1) soit le bat est lié à un main bat            
        //2) soit il l est pas
        bool _newCoBat = true;

        _tempSimpleBat = new BatimentsGOList();
        for (int _loop = 0; _loop < _connexionList.Count; _loop++)
        {
            if (_connexionList[_loop]._coBatiments != null)
            {
                for (int _group = 0; _group < _connexionList[_loop]._coBatiments.Count; _group++)
                {
                    if (_connexionList[_loop]._coBatiments[_group]._batiment == _simpleBat && _newCoBat)
                    {
                        _newCoBat = !_newCoBat;
                        _connexionList[_loop]._coBatiments[_group]._coDice = _dice;
                    }
                }
            }

        }
        if (_newCoBat)
        {

            //si il n est pas deja présent
            _tempSimpleBat._batiment = _simpleBat;
            _tempSimpleBat._coDice = _dice;
            _waitingCoBatiments.Add(_tempSimpleBat);
        }

    }

}


public enum GameState
{
    Default,
    InGame,
    Pause,
    IsBuying,
    MovingTheCamera,
}



