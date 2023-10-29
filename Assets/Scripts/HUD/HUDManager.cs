using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject _tile;
    public HUDType _type;
    bool _selected;

    public void Highlight()
    {
        _selected = true;
    }

    public void NotEventHighlight()
    {
        _selected = false;
        if (_type == HUDType.Add && GameManager._instance._gameState != GameState.IsBuying)
        {

        }

    }

    private void Update()
    {
        if (_selected && Input.GetMouseButtonDown(0))
            Pressed();
    }

    void Pressed()
    {
        if (_type == HUDType.Add)
        {

        }
        

    }

}
public enum HUDType
{
    Add,
    Tour,
    Marché,
    Camps,
    Artisanat,
    Habitations,
    Puits
}


