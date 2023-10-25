using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSlotManager : MonoBehaviour
{
    bool _isRotating = false;
    public HUDtype _type;

    Vector2 _origin;
    bool _isActive;
    public void ButtonIsPress()
    {
        Debug.Log(_type);
        if(_type == HUDtype.barre)
        {

        }
    }

    public void ButtonIsRelease()
    {

    }
}

public enum HUDtype
{
    barre,
    croix,
    rotation
}
