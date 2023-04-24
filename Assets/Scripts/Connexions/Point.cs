using UnityEngine;


[System.Serializable]
public class Point
{
    public Vector2 _coordonnees;
    public Type _type;
    public bool _connecte;
    public PointState _state;
    public int _intID;
}

public enum Type
{
    De,
    Batiment,
    BatimentsPourTourelle,
}

public enum PointState
{
    Hide,
    Visible,
    Appear,
    Disappear,
}

