using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToBat : MonoBehaviour
{
    public GameObject _bat;


    public void NewNumber(int _nb)
    {
        _bat.GetComponent<NumberBat>()._number = _nb;
        _bat.GetComponent<NumberBat>().NumberActualisation();
    }
}
