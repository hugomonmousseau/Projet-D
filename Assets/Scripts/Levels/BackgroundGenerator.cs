using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] List<Background> _backgrounds;
    [SerializeField] Transform _localisation;
    [SerializeField] Transform _pivotLocalisation;
    [SerializeField] Transform _parent;
    [SerializeField] int _backgroundNumber;


    [ContextMenu("Background")]
    private void BackgroundGeneration()
    {
        //on recupere le perimettre
        for (int _back = 0; _back < _backgroundNumber; _back++)
        {
            //on choisi le background à prendre
            int _rng = Random.Range(0, _backgrounds.Count);
            GameObject _background = Instantiate(_backgrounds[_rng]._background,_localisation);

            _background.transform.parent = _parent;
            _background.transform.position = new Vector3(_localisation.position.x, _localisation.position.y - _backgrounds[_rng]._altitude, _localisation.position.z);

            //on tourne le pivot
            _pivotLocalisation.localEulerAngles = new Vector3(0, (360f / _backgroundNumber) * _back, 0);
            //Debug.Log((360f / _backgroundNumber) * _back + " " + _localisation.position);
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
