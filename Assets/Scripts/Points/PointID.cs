using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointID : MonoBehaviour
{
    GameObject _gameManager;

    public Point _point = new Point();

    [Header("IntID")]
    public int _intID;
    void Start()
    {
        //init point
        _point._coordonnees = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        _gameManager = GameObject.FindGameObjectWithTag("GameController");
        _intID = GameManager._instance._allPoints.Count;
        _point._intID = _intID;
        _gameManager.GetComponent<GameManager>()._allPoints.Add(_point);
        GameManager._instance._allPointsGO.Add(gameObject);
    }

    public void PointUpdate()
    {
        _point._coordonnees = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //GameManager._instance._allPoints[_point._intID] = _point;
    }

    public void OnePointAppear()
    {
        //Debug.Log("here");
        GetComponent<Animator>().SetBool("IsHere", true);
    }
    public void OnePointDisappear()
    {
        GetComponent<Animator>().SetBool("IsHere", false);
    }

    public void PointVisible()
    {
        _point._state = PointState.Visible;
    }
    public void PointInvisible()
    {
        _point._state = PointState.Hide;
    }
}



