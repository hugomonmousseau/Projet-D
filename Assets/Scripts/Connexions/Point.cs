using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Point
{
    public Vector2 _coordonnees;
    public Type _type;
    public bool _connecte;
}

public enum Type
{
    De,
    Batiment,
}

public enum State
{
    Hide,
    Visible,
    Appear,
    Disappear,
}

