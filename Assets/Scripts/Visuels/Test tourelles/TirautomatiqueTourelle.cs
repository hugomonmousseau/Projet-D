using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirautomatiqueTourelle : MonoBehaviour
{
    public Transform _focus;
    [SerializeField] Transform _pivot;
    [SerializeField] Transform _muzzleSpawn;
    [SerializeField] GameObject _muzzle;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _delay;


    void Start()
    {
        StartCoroutine(Shoot());
    }


    void Update()
    {
        _pivot.LookAt(_focus);
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(_delay);
        Instantiate(_muzzle, _muzzleSpawn);
        GameObject _newBullet = Instantiate(_bullet, _muzzleSpawn.transform.position , Quaternion.identity);

        //tir
        _newBullet.transform.LookAt(_focus);
        Rigidbody _rb = _newBullet.GetComponent<Rigidbody>();
        _rb.velocity = _newBullet.transform.forward * _newBullet.GetComponent<BulletScript>()._speed;


        StartCoroutine(Shoot());
    }
}
