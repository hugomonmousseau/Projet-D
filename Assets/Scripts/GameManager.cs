using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;
    public GameState _gameState;
    public List<Point> _allPoints;
    public List<GameObject> _allPointsGO;
    public List<LigneCo> _allLines;
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
    [SerializeField] GameObject _diceLineConnexion;
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
                GameObject _newLine = Instantiate(_diceLineConnexion);
                _newLine.GetComponent<Line>()._startPosition = _allPoints[_loop]._coordonnees;
                _newLine.GetComponent<Line>()._startPoint = _allPoints[_loop];
                if (_allPoints[_loop]._type == Type.Batiment)
                    _newLine.GetComponent<LineRenderer>().material = _newLine.GetComponent<Line>()._batMat;
                else
                    _newLine.GetComponent<LineRenderer>().material = _newLine.GetComponent<Line>()._diceMat;

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
            if(_point._type == _allPoints[_loop]._type)
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
}

public enum GameState
{
    InGame,
    Pause,
    IsBuying,
}


