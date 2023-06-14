using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingManager : MonoBehaviour
{
    public GameObject _previsualisation;
    bool _onlyOneGO;
    private Vector3 _velocity = Vector3.zero;
    [SerializeField] float _speed = .15f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ici la fin de l'achat
        if(Input.GetMouseButtonUp(0) && GameManager._instance._gameState == GameState.IsBuying)
        {
            //to do : liste de go != de prévisualisation
            //on remplace la prévisualisation
            _previsualisation.GetComponent<Previsualisations>().DeathRattle();




            GameManager._instance._gameState = GameState.Default;
            GameManager._instance._inHand = null;
            _previsualisation = null;
            _onlyOneGO = false;
            GetComponent<ScrollViewShop>().ContinueScroll();

            //on reafffiche l hexagone
            GameManager._instance._hexagonSelection.SetActive(true);

            //on informe le ld manager
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TileManager>().NotBuying();

            //on cache les prev tiles
            for (int _loop = 0; _loop < GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TileManager>()._tilesList.Count; _loop++)
            {
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TileManager>()._tilesList[_loop].GetComponent<TilePrevisualisationManager>().Disappear();
            }

        }



        //----ici non----//
        if(GameManager._instance._gameState == GameState.IsBuying)
        {
            if (GameManager._instance._tileWeAreLooking._isEmpty && _previsualisation.GetComponent<Previsualisations>()._type == PrevisualisationType.Wrong)
                _previsualisation.GetComponent<Previsualisations>().GoodTile();

            if (!GameManager._instance._tileWeAreLooking._isEmpty && _previsualisation.GetComponent<Previsualisations>()._type == PrevisualisationType.Good)
                _previsualisation.GetComponent<Previsualisations>().WrongTile();


            //_previsualisation.transform.position = new Vector3(GameManager._instance._selectionWorldCoordonnees.x, 0, GameManager._instance._selectionWorldCoordonnees.y);
            //_previsualisation.transform.position = GameManager._instance._tileWeAreLooking._coordonnees;
            _previsualisation.transform.position =  Vector3.SmoothDamp(_previsualisation.transform.position, GameManager._instance._tileWeAreLooking._coordonnees, ref _velocity, _speed);
        }

    }

    public void PrevisualisationAchat()
    {
        if (!_onlyOneGO)
        {
            _previsualisation = Instantiate(GameManager._instance._inHand,new Vector3(0,-10,0),Quaternion.Euler(0,60 * Random.Range(0,5),0));
            _onlyOneGO = true;
            GetComponent<ScrollViewShop>().StopScroll();


            //on informe le ld manager
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TileManager>().IsBuying();
        }

    }
}
