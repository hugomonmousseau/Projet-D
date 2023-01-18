using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;

    public List<Point> _allPoints;
    private void Awake()
    {
        _instance = this;

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

}


