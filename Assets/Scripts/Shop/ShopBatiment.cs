using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ShopBatiment
{
    public Batiment _batiment;
    public GameObject _batimentGO;
    public int _prixEnOr;
    public int _prixEnMagie;
    public int _prixEnSciences;
}
public enum Batiment
{
    None,
    Marché,
    Laboratoire,
    Autel,
    Fabrique,
    PierreTaillee,
    TailleurDePierre,
    Observatoire,
    Maison,
    CarriereDoree,
    Forum,
    Theatre,
    EcoleDePhilosophie,
    Puis,
}
