using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingManager : MonoBehaviour
{
    public GameObject _previsualisation;
    [SerializeField] int _prix;
    ButtonState _lastState;
    private void Update()
    {
        if(GetComponent<SelectableStateReader>()._state != _lastState)
        {
            _lastState = GetComponent<SelectableStateReader>()._state;
            if (_lastState == ButtonState.Pressed)
                PrevisualisationAchat();
        }
    }

    public void PrevisualisationAchat()
    {
        if(_prix >= GetComponentInParent<PlayerScript>()._golds)
        {
            GetComponentInParent<PlayerScript>().NowShopping(_previsualisation);
        }
    }
}
