using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCheckerWorkshop : MonoBehaviour
{
    [SerializeField] Transform _waterChecker;
    [SerializeField] LayerMask _layer;


    void Update()
    {
        RaycastHit _ray = new RaycastHit();
        if (Physics.Raycast(transform.position, _waterChecker.TransformDirection(-Vector3.up), out _ray))
            if (_ray.collider.GetComponent<TileID>()._isNextToWater)
                while (Physics.Raycast(_waterChecker.position,_waterChecker.TransformDirection(-Vector3.up), out _ray, Mathf.Infinity, _layer))
                    if (_ray.collider != null)
                        transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y + 60, 0);
    }
}
