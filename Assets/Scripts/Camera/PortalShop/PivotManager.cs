using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotManager : MonoBehaviour
{
    [SerializeField] Transform _cameraHolder;
    public float _holderRatio;
    [SerializeField] Vector3 _classicCameraRotation;

    private void Start()
    {
        _classicCameraRotation = transform.rotation.eulerAngles;
    }
    void Update()
    {
        Vector3 _holderRotation = _cameraHolder.transform.rotation.eulerAngles;

        Quaternion _rotationPivot = Quaternion.Euler(new Vector3(
            _holderRotation.x * _holderRatio + _classicCameraRotation.x * (1 - _holderRatio),
            _holderRotation.y * _holderRatio,
            _holderRotation.z * _holderRatio));
        transform.rotation = _rotationPivot;Z
    }
}
