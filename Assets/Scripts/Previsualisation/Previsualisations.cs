using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Previsualisations : MonoBehaviour
{
    public GameObject _bat;
    public bool _wrong;
    [SerializeField] List<GameObject> _list;

    [SerializeField] GameObject _cross;
    [Header("Materials")]
    [SerializeField] Material _prev;
    [SerializeField] Material _wrongPrev;


    public void Deathrattle(TileType _tileType)
    {
        if (!_wrong)
        {
            GameObject _go = Instantiate(_bat, transform.position, transform.rotation);
            _go.GetComponent<BuildingManager>()._tileType = _tileType;
        }
        Destroy(gameObject);
    }

    public void NowPrev()
    {
        foreach (GameObject _go in _list)
            _go.GetComponent<MeshRenderer>().material = _prev;
        _wrong = !_wrong;
        _cross.SetActive(false);
    }

    public void NowWrongPrev()
    {
        foreach (GameObject _go in _list)
            _go.GetComponent<MeshRenderer>().material = _wrongPrev;

        _wrong = !_wrong;
        _cross.SetActive(true);
    }
}
