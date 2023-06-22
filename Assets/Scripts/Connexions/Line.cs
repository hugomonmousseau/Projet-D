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

    [Space]
    [Header("Connexion")]
    public BatimentsGOList _tempSimpleBat;
    public MainBatimentsGOList _tempMainBat;
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
            _lineRenderer.SetPosition(1, new Vector3(GameManager._instance._selectionConnexionCoordonnees.x, GameManager._instance._pointHeight, GameManager._instance._selectionConnexionCoordonnees.y));
        //is end
        else if (_isEnd && !_destruction)
        {
            //renvoie l'id (int) du point séléctionné. Si aucun point , renvoie -1
            int _idPoint = GameManager._instance.IdPointMousePosition();
            if (_idPoint >= 0)
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
            if (_destrucionBeginAtStart)
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
            if (_idPoint != -1 && GameManager._instance.BonneCombinaison(GameManager._instance._allPoints[_idPoint], _startPoint) && (GameManager._instance._allPoints[_idPoint]._intID != _startPoint._intID))
            {
                _isEnd = true;
                _lineRenderer.SetPosition(1, new Vector3(GameManager._instance._allPoints[_idPoint]._coordonnees.x, GameManager._instance._pointHeight, GameManager._instance._allPoints[_idPoint]._coordonnees.y));
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

                //GameManager._instance.ShowLinesNeeded();
                //GameManager._instance.PrevisualisationPointAfterLine();

                //on rajoute la batB
                _GOPointB = GameManager._instance._allPointsGO[_idPoint].GetComponent<PointToBat>()._bat;
                ColorNewConnexion();
                NewConnexion();

                //on doit trouver sa couleur
                Debug.Log(_GOPointA.GetComponent<Colorisation>()._color);
                //aller la chercher dans le manager
                GameObject _ColorManager = GameObject.FindGameObjectWithTag("ColorManager");
                for(int _tier = 0; _tier < _ColorManager.GetComponent<ColorManager>()._tierList.Count; _tier++)
                {
                    for(int _color = 0; _color < _ColorManager.GetComponent<ColorManager>()._tierList[_tier]._tier.Count; _color++)
                    {
                        if (_ColorManager.GetComponent<ColorManager>()._tierList[_tier]._tier[_color]._color == _GOPointA.GetComponent<Colorisation>()._color)
                            GetComponent<LineColorisation>().LineNewColor(_ColorManager.GetComponent<ColorManager>()._tierList[_tier]._tier[_color]._appearance);
                    }
                }
            }
            else
            {

                //Si on fini pas la ligne
                GameManager._instance.HideAllPointsExceptSelected();

                //GameManager._instance.ShowLinesNeeded();
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
        DeleteConnexion();
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
                    if (GameManager._instance._allLines[_loop]._pointA._intID == _GOPointA.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._intID)
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

    public void NewConnexion()
    {
        //plusieurs possibilités:

        //1) on lie un main bat et un dé
        //2) on lie un main bat et un bat
        //3) on lie un bat et un dé

        //1)
        if (_GOPointA.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.MainBat && _GOPointB.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.Dice)
        {
            GameManager._instance.WhoIsConnectedTry1(_GOPointA, _GOPointB);
        }

        else if (_GOPointB.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.MainBat && _GOPointA.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.Dice)
        {
            GameManager._instance.WhoIsConnectedTry1(_GOPointB, _GOPointA);
        }



        //2)
        if (_GOPointA.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.MainBat && _GOPointB.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.SimpleBat)
        {
            GameManager._instance.WhoIsConnectedTry2(_GOPointA, _GOPointB);
        }

        else if (_GOPointB.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.MainBat && _GOPointA.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.SimpleBat)
        {
            GameManager._instance.WhoIsConnectedTry2(_GOPointB, _GOPointA);
        }


        //3)
        if (_GOPointA.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.Dice && _GOPointB.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.SimpleBat)
        {
            GameManager._instance.WhoIsConnectedTry3(_GOPointA, _GOPointB);
        }

        else if (_GOPointB.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.Dice && _GOPointA.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.SimpleBat)
        {
            GameManager._instance.WhoIsConnectedTry3(_GOPointB, _GOPointA);
        } 
    
    }
        /*
        public void NewConnexion()
        {

            bool _newColor = true;
            //on tests tous les groupes
            for(int _loop = 0; _loop < GameManager._instance._connexionList.Count; _loop++)
            {
                //on test chaque element du groupe
                for(int _group = 0; _group < GameManager._instance._connexionList[_loop]._batiments.Count; _group ++)
                {
                    if (GameManager._instance._connexionList[_loop]._batiments.Contains(_GOPointA) && _newColor)
                    {
                        GameManager._instance._connexionList[_loop]._batiments.Add(_GOPointB);
                        _newColor = false;
                    }
                    else if(GameManager._instance._connexionList[_loop]._batiments.Contains(_GOPointB) && _newColor)
                    {
                        GameManager._instance._connexionList[_loop]._batiments.Add(_GOPointA);
                        _newColor = false;
                    }
                }
            }

            if (_newColor)
            {
                _bat._batiments = new List<GameObject>();
                _bat._batiments.Add(_GOPointA);
                _bat._batiments.Add(_GOPointB);
                GameManager._instance._connexionList.Add(_bat);
            }       

        }*/

        
    public void DeleteConnexion()
    {
        //soit un dé et un main bat
        //soit un main bat et un simple bat
        //soit on déco un dé et un simple bat,

        //1) ici on vire le dé de la liste
        if (_GOPointA.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.MainBat && _GOPointB.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.Dice)
        {
            DeleteConnexionCas1(_GOPointA);
        }
        else if (_GOPointB.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.MainBat && _GOPointA.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.Dice)
        {
            DeleteConnexionCas1(_GOPointB);
        }

        //2) ici on vire le simple bat et si il est co à un dé on les met dans la waiting liste 
        if (_GOPointA.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.MainBat && _GOPointB.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.SimpleBat)
        {
            DeleteConnexionCas2(_GOPointA, _GOPointB);
        }
        else if (_GOPointB.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.MainBat && _GOPointA.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.SimpleBat)
        {
            DeleteConnexionCas2(_GOPointB, _GOPointA);
        }

        //3) ici on vire les 2 de la waiting liste si ils y sont sinon on vire le dé de la liste
        if (_GOPointA.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.Dice && _GOPointB.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.SimpleBat)
        {

        }
        else if (_GOPointB.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.Dice && _GOPointA.GetComponent<BatimentManager>()._hierarchy == BatHierarchie.SimpleBat)
        {

        }
    }

    void DeleteConnexionCas1(GameObject _mainBat)
    {
        for(int _loop = 0; _loop < GameManager._instance._connexionList.Count; _loop++)
        {
            if (_mainBat == GameManager._instance._connexionList[_loop]._mainBatiment)
                GameManager._instance._connexionList[_loop]._coDice = null;
        }
    }
    void DeleteConnexionCas2(GameObject _mainBat, GameObject _simpleBat)
    {
        for(int _loop = 0; _loop < GameManager._instance._connexionList.Count; _loop++)
        {
            if (_mainBat == GameManager._instance._connexionList[_loop]._mainBatiment)
            {
                //le simple bat est il connecté
                for(int _group = 0; _group < GameManager._instance._connexionList[_loop]._coBatiments.Count; _group++)
                {
                    if(_simpleBat == GameManager._instance._connexionList[_loop]._coBatiments[_group]._batiment)
                    {
                        if(GameManager._instance._connexionList[_loop]._coBatiments[_group]._coDice != null)
                        {
                            //ici c est si il est co a un dé
                            //et on les rajoute a la waiting list
                            GameManager._instance._waitingCoBatiments.Add(GameManager._instance._connexionList[_loop]._coBatiments[_group]);
                        }

                        GameManager._instance._connexionList[_loop]._coBatiments.Remove(GameManager._instance._connexionList[_loop]._coBatiments[_group]);
                    }
                }
            }
        }
    }
    void DeleteConnexionCas3(GameObject _mainBat, GameObject _dice)
    {

    }


}