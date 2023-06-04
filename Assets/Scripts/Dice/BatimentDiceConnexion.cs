using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatimentDiceConnexion : MonoBehaviour
{
    //ce sript envoie les infos du résultat des dés aux connéctés

    void Update()
    {
        //Debug.Log(GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._connecte);
    }

    public void DiceNumberTransmission(int _nb)
    {
        //si il est connecte
        if (GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._connecte)
        {
            //Debug.Log(GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._intID);

            //on cherche l autre point

            //point A
            for(int _loop = 0; _loop < GameManager._instance._allLines.Count; _loop++)
            {
                if(GameManager._instance._allLines[_loop]._pointB._intID == GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._intID)
                {
                    GameManager._instance._allPointsGO[GameManager._instance._allLines[_loop]._pointA._intID].GetComponent<PointToBat>().NewNumber(_nb);
                }
            }

            //point B
            for (int _loop = 0; _loop < GameManager._instance._allLines.Count; _loop++)
            {
                if (GameManager._instance._allLines[_loop]._pointA._intID == GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._intID)
                {
                    GameManager._instance._allPointsGO[GameManager._instance._allLines[_loop]._pointB._intID].GetComponent<PointToBat>().NewNumber(_nb);
                }
            }
            //GameManager._instance._allPointsGO[GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._point._intID].GetComponent<NumberBat>().NumberActualisation();
        }
    }
}
