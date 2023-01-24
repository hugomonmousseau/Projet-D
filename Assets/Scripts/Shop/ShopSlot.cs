using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    public ShopBatiment _slot;


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
