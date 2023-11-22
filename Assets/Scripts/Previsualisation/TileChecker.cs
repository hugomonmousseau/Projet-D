using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChecker : MonoBehaviour
{
    public List<Transform> _checkers;
    [SerializeField] LayerMask _layer;


    void Update()
    {
        /*
        RaycastHit _ray = new RaycastHit();
        if (Physics.Raycast(transform.position, _waterChecker.TransformDirection(-Vector3.up), out _ray))
            if (_ray.collider.GetComponent<TileID>()._isNextToWater)
                while (Physics.Raycast(_waterChecker.position,_waterChecker.TransformDirection(-Vector3.up), out _ray, Mathf.Infinity, _layer))
                    if (_ray.collider != null)
                        transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y + 60, 0);
        */
    }

    public GameObject Checker(int _checkerID)
    {
        RaycastHit _ray = new RaycastHit();
        if (Physics.Raycast(_checkers[_checkerID].position, _checkers[_checkerID].TransformDirection(-Vector3.up), out _ray, Mathf.Infinity, _layer))
            if (_ray.collider != null)
                return _ray.collider.gameObject;

        return null;
    }
}
