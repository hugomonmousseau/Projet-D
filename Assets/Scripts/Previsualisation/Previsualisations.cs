using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Previsualisations : MonoBehaviour
{
    [SerializeField] GameObject _bat;

    public void Deathrattle(TileType _tileType)
    {
        GameObject _go = Instantiate(_bat, transform.position, transform.rotation);
        _go.GetComponent<BuildingManager>()._tileType = _tileType;
        Destroy(gameObject);
    }
}
