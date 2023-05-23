using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;
    public GameState _gameState;
    public List<Point> _allPoints;
    public List<GameObject> _allPointsGO;
    public int _numberOfLine;
    public List<LigneCo> _allLines;
    public List<GameObject> _allLinesGO;
    [Space]
    [Header("Connexion")]
    //connexion
    public Vector2 _selectionConnexionCoordonnees;
    public Vector2 _startSelectionConnexionCoordonnees;
    public Vector2 _actualSelectionConnexionCoordonnees;
    [Space]
    [Header("World")]
    //world
    public Vector2 _selectionWorldCoordonnees;
    public Vector2 _startSelectionWorldCoordonnees;

    [Space]
    [Header("Prefabs")]
    [SerializeField] GameObject _lineConnexionFromDice;
    [SerializeField] GameObject _lineConnexionFromBat;
    [SerializeField] GameObject _lineConnexionFromTower;
    [SerializeField] GameObject _lineConnexionForTower;
    List<GameObject> _connexionList = new List<GameObject>();
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

    
    private void Awake()
    {
        _instance = this;

    }
    void Start()
    {
        _connexionList.Add(_lineConnexionFromDice);
        _connexionList.Add(_lineConnexionFromBat);
        _connexionList.Add(_lineConnexionFromTower);
        _connexionList.Add(_lineConnexionForTower);
    }

    void Update()
    {

    }

    public void NewConnexion()
    {
        for(int _loop = 0; _loop < _allPoints.Count; _loop++)
        {
            //Debug.Log(_loop);
            //si tu touches un point pas connecté
            if(_allPoints[_loop]._coordonnees.x > _startSelectionConnexionCoordonnees.x -_taillePoint.x && _allPoints[_loop]._coordonnees.x < _startSelectionConnexionCoordonnees.x + _taillePoint.x && _allPoints[_loop]._coordonnees.y > _startSelectionConnexionCoordonnees.y - _taillePoint.y && _allPoints[_loop]._coordonnees.y < _startSelectionConnexionCoordonnees.y + _taillePoint.y && !_allPoints[_loop]._connecte)
            {

                int _connexionID = 0;
                if (_allPoints[_loop]._type == Type.De)
                    _connexionID = 1;
                else if (_allPoints[_loop]._type == Type.DePourBatiment || _allPoints[_loop]._type == Type.DePourTourelle)
                    _connexionID = 2;
                else if (_allPoints[_loop]._type == Type.Tourelle)
                    _connexionID = 3;
                else if (_allPoints[_loop]._type == Type.BatimentsPourTourelle)
                    _connexionID = 4;



                GameObject _newLine = Instantiate(_connexionList[_connexionID - 1]);
                _newLine.GetComponent<Line>()._startPosition = _allPoints[_loop]._coordonnees;
                _newLine.GetComponent<Line>()._startPoint = _allPoints[_loop];
                /*
                if (_allPoints[_loop]._type == Type.Tourelle)
                    _newLine.GetComponent<LineRenderer>().material = _newLine.GetComponent<Line>()._batMat;
                else if(_allPoints[_loop]._type == Type.De)
                    _newLine.GetComponent<LineRenderer>().material = _newLine.GetComponent<Line>()._diceMat;
                else if(_allPoints[_loop]._type == Type.BatimentsPourTourelle)
                    _newLine.GetComponent<LineRenderer>().material = _newLine.GetComponent<Line>()._diceMat;
                */
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

        _lastSelection = _batiment;
        _lastSelection.GetComponent<OnSelectedBatiment>().ImSelected();
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

    //void appelée apres avoir tiré la ligne
    public void PrevisualisationPointAfterLine()
    {
        for(int _loop = 0; _loop < _visiblesPointsDuringLine.Count; _loop++)
        {
            //si le point n°x N est PAS présent dans la liste des points visibles par séléction
            if (! _visiblesPointsSelected.Contains(_visiblesPointsDuringLine[_loop]))
            {
                //Debug.Log("je dois faire disparaitre le point : " + _visiblesPointsDuringLine[_loop]);
                _allPointsGO[_visiblesPointsDuringLine[_loop]].GetComponent<PointID>().OnePointDisappear();
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
            if(!_visiblesPointsSelected.Contains( GameManager._instance._allLines[_loop]._pointA._intID) && !_visiblesPointsSelected.Contains(GameManager._instance._allLines[_loop]._pointB._intID))
            {
                //si elle n est pas deja cachée
                if (_allLines[_loop]._visible)
                {
                    _allLinesGO[_loop].GetComponent<LineAnim>().LineDisappear();
                    _allLines[_loop]._visible = false;
                    Debug.Log("hello");
                }
            }
            else if (!_allLines[_loop]._visible)
            {
                _allLinesGO[_loop].GetComponent<LineAnim>().LineAppear();
                _allLines[_loop]._visible = true;
                Debug.Log("good bye");

            }
            Debug.Log("loop numero " + _loop + " visibility : " + _allLines[_loop]._visible);
        }
    }
    public bool BonneCombinaison(Point _pointA, Point _pointB)
    {
        //dé et dé pour bat
        if ((_pointA._type == Type.De || _pointB._type == Type.De) && (_pointA._type == Type.DePourBatiment || _pointB._type == Type.DePourBatiment))
            return true;

        //dé et tourelle
        if ((_pointA._type == Type.De || _pointB._type == Type.De) && (_pointA._type == Type.DePourTourelle || _pointB._type == Type.DePourTourelle))
            return true;

        //bat et tourelle
        if ((_pointA._type == Type.Tourelle || _pointB._type == Type.Tourelle) && (_pointA._type == Type.BatimentsPourTourelle || _pointB._type == Type.BatimentsPourTourelle))
            return true;
        return false;

    }
}

public enum GameState
{
    InGame,
    Pause,
    IsBuying,
}


