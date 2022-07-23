using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool _isTouchTop;
    public bool _isTouchBottom;
    public bool _isTouchLeft;
    public bool _isTouchRight;
    public bool _isHit;

    public int _life;
    public int _score;

    public GameObject _bulletA;
    public GameObject _bulletB;
    public GameManager _gManager;

    public float _speed;
    public int _power;
    public float _curShotDelay;

    Animator _ani;
    Rigidbody2D _rigid;
    Rigidbody2D _rigidL;
    Rigidbody2D _rigidR;

    public const int PLAYER_BASIC_LIFE = 3;
    const int PLAYER_FIRE_FORCE = 10;
    const float PLAYER_FIRE_DELAY = 0.2f;
    const string RESOURCES_PREFABS_PATH = "Prefabs";

    void Awake()
    {
        _ani = GetComponent<Animator>();
        // _gManager = GameObject.FindObjectOfType<GameManager>();
        _gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        _speed = 3.0f;
        _power = 3;
        _life  = PLAYER_BASIC_LIFE;
        _bulletA = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/PlayerBulletA");
        _bulletB = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/PlayerBulletB");
    }

    // Update is called once per frame
    void Update()
    {   
        MovePlayer();
        Fire();
        Reload();
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

    void Fire()
    {
        if ( Input.GetButton("Fire1") )
        {
            if (_curShotDelay >= PLAYER_FIRE_DELAY)
            {

                switch (_power)
                {
                    case 1:
                        GameObject bullet = Instantiate(_bulletA, transform.position + (Vector3.up * 0.1f), transform.rotation);
                        bullet.name = "PlayerBulletA";

                        _rigid = bullet.GetComponent<Rigidbody2D>();
                        _rigid.AddForce(Vector2.up * PLAYER_FIRE_FORCE, ForceMode2D.Impulse);
                        break;
                    case 2:
                        GameObject bulletL = Instantiate(_bulletA, transform.position + (Vector3.up * 0.1f) + (Vector3.left * 0.1f), transform.rotation);
                        bulletL.name = "PlayerBulletA";

                        _rigidL = bulletL.GetComponent<Rigidbody2D>();
                        _rigidL.AddForce(Vector2.up * PLAYER_FIRE_FORCE, ForceMode2D.Impulse);

                        GameObject bulletR = Instantiate(_bulletA, transform.position + (Vector3.up * 0.1f) + (Vector3.right * 0.1f), transform.rotation);
                        bulletR.name = "PlayerBulletA";

                        _rigidR = bulletR.GetComponent<Rigidbody2D>();
                        _rigidR.AddForce(Vector2.up * PLAYER_FIRE_FORCE, ForceMode2D.Impulse);
                        break;
                    case 3:
                        GameObject bulletCC = Instantiate(_bulletB, transform.position + (Vector3.up * 0.1f), transform.rotation);
                        bulletCC.name = "PlayerBulletB";

                        _rigid = bulletCC.GetComponent<Rigidbody2D>();
                        _rigid.AddForce(Vector2.up * PLAYER_FIRE_FORCE, ForceMode2D.Impulse);

                        GameObject bulletLL = Instantiate(_bulletA, transform.position + (Vector3.up * 0.1f) + (Vector3.left * 0.3f), transform.rotation);
                        bulletLL.name = "PlayerBulletA";

                        _rigidL = bulletLL.GetComponent<Rigidbody2D>();
                        _rigidL.AddForce(Vector2.up * PLAYER_FIRE_FORCE, ForceMode2D.Impulse);

                        GameObject bulletRR = Instantiate(_bulletA, transform.position + (Vector3.up * 0.1f) + (Vector3.right * 0.3f), transform.rotation);
                        bulletRR.name = "PlayerBulletA";

                        _rigidR = bulletRR.GetComponent<Rigidbody2D>();
                        _rigidR.AddForce(Vector2.up * PLAYER_FIRE_FORCE, ForceMode2D.Impulse);
                        break;
                }

                _curShotDelay = 0;
            }
        }
    }

    void Reload()
    {
        _curShotDelay += Time.deltaTime;
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
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {

            if (_isHit)
                return;

            _isHit = true;
            _life--;
            _gManager.UpdateImgLife(_life);
            if (_life == 0)
            {
                _gManager.GameOver();
            }
            else
            {
                _gManager.ReSpawnPlayer();
            }
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
}
