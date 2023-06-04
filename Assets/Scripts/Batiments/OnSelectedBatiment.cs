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
        //GetComponent<PointsManager>().PointsAppear();

        //on ajoute les 2 points du batiment
        for (int _loop = 0; _loop < GetComponent<PointsManager>()._listPoints.Count; _loop++)
        {
            GameManager._instance._visiblesPointsSelected.Add(GetComponent<PointsManager>()._listPoints[_loop].GetComponent<PointID>()._intID);
            //Debug.Log(GetComponent<PointsManager>()._listPoints[_loop].GetComponent<PointID>()._intID);
        }

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

        //on fait apparaitre les points en fonction de la liste
        for (int _loop = 0; _loop < GameManager._instance._visiblesPointsSelected.Count; _loop++)
        {
            GameManager._instance._allPointsGO[GameManager._instance._visiblesPointsSelected[_loop]].GetComponent<PointID>().OnePointAppear();
        }

        //et les lignes

        GameManager._instance.ShowLinesNeeded();
    }
    public void ImNotSelected()
    {
        _selected = false;
        //GameManager._instance.PrevisualisationHideSelectedPoints();

        //on refait la liste de séléction
        GameManager._instance._visiblesPointsSelected = new List<int>();

        //Debug.Log("Liste visible reset");
        GameManager._instance.HideAllPointsExceptSelected();
        GameManager._instance.ShowLinesNeeded();
    }


}
