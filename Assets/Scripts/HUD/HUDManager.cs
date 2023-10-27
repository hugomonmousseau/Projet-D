using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    bool _isRotating = false;
    public HUDtype _type;

    public List<GameObject> _pivotList;
    [SerializeField] float _rotationDuration;
    [SerializeField] AnimationCurve _curve;
    bool _isActive;
    public void ButtonIsPress()
    {
        //Debug.Log(_type);
        if(_type == HUDtype.rotation)
        {
            _isActive = !_isActive;

            foreach (GameObject _pivot in _pivotList)
            {
                StartCoroutine(SwitchRotationMode(_pivot));
            }
        }
    }

    public void ButtonIsRelease()
    {
    }

    public void ButtonIsSelected()
    {

    }

    private IEnumerator SwitchRotationMode(GameObject _pivot)
    {
        float _duration = 0;
        while (_rotationDuration > _duration)
        {
            if(_isActive)
                _pivot.GetComponent<PivotManager>()._holderRatio = _curve.Evaluate(_duration / _rotationDuration);
            else
                _pivot.GetComponent<PivotManager>()._holderRatio = _curve.Evaluate(1 - (_duration / _rotationDuration));
            _duration += Time.deltaTime;
            yield return null;
        }
    }

}

public enum HUDtype
{
    barre,
    croix,
    rotation
}
