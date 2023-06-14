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
            Instantiate(_batimentAfterPrevisualisation,GameManager._instance._tileWeAreLooking._coordonnees, transform.rotation);
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
