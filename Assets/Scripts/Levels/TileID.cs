using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileID : MonoBehaviour
{
    public Tile _tile;
    public TileType _type;
    public List<GameObject> _tilesPossible;

    [Header("Textures")]
    [SerializeField] Material _sandMaterial;
    [SerializeField] Material _grassMaterial;

    [ContextMenu("Devient du sable")]
    void SandTile()
    {
        for (int _loop = 0; _loop < _tilesPossible.Count; _loop++)
        {
            _tilesPossible[_loop].GetComponent<MeshRenderer>().material = _sandMaterial;
        }
    }
    [ContextMenu("Devient de l'herbe")]
    void GrassTile()
    {
        for (int _loop = 0; _loop < _tilesPossible.Count; _loop++)
        {
            _tilesPossible[_loop].GetComponent<MeshRenderer>().material = _grassMaterial;
        }
    }
}

public enum TileType
{
    Sand,
    Grass
}
