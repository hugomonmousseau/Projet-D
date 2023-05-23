using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiffreManager : MonoBehaviour
{
    public Animator _anim;
    public float _animDuration = .2F;
    public float _delay;
    void Start()
    {
        _anim = GetComponent<Animator>();
        StartCoroutine(DispawnAnim());
    }



    public IEnumerator DispawnAnim()
    {
        yield return new WaitForSeconds(_delay - _animDuration);
        _anim.SetBool("Dispawn", true);
        StartCoroutine(GetComponent<MaterialDependOnGradient>().ColorChanger());
        //Debug.Log(_delay - _animDuration);
    }


}
