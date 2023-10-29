using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    public GameObject _hud;
    public Camp _camp;

    public void Highlight()
    {
        if (GetComponent<TileID>()._tile._isEmpty)
            _hud.GetComponent<Animator>().SetBool("IsHere", true);
    }

    public void NotEventHighlight()
    {
        if(GameManager._instance._gameState != GameState.IsBuying)
            _hud.GetComponent<Animator>().SetBool("IsHere", false);

    }
}