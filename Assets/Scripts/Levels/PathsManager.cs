using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathsManager : MonoBehaviour
{
    public List<Path> _pathsList;

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        for(int _loop = 0; _loop < _pathsList.Count; _loop++)
        {
            for(int _way = 0; _way < _pathsList[_loop]._path.Count - 1; _way++)
            {
                Gizmos.DrawLine(new Vector3(_pathsList[_loop]._path[_way].transform.position.x, .1f, _pathsList[_loop]._path[_way].transform.position.z), new Vector3(_pathsList[_loop]._path[_way + 1].transform.position.x, .1f, _pathsList[_loop]._path[_way + 1].transform.position.z));
            }

        }
    }

    */
}
