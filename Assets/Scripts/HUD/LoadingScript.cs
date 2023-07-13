using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] List<Sprite> _sprites;
    public float _time;
    [SerializeField] float _duration;
    
    void Start()
    {
    }
    void Update()
    {
        if (_time <= _duration && _time >= 0)
            GetComponent<SpriteRenderer>().sprite = _sprites[(int)(_time * (_sprites.Count-1)/ _duration)];
        if (_time < 0)
            Destroy(gameObject);
    }
}
