using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDColorisation : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> _faces;

    public void ColorFaces(Color _color)
    {
        for (int _loop = 0; _loop < _faces.Count; _loop++)
        {
            _faces[_loop].color = _color;
        }
    }
}
