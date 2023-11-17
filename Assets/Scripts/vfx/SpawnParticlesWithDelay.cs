using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticlesWithDelay : MonoBehaviour
{
    [SerializeField] GameObject _particles;
    [SerializeField] float _delay;
    void Start()
    {
        if(_delay != 0f)
            Invoke("SpawnParticles", _delay);
    }

    void SpawnParticles()
    {
        Debug.Log(_delay);
        Instantiate(_particles, transform.position, Quaternion.identity);
        Invoke("SpawnParticles", _delay);
    } 
}
