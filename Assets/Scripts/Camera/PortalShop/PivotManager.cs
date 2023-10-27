using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotManager : MonoBehaviour
{
    [SerializeField] Transform _cameraHolder;
    public float _holderRatio;
    [SerializeField] Vector3 _classicCameraRotation;
    public float _holderYSetOff;

    private void Start()
    {
        _classicCameraRotation = transform.rotation.eulerAngles;
    }
    void Update()
    {
        Vector3 _holderRotation = _cameraHolder.transform.rotation.eulerAngles;

        Quaternion _rotationPivot = Quaternion.Euler(new Vector3(
            _holderRotation.x * _holderRatio + _classicCameraRotation.x * (1 - _holderRatio),
            _holderYSetOff,
            _holderRotation.z * _holderRatio));
        transform.rotation = _rotationPivot;
    }
}
