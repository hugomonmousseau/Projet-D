using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChecker : MonoBehaviour
{
    public List<GameObject> _checkers;
    [SerializeField] LayerMask _layer;


    public GameObject CheckerInt(int _checkerID)
    {
        RaycastHit _ray = new RaycastHit();
        if (Physics.Raycast(_checkers[_checkerID].transform.position, _checkers[_checkerID].transform.TransformDirection(-Vector3.up), out _ray, Mathf.Infinity, _layer))
            if (_ray.collider != null)
                return _ray.collider.gameObject;

        return null;
    }

    public bool Checkers()
    {
        int nb = 0;
        RaycastHit _ray = new RaycastHit();
        for (int _checker = 0; _checker < _checkers.Count; _checker++)
        {
            if (Physics.Raycast(_checkers[_checker].transform.position, _checkers[_checker].transform.TransformDirection(-Vector3.up), out _ray, Mathf.Infinity, _layer))
                if (_ray.collider != null)
                    if(_ray.collider.GetComponent<TileID>()._tile._isEmpty)
                        nb++;

        }
        if (nb == _checkers.Count)
            return true;
        return false;
    }

    public bool CheckerBool(int _checkerID)
    {
        RaycastHit _ray = new RaycastHit();
        if (Physics.Raycast(_checkers[_checkerID].transform.position, _checkers[_checkerID].transform.TransformDirection(-Vector3.up), out _ray, Mathf.Infinity, _layer))
        {
            if (_ray.collider == null)
                return false;            
            else if (!_ray.collider.GetComponent<TileID>()._tile._isEmpty)
                return false;            
            else
                return true;
        }
        return false;
    }
}
