using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSelectedBatiment : MonoBehaviour
{
    public bool _selected;
    bool _isWaiting;
    //ici on a la selection
    public void ImSelected()
    {
        //qd je séléctionne un batiment
        _selected = true;
        //GetComponent<PointsManager>().PointsAppear();
        //on commence par chercher la famille du batiment
        _isWaiting = false;
        int _idGroup = -1;
        for(int _loop = 0; _loop < GameManager._instance._connexionList.Count; _loop++)
        {
            if (GameManager._instance._connexionList[_loop]._mainBatiment == gameObject || GameManager._instance._connexionList[_loop]._coDice == gameObject)
                _idGroup = _loop;
            if(GameManager._instance._connexionList[_loop]._coBatiments != null)
            {                //on cherche dans les groupes
                for (int _group = 0; _group < GameManager._instance._connexionList[_loop]._coBatiments.Count; _group++)
                {
                    if (GameManager._instance._connexionList[_loop]._coBatiments[_group]._batiment == gameObject || GameManager._instance._connexionList[_loop]._coBatiments[_group]._coDice == gameObject)
                        _idGroup = _loop;
                }

            }

        }
        //on verifie si le batiment est dans la waiting list
        if(_idGroup == -1)
        {
            for (int _loop = 0; _loop < GameManager._instance._waitingCoBatiments.Count; _loop++)
            {
                if (GameManager._instance._waitingCoBatiments[_loop]._batiment == gameObject || GameManager._instance._waitingCoBatiments[_loop]._coDice == gameObject)
                {
                    _isWaiting = true;
                    _idGroup = _loop;
                }
        }
        }




            
        //GameManager._instance._visiblesPointsSelected         
        //on regarde si il attend pas

        if (_idGroup == -1)
        {
            //les batiments ici sont des batiments seuls
            //on affiche donc uniquement leurs points
            for(int _loop = 0; _loop < GetComponent<PointsManager>()._listPoints.Count; _loop ++)
            {
                GameManager._instance._visiblesPointsSelected.Add(GetComponent<PointsManager>()._listPoints[_loop].GetComponent<PointID>()._intID);

            }
        }
        else if(!_isWaiting)
        {
            //les batiments ici sont connectés à un main bat à un moment donné

            //main bat
            if (GameManager._instance._connexionList[_idGroup]._mainBatiment.GetComponent<PointsManager>()._dicePoint != null)
                GameManager._instance._visiblesPointsSelected.Add(GameManager._instance._connexionList[_idGroup]._mainBatiment.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._intID);
            GameManager._instance._visiblesPointsSelected.Add(GameManager._instance._connexionList[_idGroup]._mainBatiment.GetComponent<PointsManager>()._batimentPoint.GetComponent<PointID>()._intID);
            
            
            //si il a un dé on le rajoute
            if (GameManager._instance._connexionList[_idGroup]._coDice != null)
                GameManager._instance._visiblesPointsSelected.Add(GameManager._instance._connexionList[_idGroup]._coDice.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._intID);
            

            //puis le reste
            for(int _loop = 0; _loop < GameManager._instance._connexionList[_idGroup]._coBatiments.Count; _loop++)
            {
                for(int _points = 0; _points < GameManager._instance._connexionList[_idGroup]._coBatiments[_loop]._batiment.GetComponent<PointsManager>()._listPoints.Count; _points++)
                {
                    GameManager._instance._visiblesPointsSelected.Add(GameManager._instance._connexionList[_idGroup]._coBatiments[_loop]._batiment.GetComponent<PointsManager>()._listPoints[_points].GetComponent<PointID>()._intID);

                }
                
                if(GameManager._instance._connexionList[_idGroup]._coBatiments[_loop]._coDice != null)
                    GameManager._instance._visiblesPointsSelected.Add(GameManager._instance._connexionList[_idGroup]._coBatiments[_loop]._coDice.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._intID);
                
            }
        }
        else
        {
            //tous ces batiments sont dans la waiting list
            for (int _points = 0; _points < GameManager._instance._waitingCoBatiments[_idGroup]._batiment.GetComponent<PointsManager>()._listPoints.Count; _points++)
            {
                GameManager._instance._visiblesPointsSelected.Add(GameManager._instance._waitingCoBatiments[_idGroup]._batiment.GetComponent<PointsManager>()._listPoints[_points].GetComponent<PointID>()._intID);

            }
            GameManager._instance._visiblesPointsSelected.Add(GameManager._instance._waitingCoBatiments[_idGroup]._coDice.GetComponent<PointsManager>()._dicePoint.GetComponent<PointID>()._intID);
        }


        //Debug.Log(_idGroup);

        //commencons par les batiments qui ne sont pas dans la waiting list


        // on cherche si des lignes sont connectées
        for (int _loop = 0; _loop < GameManager._instance._allLines.Count; _loop++)
        {
            // les lignes sont composées de 2 points :
            // on vérifie si le batiment séléctionné à un point en commun avec une ligne active

            //on commence par le point A 
                
            if(GameManager._instance._visiblesPointsSelected.Contains(GameManager._instance._allLines[_loop]._pointA._intID))                
            {
                    //et on verifie que le point b n y est pas deja
                    
                if(!GameManager._instance._visiblesPointsSelected.Contains(GameManager._instance._allLines[_loop]._pointB._intID))                    
                {                        
                    //on ajoute le point B                        
                    GameManager._instance._visiblesPointsSelected.Add(GameManager._instance._allLines[_loop]._pointB._intID);
                    
                }                
            }
                
            // et on fait la meme avec le B                
            if (GameManager._instance._visiblesPointsSelected.Contains(GameManager._instance._allLines[_loop]._pointB._intID) && !GameManager._instance._visiblesPointsSelected.Contains(GameManager._instance._allLines[_loop]._pointA._intID))
                
            {                    
                GameManager._instance._visiblesPointsSelected.Add(GameManager._instance._allLines[_loop]._pointA._intID);
                
            }                
            
        }
        if(GameManager._instance._allPointsGO.Count > 0)
        {
            //on fait apparaitre les points en fonction de la liste
            for (int _loop = 0; _loop < GameManager._instance._visiblesPointsSelected.Count; _loop++)
            {
                //Debug.Log("pts visible : " + GameManager._instance._visiblesPointsSelected.Count + " loop : " + _loop + " allPts : " + GameManager._instance._allPointsGO.Count);
                GameManager._instance._allPointsGO[GameManager._instance._visiblesPointsSelected[_loop]].GetComponent<PointID>().OnePointAppear();
            }
        }


        //et les lignes

        //GameManager._instance.ShowLinesNeeded();
    }
    public void ImNotSelected()
    {
        _selected = false;
        //GameManager._instance.PrevisualisationHideSelectedPoints();

        //on refait la liste de séléction
        GameManager._instance._visiblesPointsSelected = new List<int>();

        //Debug.Log("Liste visible reset");
        GameManager._instance.HideAllPointsExceptSelected();
        //GameManager._instance.ShowLinesNeeded();
    }


}
