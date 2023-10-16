using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{

    [SerializeField] float _rotationMin;
    [SerializeField] float _rotationMax;


    void Start()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(_rotationMin, _rotationMax), 0);
    }



    
}
