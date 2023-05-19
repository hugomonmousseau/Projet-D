using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{

    public int _number;
    int _face;
    public List<int> _listFaces = new List<int>();
    public float _delay = 4f;
    [SerializeField] GameObject _dice;

    void Start()
    {
        StartCoroutine(NumberActualisation());

    }

    public int NewNumber(List<int> _list)
    {
        _face = Random.Range(0, _list.Count - 1);
        return _list[_face];
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

        //également au dé
        _dice.GetComponent<RotationDe>().StartAnimDice(_face);
    }
}
