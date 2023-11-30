using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimatorManager : MonoBehaviour
{
    [SerializeField] List<Animator> _animatorList = new List<Animator>();
    [SerializeField] string _parameter;
    bool _actif;
    public void ButtonChangeAnim()
    {
        foreach (Animator _animator in _animatorList)
        {
            _animator.SetBool(_parameter, !_animator.GetBool(_parameter));
            //Debug.Log("param " + _parameter+ " bool " + _animator.GetBool(_parameter));
        }
    }
    private void Update()
    {
        if(GetComponent<SelectableStateReader>()._state == ButtonState.Highlighted || GetComponent<SelectableStateReader>()._state == ButtonState.Pressed)
        {
            if (!_actif)
            {
                ButtonChangeAnim();
                _actif = true;
            }
        }
        else if (_actif)
        {
            ButtonChangeAnim();
            _actif = false;
        }
    }
}
