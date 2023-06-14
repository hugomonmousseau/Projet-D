using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<GameObject> _tilesList;
    public void IsBuying()
    {
        //on active les collider des tuiles
        for (int _loop= 0; _loop < _tilesList.Count; _loop++)
        {
            _tilesList[_loop].GetComponent<MeshCollider>().enabled = true;
        }
        // on deactive les collider des batiments
        for(int _loop =0; _loop < GetComponent<LevelManager>()._allBats.Count; _loop++)
        {
            GetComponent<LevelManager>()._allBats[_loop].GetComponent<SphereCollider>().enabled = false;
        }
    }
    public void NotBuying()
    {
        for (int _loop = 0; _loop < _tilesList.Count; _loop++)
        {
            _tilesList[_loop].GetComponent<MeshCollider>().enabled = false;
        }
        for (int _loop = 0; _loop < GetComponent<LevelManager>()._allBats.Count; _loop++)
        {
            GetComponent<LevelManager>()._allBats[_loop].GetComponent<SphereCollider>().enabled = true;
        }
    }
}
