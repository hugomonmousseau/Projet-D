using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public TileType _tileType;
    public Building _building;
}

public enum Building
{
    PirateArtillerie,
    PirateArtisanat,
    PirateCamp,
    PirateHabitat,
    PirateCrane,
    PirateMarch�,

    RoyaumeMarch�,
    RoyaumeTour,
    RoyaumeArtisanat,
    RoyaumeCamp
}
