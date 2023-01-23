using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    LineRenderer _lineRenderer;
    [HideInInspector] public Vector2 _startPosition;
    public bool _isEnd;
    bool _destruction;
    bool _destrucionBeginAtStart;
    float _duration;
    [SerializeField] float _delayBeforeReset;

    [Space]
    [Header("Points")]
    public Point _startPoint;
    public Point _endPoint;

    [Space]
    [Header("Cut Connxion")]
    [SerializeField] Transform _connextionCutPoint;
    [SerializeField] Transform _connextionCutEndPoint;
    Vector3 _velocity = Vector3.zero;
    [SerializeField] float _cutSpeed;


    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, new Vector3(_startPosition.x,.3f,_startPosition.y));
    }

    void Update()
    {        


        if (!_isEnd && !_destruction)
            _lineRenderer.SetPosition(1,new Vector3(GameManager._instance._selectionConnexionCoordonnees.x,.3f, GameManager._instance._selectionConnexionCoordonnees.y));
        //is end
        else if (_isEnd && !_destruction)
        {
            //renvoie l'id (int) du point séléctionné. Si aucun point , renvoie -1
            int _idPoint = GameManager._instance.IdPointMousePosition();

            if (_duration > _delayBeforeReset && (_startPoint == GameManager._instance._allPoints[_idPoint] || _endPoint == GameManager._instance._allPoints[_idPoint]))
            {
                //Debug.Log(GameManager._instance._allPoints[_idPoint]._coordonnees);
                ConnexionCut(GameManager._instance._allPoints[_idPoint]);
                _destruction = true;
                StartCoroutine(LastActionBeforeDestruction(_cutSpeed));
            }

            if (Input.GetMouseButton(0) && _idPoint >= 0)
                _duration += Time.deltaTime;

            if (Input.GetMouseButtonUp(0) || _idPoint == -1)
                _duration = 0;
        }
        else if (_destruction)
        {
            if(_destrucionBeginAtStart)
                _lineRenderer.SetPosition(0, new Vector3(_connextionCutPoint.position.x, .3f, _connextionCutPoint.position.z));
            else
                _lineRenderer.SetPosition(1, new Vector3(_connextionCutPoint.position.x, .3f, _connextionCutPoint.position.z));

            _connextionCutPoint.position = Vector3.SmoothDamp(_connextionCutPoint.position, _connextionCutEndPoint.position, ref _velocity, _cutSpeed);

            
        }



        if (Input.GetMouseButtonUp(0) && !_isEnd)
        {
            //renvoie l'id (int) du point séléctionné. Si aucun point , renvoie -1
            int _idPoint = GameManager._instance.IdPointLocalisation();
            Debug.Log(_idPoint);

            //Debug.Log(_idPoint);
            if (_idPoint >= 0)
            {
                _isEnd = true;
                _lineRenderer.SetPosition(1,new Vector3(GameManager._instance._allPoints[_idPoint]._coordonnees.x, .3f, GameManager._instance._allPoints[_idPoint]._coordonnees.y));
                _endPoint = GameManager._instance._allPoints[_idPoint];

                _startPoint._connecte = true;
                _endPoint._connecte = true;

            }
            else
                Destroy(gameObject);
        }


    }

    void ConnexionCut(Point _point)
    {
        _point._connecte = false;
        //Debug.Log(_startPoint == _point);
        //verifions si le point est le premier ou le dernier
        if (!_startPoint._connecte)
        {
            //cad que la connexion se coupe par le debut
            _destrucionBeginAtStart = true;
            _connextionCutPoint.position = new Vector3(_startPoint._coordonnees.x, .3f, _startPoint._coordonnees.y);
            _connextionCutEndPoint.position = new Vector3(_endPoint._coordonnees.x, .3f, _endPoint._coordonnees.y);
        }
        else
        {
            _connextionCutPoint.position = new Vector3(_endPoint._coordonnees.x, .3f, _endPoint._coordonnees.y);
            _connextionCutEndPoint.position = new Vector3(_startPoint._coordonnees.x, .3f, _startPoint._coordonnees.y);
        }
    }
    IEnumerator LastActionBeforeDestruction(float _delay)
    {
        yield return new WaitForSeconds(_delay * 2.5f);
        _startPoint._connecte = false;
        _endPoint._connecte = false;
        Destroy(gameObject);
    }

}
