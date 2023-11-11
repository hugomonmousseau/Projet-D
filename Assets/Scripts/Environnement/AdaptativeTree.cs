using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptativeTree : MonoBehaviour
{
    [SerializeField] GameObject _grass;
    [SerializeField] GameObject _sand;

    private void Start()
    {
        ShowRightGO();
    }

    void ShowRightGO()
    {
        if (GetComponentInParent<BuildingManager>()._tileType == TileType.Grass)
            _grass.SetActive(true);
        if (GetComponentInParent<BuildingManager>()._tileType == TileType.Sand)
            _sand.SetActive(true);
    }
}
