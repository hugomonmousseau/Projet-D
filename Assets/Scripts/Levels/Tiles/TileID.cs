using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileID : MonoBehaviour
{
    public Tile _tile;
    public TileType _type;
    public bool _isNextToWater;
    public List<GameObject> _tilesPossible;

    [Header("Visuals")]
    [SerializeField] Material _sandMaterial;
    [SerializeField] Material _grassMaterial;
    [SerializeField] GameObject _column;


    private void Start()
    {
        GameManager._instance._tiles.Add(gameObject);
    }
    [ContextMenu("Devient du sable")]
    void SandTile()
    {
        _type = TileType.Sand;
        for (int _loop = 0; _loop < _tilesPossible.Count; _loop++)
        {
            _tilesPossible[_loop].GetComponent<MeshRenderer>().material = _sandMaterial;
        }
    }
    [ContextMenu("Devient de l'herbe")]
    void GrassTile()
    {
        _type = TileType.Grass;
        for (int _loop = 0; _loop < _tilesPossible.Count; _loop++)
        {
            _tilesPossible[_loop].GetComponent<MeshRenderer>().material = _grassMaterial;
        }
    }
    [ContextMenu("Pose une colonne")]
    void Column()
    {
        Instantiate(_column, transform);
    }
}

public enum TileType
{
    Sand,
    Grass
}
