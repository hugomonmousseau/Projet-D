using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileID : MonoBehaviour
{
    public Tile _tile;
    GameObject _levelManager;
    int _id;
    void Start()
    {
        _levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        _tile._coordonnees = transform.position;
        _id = _levelManager.GetComponent<TileManager>()._tilesList.Count;
        _tile._id = _id;
        _levelManager.GetComponent<TileManager>()._tilesList.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
