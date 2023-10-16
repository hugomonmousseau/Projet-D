using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePrevisualisationManager : MonoBehaviour
{
    [SerializeField] GameObject _emptyPrevisualisationTile;
    [SerializeField] GameObject _xPrevisualisationTile;

    public void Appear()
    {
        if (GetComponent<TileID>()._tile._isEmpty)
        {
            //_emptyPrevisualisationTile.SetActive(true);

        }
        else
            _xPrevisualisationTile.SetActive(true);

    }

    public void Disappear()
    {
        _emptyPrevisualisationTile.SetActive(false);            
        _xPrevisualisationTile.SetActive(false);
    }
}
