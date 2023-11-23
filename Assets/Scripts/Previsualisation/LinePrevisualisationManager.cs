using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePrevisualisationManager : MonoBehaviour
{
    LineRenderer _line;
    public List<Transform> _points = new List<Transform>();
    void Start()
    {
        _line = GetComponent<LineRenderer>();
    }

    public void RenderLine()
    {

    }
}
