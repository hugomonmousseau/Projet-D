using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] GameObject _dicePoint;
    [SerializeField] GameObject _batimentPoint;

    [Space]
    [Header("Transforms")]
    [SerializeField] Transform _centre;
    [SerializeField] Transform _diceCote;
    [SerializeField] Transform _batCote;
    [SerializeField] Transform _pivot;
}
