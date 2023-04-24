using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{

    public int _number;
    public List<int> _listFaces = new List<int>();
    public float _delay = 4f;

    void Start()
    {
        StartCoroutine(NumberActualisation());

    }

    public int NewNumber(List<int> _list)
    {
        return _list[Random.Range(0, _list.Count - 1)];
    }
    
    IEnumerator NumberActualisation()
    {
        _number = NewNumber(_listFaces);
        //Debug.Log(_number);
        yield return new WaitForSeconds(_delay);
        StartCoroutine(NumberActualisation());

        //on affiche ca
        GetComponent<DiceNumberManager>().DiceNumberActualisation();

        //on transmet
        GetComponent<BatimentDiceConnexion>().DiceNumberTransmission(_number);
    }
}
