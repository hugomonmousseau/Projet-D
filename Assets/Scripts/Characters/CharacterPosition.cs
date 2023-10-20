using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPosition : MonoBehaviour
{
    [SerializeField] Vector3 _characterOffset;

    [SerializeField] Transform _charactertransform;

    private void Update()
    {
        _charactertransform.position = new Vector3(_charactertransform.position.x, _charactertransform.position.y, 0);
    }
}
