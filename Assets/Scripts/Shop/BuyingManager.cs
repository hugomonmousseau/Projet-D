using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingManager : MonoBehaviour
{
    public GameObject _previsualisation;
    bool _onlyOneGO;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && GameManager._instance._gameState == GameState.IsBuying)
        {
            GameManager._instance._gameState = GameState.InGame;
            GameManager._instance._inHand = null;
            _previsualisation = null;
            _onlyOneGO = false;
            GetComponent<ScrollViewShop>().ContinueScroll();
        }
        if(GameManager._instance._gameState == GameState.IsBuying)
        {
            _previsualisation.transform.position = new Vector3(GameManager._instance._selectionWorldCoordonnees.x, 0, GameManager._instance._selectionWorldCoordonnees.y);
        }

    }

    public void PrevisualisationAchat()
    {
        if (!_onlyOneGO)
        {
            _previsualisation = Instantiate(GameManager._instance._inHand);
            _onlyOneGO = true;
            GetComponent<ScrollViewShop>().StopScroll();
        }

    }
}
