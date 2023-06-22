using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{

    public int _number;
    int _face;
    public List<int> _listFaces = new List<int>();
    public float _delay = 4f;
    float _timer;
    [SerializeField] GameObject _dice;
    [SerializeField] Transform _center;
    [Header("chiffres")]
    [SerializeField] List<GameObject> _numbers;
    GameObject _actualNumber;
    void Start()
    {
        StartCoroutine(NumberActualisation());
    }

    void Update()
    {
        _timer -= Time.deltaTime;
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
        //GetComponent<DiceNumberManager>().DiceNumberActualisation();

        //on transmet
        GetComponent<BatimentDiceConnexion>().DiceNumberTransmission(_number);

        //on gere le d�
        _dice.SetActive(true);
        _dice.GetComponent<RotationDe>().StartAnimDice(_face);

        //pour le dispawn

        _timer = _delay;
    }

    public void NumberAppear()
    {        
        _actualNumber = Instantiate(_numbers[_number], _center.position, Quaternion.identity);
        _actualNumber.GetComponent<ChiffreManager>()._delay = _timer;
        //_actualNumber.transform.localScale = new Vector3(_size, _size, _size);
    }
    public void NumberDisappear()
    {
        Destroy(_actualNumber);
    }
}