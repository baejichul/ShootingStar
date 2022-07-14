using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float _speed;
    public bool _isTouchTop;
    public bool _isTouchBottom;
    public bool _isTouchLeft;
    public bool _isTouchRight;

    Animator _ani;

    void Awake()
    {
        _ani = GetComponent<Animator>();
    }
    void Start()
    {
        _speed = 3.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // BorderLine 에 닿을 경우 체크
        if ((_isTouchRight && h == 1.0f) || (_isTouchLeft && h == -1.0f))
            h = 0;

        if ((_isTouchTop && v == 1.0f) || (_isTouchBottom && v == -1.0f))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * _speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        //if ( h == 0.0f || h == 1.0f || h == -1.0f)
        //_ani.SetInteger("input", int.Parse(h.ToString()));
        _ani.SetInteger("input", (int) h);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.tag == "Border" )
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    _isTouchTop = true;
                    break;
                case "Bottom":
                    _isTouchBottom = true;
                    break;
                case "Left":
                    _isTouchLeft = true;
                    break;
                case "Right":
                    _isTouchRight = true;
                    break;
            }
                
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    _isTouchTop = false;
                    break;
                case "Bottom":
                    _isTouchBottom = false;
                    break;
                case "Left":
                    _isTouchLeft = false;
                    break;
                case "Right":
                    _isTouchRight = false;
                    break;
            }

        }
    }
}
