using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointID : MonoBehaviour
{
    GameObject _gameManager;
    [SerializeField] Vector2 _coordonnees;
    [SerializeField] Type _type;

    [SerializeField] Point _point = new Point();
    void Start()
    {
        _coordonnees = new Vector2(transform.position.x, transform.position.z);
        //init point
        _point._coordonnees = _coordonnees;
        _point._type = _type;

        _gameManager = GameObject.FindGameObjectWithTag("GameController");
        _gameManager.GetComponent<GameManager>()._allPoints.Add(_point);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



