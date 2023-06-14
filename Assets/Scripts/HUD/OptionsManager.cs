using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] GameObject _reglages;
    [SerializeField] GameObject _pause;
    [SerializeField] GameObject _sound;
    [SerializeField] GameObject _home;

    ButtonState _reglagesStats;
    bool _optionsAreVisible;
    private void Update()
    {
        if(_reglages.GetComponent<SelectableStateReader>()._state == ButtonState.Pressed && _reglagesStats != ButtonState.Pressed)
        {
            _reglagesStats = ButtonState.Pressed;
            _reglages.GetComponent<Animator>().SetBool("_clicking", true);
            

        }
        else if (_reglages.GetComponent<SelectableStateReader>()._state != ButtonState.Pressed && _reglagesStats == ButtonState.Pressed)
        {
            _reglagesStats = _reglages.GetComponent<SelectableStateReader>()._state;
            _reglages.GetComponent<Animator>().SetBool("_clicking", false);
            OptionsSwitchMode(_optionsAreVisible);
        }
    }

    
    void OptionsSwitchMode(bool _bool)
    {
        _optionsAreVisible = !_bool;
        GetComponent<Animator>().SetBool("_isHere", _bool);
    }
    

}
