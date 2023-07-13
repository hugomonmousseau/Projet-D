using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [Header("Waves")]
    public List<Wave> _wavesList;
    public List<UnitsReference> _unitReference;
    int _waveID;

    void Start()
    {
        StartCoroutine(LanchBoat());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LanchBoat()
    {
        yield return new WaitForSeconds(_wavesList[_waveID]._maxDelay);
        for(int _loop =0; _loop < _wavesList.Count; _loop++)
        {
            for(int _wave = 0; _wave < _wavesList[_loop]._wave.Count; _wave++)
            {
                for(int _ref = 0; _ref < _unitReference.Count; _ref++)
                {
                    if (_wavesList[_loop]._wave[_wave]._unit == _unitReference[_ref]._unit)
                    {
                        //Debug.Log(_unitReference[_ref]._unit);
                        for(int _quantity = 0; _quantity < _wavesList[_loop]._wave[_wave]._quantiy; _quantity++)
                        {
                            GameObject _unit = Instantiate(_unitReference[_ref]._reference);
                            _unit.transform.localScale = new Vector3(.75f, .75f, .75f);
                            _wavesList[_waveID]._boat.GetComponent<UnitSpawner>()._unitList.Add(_unit);
                        }
                    }

                }
            }
        }
        //_wavesList[_waveID]._boat.GetComponent<UnitSpawner>()._unitList.Add();
    }
}
