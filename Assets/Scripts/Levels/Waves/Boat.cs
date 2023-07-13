using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] GameObject _fx;
    [SerializeField] GameObject _boatTrail;
    [SerializeField] List<GameObject> _sparkles;
    [SerializeField] float _duration = .75f;
    public List<Transform> _boatPoints;
    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.collider.tag == "End")
        {
            GetComponentInParent<UnitSpawner>().EndOfMovement();
            StartCoroutine(Trail());
            StartCoroutine(Sparkles());

        }
    }

    public IEnumerator Trail()
    {
        yield return new WaitForSeconds(_duration);
        _boatTrail.SetActive(false);
    }

    public IEnumerator Sparkles()
    {
        float _time = _duration;
        while (_time > 0)
        {
            _time -= Time.deltaTime;
            float _ratio = (_time/_duration);
            for (int _loop = 0; _loop < _sparkles.Count; _loop++)
            {
                _sparkles[_loop].transform.localScale = new Vector3(_ratio, _ratio, _ratio);
            }
            yield return null;
        }
    }

}
