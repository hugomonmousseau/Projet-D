using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    public ShopBatiment _slot;
    public GameObject _buttonManager;

    void Update()
    {
        if (GameManager._instance._or >= _slot._prixEnOr && GameManager._instance._magie >= _slot._prixEnMagie && GameManager._instance._sciences >= _slot._prixEnSciences && GetComponent<SelectableStateReader>()._state == ButtonState.Pressed)
        {
            GameManager._instance._gameState = GameState.IsBuying;
            GameManager._instance._inHand = _slot._batimentGO;
            _buttonManager.GetComponent<BuyingManager>().PrevisualisationAchat();
        }


    }

    //if(GameManager._instance._or >= _slot._prixEnOr && GameManager._instance._magie >= _slot._prixEnMagie && GameManager._instance._sciences >= _slot._prixEnSciences && GameManager._instance._gameState == GameState.InGame)

}
