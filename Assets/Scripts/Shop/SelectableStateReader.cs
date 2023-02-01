using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class SelectableStateReader : MonoBehaviour
{
    public Selectable AnySelectable;
    private PropertyInfo _selectableStateInfo = null;
    public ButtonState _state;

    private void Awake()
    {
        _selectableStateInfo = typeof(Selectable).GetProperty("currentSelectionState", BindingFlags.NonPublic | BindingFlags.Instance);
    }

    private void Update()
    {
        int selectableState = (int)_selectableStateInfo.GetValue(AnySelectable);
        switch (selectableState)
        {
            case 0:
                //Normal Selection State
                _state = ButtonState.Normal;
                break;
            case 1:
                //Highlighted Selection State
                _state = ButtonState.Highlighted;
                break;
            case 2:
                //Pressed Selection State
                //Debug.Log("Pressed");
                _state = ButtonState.Pressed;
                break;
            case 3:
                //Selected Selection State
                //Debug.Log("Selected");
                _state = ButtonState.Selected;
                break;
            case 4:
                //Disabled Selection State
                //Debug.Log("Disabled");
                _state = ButtonState.Disabled;
                break;
        }
    }
}