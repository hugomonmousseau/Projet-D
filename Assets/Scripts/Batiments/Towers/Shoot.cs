using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [HideInInspector] public GameObject _focus;
    GameObject _oldFocus;
    [SerializeField] float _delay;
    float _cd;

    GameObject _actualFocus;


    private void Start()
    {
        _oldFocus = GetComponent<WindMillShoot>()._realFocus.gameObject;
    }
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("LevelManager").GetComponent<WavesManager>()._unitsAlive.Count != 0 && _cd <= 0)
        {
            if (_focus != null)
                _oldFocus = _focus;
            _focus = NewFocus();

            
            StartCoroutine(NewShoot());

            //windmill
            if (GetComponent<BatimentManager>()._type == Batiment.Moulin)
            {
                WindMillFocus();
                StartCoroutine(GetComponent<WindMillShoot>().BladeAnim());
            }

        }
        else
        {
            _cd -= Time.deltaTime;
        }
    }
    private void WindMillFocus()
    {
        /*
        if(_oldFocus != null)
            Debug.Log("oldF : " +_oldFocus.GetComponent<Unit>().name);
        if(_focus != null)
            Debug.Log("F : " + _focus.GetComponent<Unit>().name);
        if (_oldFocus != null && _focus != null)
            Debug.Log(_oldFocus == _focus);

        */
        if (_oldFocus != null && _focus != null)
        {
            if (_oldFocus != _focus)
            {
                //Debug.Log("confirmation");
                GetComponent<WindMillShoot>()._focus = _focus.transform;
                GetComponent<WindMillShoot>()._realFocus.position = _oldFocus.transform.position;

            }

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
        //le moulin ne réagit pas pareil
        if(GetComponent<BatimentManager>()._type == Batiment.Moulin)
        {
            GetComponent<TourelleManager>()._pivot.LookAt(new Vector3(_focus.transform.position.x, GetComponent<TourelleManager>()._pivot.position.y , _focus.transform.position.z));

        }
        else
        {
            GetComponent<TourelleManager>()._pivot.LookAt(new Vector3(_focus.transform.position.x, _focus.transform.position.y + _focus.GetComponent<Unit>()._size, _focus.transform.position.z));
        }
        _newBullet.transform.LookAt(new Vector3(_focus.transform.position.x, _focus.transform.position.y + _focus.GetComponent<Unit>()._size, _focus.transform.position.z));
        _newBullet.GetComponent<BulletScript>()._focus = _focus;



        
    }


}
