using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LigneCo _line;
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

    [Space]
    [Header("Materials")]
    public Material _diceMat;

    [Space]
    [Header("Informations")]
    public GameObject _GOPointA;
    public GameObject _GOPointB;

    //public Material _batMat;
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, new Vector3(_startPosition.x, GameManager._instance._pointHeight, _startPosition.y));
        _lineRenderer.SetPosition(1, new Vector3(_startPosition.x, GameManager._instance._pointHeight, _startPosition.y));
        GameManager._instance.PrevisualisationPointDuringLine(_startPoint);

        
    }

    void Update()
    {        


        if (!_isEnd && !_destruction)
            _lineRenderer.SetPosition(1,new Vector3(GameManager._instance._selectionConnexionCoordonnees.x, GameManager._instance._pointHeight, GameManager._instance._selectionConnexionCoordonnees.y));
        //is end
        else if (_isEnd && !_destruction)
        {
            //renvoie l'id (int) du point séléctionné. Si aucun point , renvoie -1
            int _idPoint = GameManager._instance.IdPointMousePosition();
            if(_idPoint >= 0)
            {
                if (_duration > _delayBeforeReset && (_startPoint == GameManager._instance._allPoints[_idPoint] || _endPoint == GameManager._instance._allPoints[_idPoint]))
                {
                    //Debug.Log(GameManager._instance._allPoints[_idPoint]._coordonnees);
                    ConnexionCut(GameManager._instance._allPoints[_idPoint]);
                    _destruction = true;
                    ColorDeconnexion();
                    StartCoroutine(LastActionBeforeDestruction(_cutSpeed));
                }
            }


            if (Input.GetMouseButton(0) && _idPoint >= 0)
                _duration += Time.deltaTime;

            if (Input.GetMouseButtonUp(0) || _idPoint == -1)
                _duration = 0;
        }
        else if (_destruction)
        {
            if(_destrucionBeginAtStart)
                _lineRenderer.SetPosition(0, new Vector3(_connextionCutPoint.position.x, GameManager._instance._pointHeight, _connextionCutPoint.position.z));
            else
                _lineRenderer.SetPosition(1, new Vector3(_connextionCutPoint.position.x, GameManager._instance._pointHeight, _connextionCutPoint.position.z));

            _connextionCutPoint.position = Vector3.SmoothDamp(_connextionCutPoint.position, _connextionCutEndPoint.position, ref _velocity, _cutSpeed);

            
        }
        if (Input.GetMouseButtonUp(0))
        {
            //on permet la creation d'une nouvelle ligne
            GameManager._instance._alreadyALine = false;
        }

        //ici on instance
        if (Input.GetMouseButtonUp(0) && !_isEnd)
        {
            //renvoie l'id (int) du point séléctionné. Si aucun point , renvoie -1
            int _idPoint = GameManager._instance.IdPointLocalisation();






            //Debug.Log(_idPoint);
            //if (_idPoint >= 0 &&(GameManager._instance._allPoints[_idPoint]._type == _startPoint._type) && (GameManager._instance._allPoints[_idPoint]._intID != _startPoint._intID))

            //on test si on peut connecter les bons points
            if(_idPoint != -1 && GameManager._instance.BonneCombinaison(GameManager._instance._allPoints[_idPoint], _startPoint) && (GameManager._instance._allPoints[_idPoint]._intID != _startPoint._intID))
            {
                _isEnd = true;
                _lineRenderer.SetPosition(1,new Vector3(GameManager._instance._allPoints[_idPoint]._coordonnees.x, GameManager._instance._pointHeight, GameManager._instance._allPoints[_idPoint]._coordonnees.y));
                _endPoint = GameManager._instance._allPoints[_idPoint];

                _startPoint._connecte = true;
                _endPoint._connecte = true;

                //ajout dans le gamemanager
                
                _line._pointA = _startPoint;
                _line._pointB = _endPoint;
                _line._intID = GameManager._instance._numberOfLine++;
                _line._visible = true;

                //on rajoute les lignes au GM
                GameManager._instance._allLines.Add(_line);
                GameManager._instance._allLinesGO.Add(gameObject);

                //on rajoute les nouveaux points a la previsualisation
                GameManager._instance._visiblesPointsSelected.Add(_endPoint._intID);



                //on fini par ca !!

                GameManager._instance.HideAllPointsExceptSelected();

                GameManager._instance.ShowLinesNeeded();
                //GameManager._instance.PrevisualisationPointAfterLine();

                //on rajoute la batB
                _GOPointB = GameManager._instance._allPointsGO[_idPoint].GetComponent<PointToBat>()._bat;
                ColorNewConnexion();

            }
            else
            {

                //Si on fini pas la ligne
                GameManager._instance.HideAllPointsExceptSelected();

                GameManager._instance.ShowLinesNeeded();
                //GameManager._instance.PrevisualisationPointAfterLine();
                Destroy(gameObject);
            }
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
            _connextionCutPoint.position = new Vector3(_startPoint._coordonnees.x, GameManager._instance._pointHeight, _startPoint._coordonnees.y);
            _connextionCutEndPoint.position = new Vector3(_endPoint._coordonnees.x, GameManager._instance._pointHeight, _endPoint._coordonnees.y);
        }
        else
        {
            _connextionCutPoint.position = new Vector3(_endPoint._coordonnees.x, GameManager._instance._pointHeight, _endPoint._coordonnees.y);
            _connextionCutEndPoint.position = new Vector3(_startPoint._coordonnees.x, GameManager._instance._pointHeight, _startPoint._coordonnees.y);
        }
    }
    IEnumerator LastActionBeforeDestruction(float _delay)
    {
        yield return new WaitForSeconds(_delay * 2.5f);
        _startPoint._connecte = false;
        _endPoint._connecte = false;

        //on tente de virer la ligne de la liste
        GameManager._instance._allLines.Remove(_line);
        GameManager._instance._allLinesGO.Remove(gameObject);
        //GameManager._instance._allLines[_line._intID]._isActive = false;

        //on retire le point qui s'isole

        //pour cela, on refait une liste
        GameManager._instance._visiblesPointsSelected = new List<int>();

        //on rajoute tt points liés
        GameManager._instance._lastSelection.GetComponent<OnSelectedBatiment>().ImSelected();

        //on cache le reste
        GameManager._instance.HideAllPointsExceptSelected();
        Destroy(gameObject);
    }

    public void ColorNewConnexion()
    {
        //test point a puis b
        if (_GOPointA.GetComponent<BatimentManager>()._type == Batiment.Tourelle || _GOPointA.GetComponent<BatimentManager>()._type == Batiment.Campement)
        {
            //si c est bon on transmet l info au b
            _GOPointB.GetComponent<Colorisation>()._color = _GOPointA.GetComponent<Colorisation>()._color;
            _GOPointB.GetComponent<Colorisation>().NewColor();



            if (_GOPointB.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._connecte)
            {
                for (int _loop = 0; _loop < GameManager._instance._allLines.Count; _loop++)
                {
                    if (GameManager._instance._allLines[_loop]._pointA._intID == _GOPointB.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._intID)
                    {
                        GameManager._instance._allPointsGO[GameManager._instance._allLines[_loop]._pointB._intID].GetComponent<PointToBat>()._bat.GetComponent<Colorisation>()._color = _GOPointA.GetComponent<Colorisation>()._color;
                        GameManager._instance._allPointsGO[GameManager._instance._allLines[_loop]._pointB._intID].GetComponent<PointToBat>()._bat.GetComponent<Colorisation>().NewColor();
                    }
                    if (GameManager._instance._allLines[_loop]._pointB._intID == _GOPointB.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._intID)
                    {
                        GameManager._instance._allPointsGO[GameManager._instance._allLines[_loop]._pointA._intID].GetComponent<PointToBat>()._bat.GetComponent<Colorisation>()._color = _GOPointA.GetComponent<Colorisation>()._color;
                        GameManager._instance._allPointsGO[GameManager._instance._allLines[_loop]._pointA._intID].GetComponent<PointToBat>()._bat.GetComponent<Colorisation>().NewColor();
                    }
                }
            }

        }

        else if (_GOPointB.GetComponent<BatimentManager>()._type == Batiment.Tourelle || _GOPointB.GetComponent<BatimentManager>()._type == Batiment.Campement)
        {
            //si c est bon on transmet l info au b
            _GOPointA.GetComponent<Colorisation>()._color = _GOPointB.GetComponent<Colorisation>()._color;
            _GOPointA.GetComponent<Colorisation>().NewColor();

            if (_GOPointA.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._connecte)
            {
                for (int _loop = 0; _loop < GameManager._instance._allLines.Count; _loop++)
                {
                    if(GameManager._instance._allLines[_loop]._pointA._intID == _GOPointA.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._intID)
                    {
                        GameManager._instance._allPointsGO[GameManager._instance._allLines[_loop]._pointB._intID].GetComponent<PointToBat>()._bat.GetComponent<Colorisation>()._color = _GOPointB.GetComponent<Colorisation>()._color;
                        GameManager._instance._allPointsGO[GameManager._instance._allLines[_loop]._pointB._intID].GetComponent<PointToBat>()._bat.GetComponent<Colorisation>().NewColor();
                    }
                    if (GameManager._instance._allLines[_loop]._pointB._intID == _GOPointA.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._intID)
                    {
                        GameManager._instance._allPointsGO[GameManager._instance._allLines[_loop]._pointA._intID].GetComponent<PointToBat>()._bat.GetComponent<Colorisation>()._color = _GOPointB.GetComponent<Colorisation>()._color;
                        GameManager._instance._allPointsGO[GameManager._instance._allLines[_loop]._pointA._intID].GetComponent<PointToBat>()._bat.GetComponent<Colorisation>().NewColor();
                    }
                }
                
            
            }
        }
        else
        {
            if (_GOPointA.GetComponent<BatimentManager>()._type == Batiment.Dé)
            {
                _GOPointA.GetComponent<Colorisation>()._color = _GOPointB.GetComponent<Colorisation>()._color;
                _GOPointA.GetComponent<Colorisation>().NewColor();

            }
            else
            {
                _GOPointB.GetComponent<Colorisation>()._color = _GOPointA.GetComponent<Colorisation>()._color;
                _GOPointB.GetComponent<Colorisation>().NewColor();
            }
        }
    }

    public void ColorDeconnexion()
    {
        //test point a puis b
        if (_GOPointA.GetComponent<BatimentManager>()._type == Batiment.Tourelle || _GOPointA.GetComponent<BatimentManager>()._type == Batiment.Campement)
        {
            //si c est bon on transmet l info au b
            _GOPointB.GetComponent<Colorisation>().Decolor();
            _GOPointB.GetComponent<Colorisation>()._color = Colors.None;

            if (_GOPointB.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._connecte)
            {
                for (int _loop = 0; _loop < GameManager._instance._allLines.Count; _loop++)
                {
                    if (GameManager._instance._allLines[_loop]._pointA._intID == _GOPointB.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._intID)
                    {
                        GameManager._instance._allPointsGO[GameManager._instance._allLines[_loop]._pointB._intID].GetComponent<PointToBat>()._bat.GetComponent<Colorisation>().Decolor();
                        _GOPointB.GetComponent<Colorisation>()._color = Colors.None;
                    }
                    if (GameManager._instance._allLines[_loop]._pointB._intID == _GOPointB.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._intID)
                    {
                        GameManager._instance._allPointsGO[GameManager._instance._allLines[_loop]._pointA._intID].GetComponent<PointToBat>()._bat.GetComponent<Colorisation>().Decolor();
                        _GOPointB.GetComponent<Colorisation>()._color = Colors.None;
                    }
                }
            }

        }

        else if (_GOPointB.GetComponent<BatimentManager>()._type == Batiment.Tourelle || _GOPointB.GetComponent<BatimentManager>()._type == Batiment.Campement)
        {
            //si c est bon on transmet l info au b
            _GOPointA.GetComponent<Colorisation>().Decolor();
            _GOPointA.GetComponent<Colorisation>()._color = Colors.None;

            if (_GOPointA.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._connecte)
            {
                for (int _loop = 0; _loop < GameManager._instance._allLines.Count; _loop++)
                {
                    if (GameManager._instance._allLines[_loop]._pointA._intID == _GOPointA.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._intID)
                    {
                        GameManager._instance._allPointsGO[GameManager._instance._allLines[_loop]._pointB._intID].GetComponent<PointToBat>()._bat.GetComponent<Colorisation>().Decolor();
                        _GOPointA.GetComponent<Colorisation>()._color = Colors.None;
                    }
                    if (GameManager._instance._allLines[_loop]._pointB._intID == _GOPointA.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._intID)
                    {
                        GameManager._instance._allPointsGO[GameManager._instance._allLines[_loop]._pointA._intID].GetComponent<PointToBat>()._bat.GetComponent<Colorisation>().Decolor();
                        _GOPointA.GetComponent<Colorisation>()._color = Colors.None;
                    }
                }


            }
        }//sinon c est entre un dé et un bat
        else
        {
            if (_GOPointA.GetComponent<BatimentManager>()._type == Batiment.Dé)
            {
                _GOPointA.GetComponent<Colorisation>()._color = Colors.None;
                _GOPointA.GetComponent<Colorisation>().Decolor();
            }

            else
            {
                _GOPointB.GetComponent<Colorisation>()._color = Colors.None;
                _GOPointB.GetComponent<Colorisation>().Decolor();

            }
        }
    }
}
