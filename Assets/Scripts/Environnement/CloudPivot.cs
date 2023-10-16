using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPivot : MonoBehaviour
{
    [SerializeField] List<GameObject> _cloudList;
    [SerializeField] Vector2 _altitude;
    [SerializeField] Vector2 _range;
    [SerializeField] Vector2 _speed;
    [SerializeField] Vector2 _cloudNumber;

    Vector2 _position;
    void Start()
    {
        GetComponent<AllIn1VfxToolkit.Demo.Scripts.AllIn1AutoRotate>().rotationSpeed = Random.Range(_speed.x, _speed.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Clouds Generation")]
    private void Clouds()
    {
        int _nbClouds = Random.Range((int)_cloudNumber.x, (int)_cloudNumber.y);
        for(int _loop = 0; _loop < _nbClouds; _loop++)
        {
            float _distance = 0;
            while (_distance < _range.x)
            {
                _position = new Vector2(Random.Range(-_range.y, _range.y), Random.Range(-_range.y, _range.y));
                _distance = Mathf.Sqrt(Mathf.Pow((transform.position.x - _position.x), 2) + Mathf.Pow((transform.position.y - _position.y), 2));
                Debug.Log(_distance);
            }
            GameObject _cloud = Instantiate(_cloudList[Random.Range(0, _cloudList.Count - 1)], new Vector3(_position.x, Random.Range(_altitude.x, _altitude.y), _position.y), Quaternion.identity);
            _cloud.transform.LookAt(new Vector3(transform.position.x, _cloud.transform.position.y, transform.position.z));
            _cloud.transform.parent = gameObject.transform;
            _cloud.transform.localScale = new Vector3(_distance / 100, _distance / 100, _distance / 100);
        }
    }
}
