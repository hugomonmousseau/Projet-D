using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourelleManager : MonoBehaviour
{
    //public TurretStats _stats;
    [Header("Stats")]
    public int _damage;
    public int _range;
    public int _AOE;
    public int _ricochet;
    public float _attackSpeed;
    public int _dmgPourcentMaxHP;

    [Space]
    [Header("fx")]
    public Transform _focus;
    public Transform _pivot;
    public Transform _muzzleSpawn;
    public GameObject _muzzle;
    public GameObject _bullet;
    void Start()
    {
        
    }


    void Update()
    {
        
    }


}

public enum TurretStats
{
    /*
    //degats par tir
    Dammage,
    //porté d attaque
    Range,
    //taille de l AOE des tirs
    AOE,
    //le nombre de ricochet par tir
    Ricochet,
    //vitesse d attaque
    AttackSpeed,
    //degats en fonction des pv max
    DmgPourcentMaxHP,
    //arjouter le reste ici https://docs.google.com/spreadsheets/d/1khq1EmqeAFr29HGM_BCIHBor10qLHc1j/edit#gid=1818272817
    */
}