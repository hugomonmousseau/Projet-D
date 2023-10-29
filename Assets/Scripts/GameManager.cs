using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;
    public GameState _gameState;

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
    DiceHUD,
    Pause,
    IsBuying,
    MovingTheCamera,
}

public enum Camp
{
    Attaquant,
    Defenseur
}


