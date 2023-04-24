using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceNumberManager : MonoBehaviour
{
    [SerializeField] TextMeshPro _txt;
    void Start()
    {
        _txt.text = "start";
    }



    public void DiceNumberActualisation()
    {
        _txt.text = GetComponent<DiceManager>()._number.ToString();
    }
}
