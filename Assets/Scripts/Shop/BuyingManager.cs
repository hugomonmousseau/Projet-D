using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingManager : MonoBehaviour
{
    public GameObject _previsualisation;
    [SerializeField] int _prix;
    bool _onlyOneGO;
    private Vector3 _velocity = Vector3.zero;
    [SerializeField] float _speed = .15f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void PrevisualisationAchat()
    {
        GetComponentInParent<PlayerScript>()._state = GameState.IsBuying;
    }
}
