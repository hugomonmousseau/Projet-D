using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject _appearance;
    public Colors _color;
    [SerializeField] GameObject _colorManager;
    List<Colors> _choiceList = new List<Colors>();
    void Start()
    {
        NewColor();

    }

    public void NewColor()
    {
        //premiere boucle pour trouver la tier list
        for (int _loop = 0; _loop < _colorManager.GetComponent<ColorManager>()._tierList.Count + 1; _loop++)
        {
            //si la liste est pas vide, il faut choisir une couleur
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
        
        for (int _loopMat = 0; _loopMat < _colorManager.GetComponent<ColorManager>()._batColorList.Count; _loopMat++)
        {
            //on change l'apparance de la tourelle
            if (_colorManager.GetComponent<ColorManager>()._batColorList[_loopMat]._color == _color)
                _appearance.GetComponent<MeshRenderer>().material.mainTexture = _colorManager.GetComponent<ColorManager>()._batColorList[_loopMat]._texture;
        }
        
    }
}
