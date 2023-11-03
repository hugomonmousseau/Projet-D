using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;
    //public GameState _gameState;
    [Header("Players")]
    public GameObject _host;
    public GameObject _client;

    [Header("Shop")]
    public GameObject _line;
    private void Awake()
    {
        _instance = this;

    }



}


public enum GameState
{
    Default,
    Pause,
    IsBuying,
    MovingTheCamera,
}

public enum Camp
{
    Attaquant,
    Defenseur
}


