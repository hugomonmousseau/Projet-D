using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHUD : MonoBehaviour
{
    [SerializeField] GameObject _board;
    Vector3 _mouseDif = new Vector3();
    bool _mouseVectorIsInit = false;

    private void Update()
    {
        if(_board.GetComponent<SelectableStateReader>()._state == ButtonState.Pressed)
        {
            if (!_mouseVectorIsInit)
            {
                _mouseVectorIsInit = true;
                //_mouseDif = GetComponent<RectTransform>().localPosition - Input.mousePosition ;
            }
            GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;
            Debug.Log("mouse : " + Input.mousePosition + " et dif : " + _mouseDif + " rect : " + GetComponent<RectTransform>().anchoredPosition);
        }
        else
        {
            _mouseVectorIsInit = false;
        }
    }
}
