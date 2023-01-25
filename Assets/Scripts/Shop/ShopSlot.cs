using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    public ShopBatiment _slot;

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && GameManager._instance._gameState == GameState.IsBuying)
        {
            GameManager._instance._gameState = GameState.InGame;
        }
    }
    public void Achat()
    {
        if(GameManager._instance._or >= _slot._prixEnOr && GameManager._instance._magie >= _slot._prixEnMagie && GameManager._instance._sciences >= _slot._prixEnSciences)
        {
            Debug.Log("Oui");
        }
        else
        {
            Debug.Log("Non");
        }
    }
}
