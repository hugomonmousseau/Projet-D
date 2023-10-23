using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotManager : MonoBehaviour
{
    [SerializeField] Transform _cameraHolder;
    void Update()
    {
        transform.rotation = _cameraHolder.rotation;
    }
}
