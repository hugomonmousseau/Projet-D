using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    /*
        [ContextMenu("SpawnLinePrev")]
        public void SpawnLinePrevisualisation()
        {
            DefineTilesGroupes();
            ListPointPrevisualisation();
        }

        private void DefineTilesGroupes()
        {
            //reset

            _usingTiles = new List<GameObject>();
            _usedTiles = new List<GameObject>();
            _waitingTiles = new List<GameObject>();
            _actualsLines = new List<GameObject>();
            _groupes = new List<TilesGroupe>();
            _ppTiles = new List<GameObject>();

            foreach(GameObject _tile in GameManager._instance._tiles)
            {
                //si on l utilise pas deja et que la tuile est vide et qu elles sont dans notre équipe
                if(_tile.GetComponent<TileID>()._tile._isEmpty && !_usingTiles.Contains(_tile) && _tile.GetComponent<TileInteraction>()._camp == _camp)
                {
                    //on le rajoute
                    _usingTiles.Add(_tile);
                    SetTilesPostProcessLayer(_tile);
                    _ppTiles.Add(_tile);

                }
            }
            //on prend les tuiles qui seront utilisées une par une
            foreach(GameObject _usingTile in _usingTiles)
            {
                //si la tuile utilisable n ai pas deja utilisée
                if (!_usedTiles.Contains(_usingTile))
                {

                    TilesGroupe _actualGroup = new TilesGroupe();

                    TileChecker(_usingTile, _actualGroup);

                    while (_waitingTiles.Count > 0)
                    {
                        TileChecker(_waitingTiles[0], _actualGroup);
                        _waitingTiles.Remove(_waitingTiles[0]);
                    }
                    _groupes.Add(_actualGroup);

                }
            }

        }

        private void TileChecker(GameObject _tile, TilesGroupe _actualGroup)
        {
            for(int _checker = 0; _checker < _tile.GetComponentInChildren<TileChecker>()._checkers.Count; _checker++)
            {
                //si l objet est nouveau
                if (_tile.GetComponentInChildren<TileChecker>().CheckerInt(_checker) != null && !_waitingTiles.Contains(_tile.GetComponentInChildren<TileChecker>().CheckerInt(_checker)) && _tile.GetComponentInChildren<TileChecker>().CheckerInt(_checker).GetComponent<TileID>()._tile._isEmpty && !_usedTiles.Contains(_tile.GetComponentInChildren<TileChecker>().CheckerInt(_checker)))
                {
                    _waitingTiles.Add(_tile.GetComponentInChildren<TileChecker>().CheckerInt(_checker));
                }
            }

            _usedTiles.Add(_tile);
            if(!_tile.GetComponentInChildren<TileChecker>().Checkers())
                _actualGroup._tiles.Add(_tile);
        }

        private void ListPointPrevisualisation()
        {

            foreach (TilesGroupe _tileGroupe in _groupes)
            {
                _pointsList = new List<Transform>();
                foreach (GameObject _tile in _tileGroupe._tiles)
                {
                    for(int _checker = 0; _checker < _tile.GetComponentInChildren<TileChecker>()._checkers.Count; _checker ++)
                    {
                        if (!_tile.GetComponentInChildren<TileChecker>().CheckerBool(_checker))
                        {
                            foreach (Transform _point in _tile.GetComponentInChildren<TileChecker>()._checkers[_checker].GetComponent<CheckerScript>()._points)
                            {
                                if (!_pointsList.Contains(_point))
                                {
                                    _pointsList.Add(_point);
                                }
                            }
                        }
                    }
                }
                _pointsList = SortTransforms(_pointsList);
                NewLine();
            }
        }

        public List<Transform> SortTransforms(List<Transform> _originalList)
        {
            if (_originalList == null || _originalList.Count == 0) return null;

            List<Transform> _sortedList = new List<Transform>();
            Transform _currentTransform = _originalList[0]; // Commencer avec le premier élément
            _sortedList.Add(_currentTransform);
            _originalList.RemoveAt(0);

            while (_originalList.Count > 0)
            {
                Transform _closestTransform = FindClosestTransform(_currentTransform, _originalList);
                _sortedList.Add(_closestTransform);
                _originalList.Remove(_closestTransform);
                _currentTransform = _closestTransform;
            }

            return _sortedList;
        }

        private Transform FindClosestTransform(Transform _currentTransform, List<Transform> _transforms)
        {
            Transform _closest = null;
            float _minDistance = float.MaxValue;

            foreach (Transform _trans in _transforms)
            {
                float distance = Vector3.Distance(_currentTransform.position, _trans.position);
                if (distance < _minDistance)
                {
                    _minDistance = distance;
                    _closest = _trans;
                }
            }

            return _closest;
        }

        private void NewLine()
        {
            GameObject _newLine = Instantiate(_line);
            _newLine.GetComponent<LinePrevisualisationManager>().RenderLine(_pointsList);
            _actualsLines.Add(_newLine);
        }

        [ContextMenu("Destroy Lines")]
        public void DestroyLines()
        {
            foreach(GameObject _line in _actualsLines)
            {
                _line.GetComponent<LinePrevisualisationManager>().DeathRattle();
            }
            _actualsLines = new List<GameObject>();

            foreach(GameObject _tile in _ppTiles)
            {
                SetTilesDefaultLayer(_tile);
            }
        }

        public void SetTilesPostProcessLayer(GameObject _tile)
        {
            //_tile.GetComponent<TileID>().SetPostProcessLayer();
        }
        public void SetTilesDefaultLayer(GameObject _tile)
        {
            //_tile.GetComponent<TileID>().SetDefaultLayer();
        }
        */
}
