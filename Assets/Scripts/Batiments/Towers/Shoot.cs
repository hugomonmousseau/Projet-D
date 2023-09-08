using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [HideInInspector] public GameObject _focus;
    [SerializeField] float _delay;
    float _cd;

    GameObject _actualFocus;
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("LevelManager").GetComponent<WavesManager>()._unitsAlive.Count != 0 && _cd <= 0)
        {
            _focus = NewFocus();
            StartCoroutine(NewShoot());
        }
        else
        {
            _cd -= Time.deltaTime;
        }
    }
    [ContextMenu("new focus")]
    public GameObject NewFocus()
    {
        float _lowerRange = 1000f;

        for(int _allUnits = 0; _allUnits < GameObject.FindGameObjectWithTag("LevelManager").GetComponent<WavesManager>()._unitsAlive.Count; _allUnits++)
        {
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<WavesManager>()._unitsAlive[_allUnits].GetComponent<Unit>().DistanceUpdate();
            if (_lowerRange > GameObject.FindGameObjectWithTag("LevelManager").GetComponent<WavesManager>()._unitsAlive[_allUnits].GetComponent<Unit>()._distanceFromCrystal)
            {
                _lowerRange = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<WavesManager>()._unitsAlive[_allUnits].GetComponent<Unit>()._distanceFromCrystal;
                _actualFocus = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<WavesManager>()._unitsAlive[_allUnits];
            }

        }
        //Debug.Log(_actualFocus.name);
        return _actualFocus;
    }

    IEnumerator NewShoot()
    {
        //Debug.Log(name + " a tiré sur : " + _focus.name);
        _focus.GetComponent<Unit>()._hp -= GetComponent<TourelleManager>()._damage;
        if (_focus.GetComponent<Unit>()._hp <= 0)
            _focus.GetComponent<Unit>().Dead();
        _cd = (1 / GetComponent<TourelleManager>()._attackSpeed);

        Instantiate(GetComponent<TourelleManager>()._muzzle, GetComponent<TourelleManager>()._muzzleSpawn);

        yield return new WaitForSeconds(_delay);
        GameObject _newBullet = Instantiate(GetComponent<TourelleManager>()._bullet, GetComponent<TourelleManager>()._muzzleSpawn.transform.position, Quaternion.identity);

        //tir
        GetComponent<TourelleManager>()._pivot.LookAt( new Vector3(_focus.transform.position.x, _focus.transform.position.y + _focus.GetComponent<Unit>()._size, _focus.transform.position.z));
        _newBullet.transform.LookAt(new Vector3(_focus.transform.position.x, _focus.transform.position.y + _focus.GetComponent<Unit>()._size, _focus.transform.position.z));
        _newBullet.GetComponent<BulletScript>()._focus = _focus;
        
    }


}
