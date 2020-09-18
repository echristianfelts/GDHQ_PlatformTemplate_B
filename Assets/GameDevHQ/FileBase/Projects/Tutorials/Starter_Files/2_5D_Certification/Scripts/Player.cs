using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Speed
    [SerializeField]
    private float _speed = 5.0f;
    //Jumpheight
    [SerializeField]
    private float _jumpForce = 5.0f;
    //Gravity
    [SerializeField]
    private float _gravity = 1.0f;
    //Direction
    private Vector3 _direction;

    private CharacterController _controller;
    private Animator _anim;
    private Collider _grabSensor;
    private bool _jumping = false;
    private bool _onLedge;
    private Ledge _activeLedge;



    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _grabSensor = GetComponentInChildren<BoxCollider>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateMovement();

        if (_onLedge==true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _anim.SetTrigger("LedgeGrabTrigger");
            }
        }

    }

    void CalculateMovement()
    {
        // if grounded
        if (_controller.isGrounded == true)
        {
            float h = Input.GetAxisRaw("Horizontal");
            _direction = new Vector3(0, 0, h) * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(h));

            // what dir are we facing..?
            // if dir on x > o then face right. 
            // else face left
            if (h != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }


            _anim.ResetTrigger("Jump");
            if (_jumping)
            {
                _jumping = false;
                _anim.SetBool("Jumping", _jumping);
            }

            // on space jump...
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpForce;
                _anim.SetTrigger("Jump");

                _jumping = true;
                _anim.SetBool("Jumping", _jumping);
            }

        }

        _direction.y -= _gravity * Time.deltaTime;
        //  calculate movement direction based on user inputs
        // if jump 
        //  adjust jump height
        //  move
        _controller.Move(_direction * Time.deltaTime);
    }

    public void GrabLedge(Vector3 handpos, Ledge currentLedge)
    {
        _anim.SetBool("LedgeGrabBool", true);
        _jumping = false;
        _anim.SetBool("Jumping", _jumping);

        _controller.enabled = false;

        //_anim.SetFloat("Speed", 0f);

        _onLedge = true;
        // update hand pos.
        handpos.y -= 2.338f*3f;  // Player grab sensor offset height times scale.
        transform.position = handpos;
        _activeLedge = currentLedge;
    }

    public void ClimbUpComplete()
    {
        //  snap position.
        transform.position = _activeLedge.GetStandPos();
        //_anim.SetFloat("Speed", 0f);
        _anim.SetBool("LedgeGrabBool", false);

        _controller.enabled = true;

    }
}
