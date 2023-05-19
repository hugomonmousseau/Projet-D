using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAnimator : MonoBehaviour
{
    private Material _material;
    public List<Texture> _idle;
    public List<Texture> _attack;
    public List<Texture> _run;
    public List<Texture> _dead;
    public List<Texture> _fall;
    public List<Texture> _jump;
    public List<Texture> _stunt;
    [SerializeField] List<Texture> _actualList;
    /*
    public bool _isAttacking;
    public bool _isRunnig;
    public bool _isDying;
    public bool _isFalling;
    public bool _isJumping;
    public bool _isStunt;
    */

    public EtatPersonnage _actualState;
    public EtatPersonnage _pastState;

    [SerializeField] bool _loopedAnimation;

    public float _animatedSpeed;
    int _frame;

    void Start()
    {
        // Obtenez le matériau de l'objet
        Renderer renderer = GetComponent<Renderer>();
        _material = renderer.material;

        // Appliquez la texture 1 au matériau au démarrage
        if(_idle.Count != 0)
            _material.mainTexture = _idle[0];
        _actualList = _idle;

        StartCoroutine(AnimateMaterial());
    }
    public void NewState()
    {
        //si nouvel etat
        if(_actualState != _pastState)
        {
            _frame = 0;
            _pastState = _actualState;
            //on divise en 2 catégories d'animation
            if (_actualState == EtatPersonnage.Attack)
            {
                _loopedAnimation = false;
                _actualList = _attack;
            }
            if (_actualState == EtatPersonnage.Dead)
            {
                _loopedAnimation = false;
                _actualList = _dead;
            }
            if (_actualState == EtatPersonnage.Stunt)
            {
                _loopedAnimation = false;
                _actualList = _stunt;
            }
            if (_actualState == EtatPersonnage.Jump)
            {
                _loopedAnimation = false;
                _actualList = _jump;
            }
            if (_actualState == EtatPersonnage.Idle)
            {
                _loopedAnimation = true;
                _actualList = _idle;
            }
            if (_actualState == EtatPersonnage.Run)
            {
                _loopedAnimation = true;
                _actualList = _run;
            }
            if (_actualState == EtatPersonnage.Fall)
            {
                _loopedAnimation = true;
                _actualList = _fall;
            }
        }
    }

    public void AnimationLooped(List<Texture> _list)
    {
        //Debug.Log(_frame);
        if (_frame >= _list.Count - 1)
            _frame = 0;
        else
            _frame++;


        _material.mainTexture = _list[_frame];
    }
    public void AnimationNonLooped(List<Texture> _list)
    {
        //Debug.Log(_frame);
        if (_frame < _list.Count - 1)
            _frame ++;

        _material.mainTexture = _list[_frame];
    }

    IEnumerator AnimateMaterial()
    {
        //Debug.Log("hi");
        //on teste ce que fait le personnage
        if (_loopedAnimation)
            AnimationLooped(_actualList);
        else
            AnimationNonLooped(_actualList);

        yield return new WaitForSeconds(1 / _animatedSpeed);
        StartCoroutine(AnimateMaterial());
    }

    
    private void Update()
    {
        NewState();
    }

}

public enum EtatPersonnage
{
    Idle,
    Attack,
    Run,
    Dead,
    Fall,
    Jump,
    Stunt,
}
