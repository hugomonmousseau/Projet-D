using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Camp _type;
    public GameState _state;
    void Start()
    {
        if (GameManager._instance._host == null)
            GameManager._instance._host = gameObject;
        else
            GameManager._instance._client = gameObject;
    }

    void Update()
    {
        
    }

}
