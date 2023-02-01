using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSelectedBatiment : MonoBehaviour
{
    public bool _selected;


    public void ImSelected()
    {
        _selected = true;

    }
    public void ImNotSelected()
    {
        _selected = false;
    }
}
