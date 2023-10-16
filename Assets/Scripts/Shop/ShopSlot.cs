using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    public ShopBatiment _slot;
    public GameObject _buttonManager;
    Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (GameManager._instance._or >= _slot._prixEnOr && GetComponent<SelectableStateReader>()._state == ButtonState.Pressed)
        {
            GameManager._instance._gameState = GameState.IsBuying;
            GameManager._instance._inHand = _slot._batimentGO;
            _buttonManager.GetComponent<BuyingManager>().PrevisualisationAchat();

            //on cache l hexagone
            GameManager._instance._hexagonSelection.SetActive(false);

            //on active les prev tiles
            for(int _loop = 0; _loop < GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TileManager>()._tilesList.Count; _loop++)
            {
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TileManager>()._tilesList[_loop].GetComponent<TilePrevisualisationManager>().Appear();
            }

        }

        if (GetComponent<SelectableStateReader>()._state == ButtonState.Highlighted && !_anim.GetBool("Highlighted"))
        {
            _anim.SetBool("Highlighted", true);
        }

        if (GetComponent<SelectableStateReader>()._state != ButtonState.Highlighted && _anim.GetBool("Highlighted"))
        {
            _anim.SetBool("Highlighted", false);
        }
    }

    //if(GameManager._instance._or >= _slot._prixEnOr && GameManager._instance._magie >= _slot._prixEnMagie && GameManager._instance._sciences >= _slot._prixEnSciences && GameManager._instance._gameState == GameState.InGame)

}
