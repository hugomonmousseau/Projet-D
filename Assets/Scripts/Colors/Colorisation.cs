using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorisation : MonoBehaviour
{
    public Colors _color;
    GameObject _colorManager;
    List<Colors> _choiceList = new List<Colors>();

    //entre 0 et 1 avec 1 = 100%
    float _ratioMax = 1;

    [SerializeField] float _colorationDuration = 1;
    void Start()
    {
        _colorManager = GameObject.FindGameObjectWithTag("ColorManager");
        if(GetComponent<BatimentManager>()._hierarchy == BatHierarchie.MainBat)
            RandomColor();

    }

    public void RandomColor()
    {
        //premiere boucle pour trouver la tier list
        for (int _loop = 0; _loop < _colorManager.GetComponent<ColorManager>()._tierList.Count + 1; _loop++)
        {
            //si la liste est pas vide, il faut choisir une couleurs
            if (_choiceList.Count != 0)
            {
                //la couleur est choisie ici
                _color = _choiceList[Random.Range(0, _choiceList.Count - 1)];


                for (int _colorLoop = 0; _colorLoop < _colorManager.GetComponent<ColorManager>()._tierList[_loop-1]._tier.Count; _colorLoop++)
                {
                    if (_color == _colorManager.GetComponent<ColorManager>()._tierList[_loop - 1]._tier[_colorLoop]._color)
                    {
                        _colorManager.GetComponent<ColorManager>()._tierList[_loop - 1]._tier[_colorLoop]._isAlreadyTook = true;

                    }
                }
                break;
            }
            
            else
            {

                //Debug.Log(_colorManager.GetComponent<ColorManager>()._tierList[_loop]._tier.Count);
                for (int _colorLoop = 0; _colorLoop < _colorManager.GetComponent<ColorManager>()._tierList[_loop]._tier.Count; _colorLoop++)
                {
                    //sielle est pas deja utilisée
                    if (!_colorManager.GetComponent<ColorManager>()._tierList[_loop]._tier[_colorLoop]._isAlreadyTook)
                    {
                        _choiceList.Add(_colorManager.GetComponent<ColorManager>()._tierList[_loop]._tier[_colorLoop]._color);
                    }


                }
            }
            
        }
        NewColor();
       
        
        
    }

    public void NewColor()
    {
        for (int _loopMat = 0; _loopMat < _colorManager.GetComponent<ColorManager>()._batColorList.Count; _loopMat++)
        {
            //on change l'apparance de la tourelle
            if (_colorManager.GetComponent<ColorManager>()._batColorList[_loopMat]._color == _color)
            {
                for (int _loop = 0; _loop < GetComponent<MaterialSwitcherColor>()._listRenderer.Count; _loop++)
                {
                    GetComponent<MaterialSwitcherColor>()._listRenderer[_loop].material.SetTexture("_coloredTexture", _colorManager.GetComponent<ColorManager>()._batColorList[_loopMat]._texture);
                    StartCoroutine(ColorTransition(_loop));
                }

                //on change aussi les points
                //on cherche la couleur des points
                for (int _tier = 0; _tier < _colorManager.GetComponent<ColorManager>()._tierList.Count; _tier++)
                {
                    for(int _color = 0; _color < _colorManager.GetComponent<ColorManager>()._tierList[_tier]._tier.Count; _color++)
                    {
                        if(_colorManager.GetComponent<ColorManager>()._tierList[_tier]._tier[_color]._color == _colorManager.GetComponent<ColorManager>()._batColorList[_loopMat]._color)
                        {
                            //Debug.Log(_colorManager.GetComponent<ColorManager>()._batColorList[_loopMat]._color);
                            //Debug.Log(_colorManager.GetComponent<ColorManager>()._tierList[_tier]._tier[_color]._PointColor);
                            if (GetComponent<PointsManager>()._dicePoint != null)
                                if(GetComponent<Colorisation>()._color != GetComponent<PointsManager>()._dicePoint.GetComponent<ColorPointChanger>()._colorName)
                                {
                                    GetComponent<PointsManager>()._dicePoint.GetComponent<ColorPointChanger>().Colorisation(_colorManager.GetComponent<ColorManager>()._tierList[_tier]._tier[_color]._PointColor);
                                    GetComponent<PointsManager>()._dicePoint.GetComponent<ColorPointChanger>()._colorName = _colorManager.GetComponent<ColorManager>()._tierList[_tier]._tier[_color]._color;

                                }
                            if (GetComponent<PointsManager>()._batimentPoint != null)
                                if (GetComponent<Colorisation>()._color != GetComponent<PointsManager>()._batimentPoint.GetComponent<ColorPointChanger>()._colorName)
                                {
                                    {
                                        GetComponent<PointsManager>()._batimentPoint.GetComponent<ColorPointChanger>().Colorisation(_colorManager.GetComponent<ColorManager>()._tierList[_tier]._tier[_color]._PointColor);
                                        GetComponent<PointsManager>()._batimentPoint.GetComponent<ColorPointChanger>()._colorName = _colorManager.GetComponent<ColorManager>()._tierList[_tier]._tier[_color]._color;

                                    }
                                }
                        }
                    }
                }
            }
        }
    }
    public void Decolor()
    {
        if (!(GetComponent<BatimentManager>()._hierarchy == BatHierarchie.MainBat))
        {
            for (int _loop = 0; _loop < GetComponent<MaterialSwitcherColor>()._listRenderer.Count; _loop++)
            {
                StartCoroutine(DecolorTransition(_loop));
            }
        }
    }

    IEnumerator ColorTransition(int _id)
    {
        float _time = 0;
        while (_time < _colorationDuration)
        {
            _time += Time.deltaTime;

            GetComponent<MaterialSwitcherColor>()._listRenderer[_id].material.SetFloat("_ratio", _ratioMax * _time / _colorationDuration);
            yield return null;

        }
    }
    IEnumerator DecolorTransition(int _id)
    {
        float _time = 0;
        while (_time < _colorationDuration)
        {
            _time += Time.deltaTime;

            GetComponent<MaterialSwitcherColor>()._listRenderer[_id].material.SetFloat("_ratio",1 -(_ratioMax * _time / _colorationDuration));

            if (_time >= _colorationDuration)
            {
                for (int _loop = 0; _loop < GetComponent<MaterialSwitcherColor>()._listRenderer.Count; _loop++)
                {
                    GetComponent<MaterialSwitcherColor>()._listRenderer[_loop].material.SetTexture("_coloredTexture", GetComponent<MaterialSwitcherColor>()._listRenderer[_loop].material.GetTexture("_defaultTexture"));

                }

            }
            yield return null;

        }
    }
}
