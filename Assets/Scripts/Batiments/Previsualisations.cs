using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Previsualisations : MonoBehaviour
{
    [SerializeField] GameObject _batimentAfterPrevisualisation;
    [SerializeField] GameObject _previsualisation;
    [SerializeField] GameObject _wrongPrevisualisation;
    public PrevisualisationType _type;

    public void DeathRattle()
    {
        if(_type == PrevisualisationType.Good)
        {
            Instantiate(_batimentAfterPrevisualisation, GameManager._instance._tileWeAreLooking._coordonnees, transform.rotation);

            for (int _loop = 0; _loop < GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TileManager>()._tilesList.Count; _loop++)
            {
                if (GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TileManager>()._tilesList[_loop].GetComponent<TileID>()._id == GameManager._instance._tileWeAreLooking._id)
                {
                    //Debug.Log("tile : " + GameManager._instance._tileWeAreLooking._id + " loop : " + _loop);
                    GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TileManager>()._tilesList[_loop].GetComponent<TileID>()._tile._isEmpty = false;
                }
            }
        }
        Destroy(gameObject);
    }

    public void WrongTile()
    {
        _type = PrevisualisationType.Wrong;
        _previsualisation.SetActive(false);
        _wrongPrevisualisation.SetActive(true);
    }
    public void GoodTile()
    {
        _type = PrevisualisationType.Good;
        _previsualisation.SetActive(true);
        _wrongPrevisualisation.SetActive(false);
    }
}
public enum PrevisualisationType
{
    Good,
    Wrong,
}
