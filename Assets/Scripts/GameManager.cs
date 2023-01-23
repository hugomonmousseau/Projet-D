using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;
    public List<Point> _allPoints;
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

}


