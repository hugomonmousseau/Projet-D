using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSelectedBatiment : MonoBehaviour
{
    public bool _selected;


    public void ImSelected()
    {
        //qd je séléctionne un batiment
        _selected = true;
        GetComponent<PointsManager>().PointsAppear();
        for (int _loop = 0; _loop < GetComponent<PointsManager>()._listPoints.Count; _loop++)
        {
            GameManager._instance._batVisiblesPoints.Add(GetComponent<PointsManager>()._listPoints[_loop].GetComponent<PointID>()._intID);
        }
    }
    public void ImNotSelected()
    {
        _selected = false;
        GetComponent<PointsManager>().PointsDisappear();
        GameManager._instance._batVisiblesPoints = new List<int>();
    }
}
