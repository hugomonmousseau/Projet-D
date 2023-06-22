using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatimentManager : MonoBehaviour
{
    public Batiment _type;
    public BatHierarchie _hierarchy;

    MainBatimentsGOList _tempMainBat = new MainBatimentsGOList();

    private void Start()
    {
        if(_hierarchy == BatHierarchie.MainBat)
        {
            _tempMainBat._mainBatiment = gameObject;
            GameManager._instance._connexionList.Add(_tempMainBat);

        }
    }

}

public enum Batiment
{
    None,
    Dé,
    Artisanat,
    Auberges,
    Batisse,
    Campement,
    Cathedrale,
    Cerisier,
    Dependances,
    Eglise,
    Fabrique,
    Fontaine,
    Hotel,
    Hutte,
    Jardin,
    Manoir,
    Marché,
    Moulin,
    Pierre,
    Temple,
    Tourelle,
}

public enum BatHierarchie
{
    SimpleBat,
    MainBat,
    Dice,
}
