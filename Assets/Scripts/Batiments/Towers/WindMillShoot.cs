using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMillShoot : MonoBehaviour
{
    public GameObject _mill;

    public Transform _focus;
    public Transform _oldFocus;
    public Transform _realFocus;


    public float _smoothTime = 0.3f;
    private Vector3 _velocity = Vector3.zero;

    Animator _anim;
    private void Start()
    {
        _focus = gameObject.transform;
        _oldFocus = gameObject.transform;
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        _realFocus.position = Vector3.SmoothDamp(_realFocus.position, _focus.position, ref _velocity, _smoothTime);
        _mill.transform.LookAt(new Vector3(_realFocus.transform.position.x, _mill.transform.position.y, _realFocus.transform.position.z));
    }
    public IEnumerator BladeAnim()
    {
        _anim.SetBool("Shot", true);
        yield return new WaitForSeconds(2f / 3f);
        _anim.SetBool("Shot", false);
    }
}
