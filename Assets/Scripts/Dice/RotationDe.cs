using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDe : MonoBehaviour
{
    [SerializeField] bool _isJumping;
    [SerializeField] float _force;
    Rigidbody _rb;
    Animator _anim;
    [SerializeField] GameObject _base;

    [Header("vfx")]
    [SerializeField] GameObject _endJumpParticles;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("DiceBase"))
        {

            //on enleve les anims

            _anim.SetBool("face1", false);
            _anim.SetBool("face2", false);
            _anim.SetBool("face3", false);
            _anim.SetBool("face4", false);
            _anim.SetBool("face5", false);
            _anim.SetBool("face6", false);

            Instantiate(_endJumpParticles, transform.position, Quaternion.Euler(0, 0, 0));
            _base.GetComponent<DiceManager>().NumberAppear();
            gameObject.SetActive(false);
        }
    }
    public void StartAnimDice(int _face)
    {

        _base.GetComponent<DiceManager>().NumberDisappear();
        _rb.AddForce(Vector3.up * _force);

        //si le dé tombe sur 1
        switch (_face + 1)
        {
            case 1:
                _anim.SetBool("face1", true);
                break;
            case 2:
                _anim.SetBool("face2", true);
                break;
            case 3:
                _anim.SetBool("face3", true);
                break;
            case 4:
                _anim.SetBool("face4", true);
                break;
            case 5:
                _anim.SetBool("face5", true);
                break;
            case 6:
                _anim.SetBool("face6", true);
                break;

        }
    }
}
