using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] _enemyArr;
    public Transform[] _spawnPoints;    //소환 위치
    public List<Transform> _spList;
    public GameObject _spawnGroup;
    public GameObject _gObjPlayer;

    public Player player;
    public GameObject _canvas;
    public Text _txtScore;
    public Image[] _imgLifeArr;
    public Image[] _imgBombArr;
    public GameObject _groupGameOver;
    public ObjectManager _objMgr;

    public float _nextSpawnDelay;
    public float _curSpawnDelay;

    public List<Spawn> _spawnList;
    public int _spawnIndex;
    public bool _spawnEnd;

    const string RESOURCES_PREFABS_PATH = "Prefabs";
    const string RESOURCES_DATA_PATH = "Data";

    void Awake()
    {
        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        _objMgr = GameObject.FindGameObjectWithTag("ObjectManager").gameObject.GetComponent<ObjectManager>();
        _txtScore = _canvas.transform.Find("TxtScore").GetComponent<Text>();
        _imgLifeArr = new Image[]
        {
            _canvas.transform.Find("ImgLife").GetComponent<Image>()
            , _canvas.transform.Find("ImgLife2").GetComponent<Image>()
            , _canvas.transform.Find("ImgLife3").GetComponent<Image>()
        };
        _imgBombArr = new Image[]
        {
            _canvas.transform.Find("ImgBomb").GetComponent<Image>()
            , _canvas.transform.Find("ImgBomb2").GetComponent<Image>()
            , _canvas.transform.Find("ImgBomb3").GetComponent<Image>()
        };
        _groupGameOver = _canvas.transform.Find("GroupGameOver").gameObject;

        _spawnGroup = GameObject.FindGameObjectWithTag("SpawnGroup");
        _spList = new List<Transform>();
        _gObjPlayer = GameObject.FindGameObjectWithTag("Player");
        player = _gObjPlayer.GetComponent<Player>();

        _spawnList = new List<Spawn>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // InitEnemyArr();
        InitSpawnPoints();
        InitSpawnFile();

        //_nextSpawnDelay = 2;
    }

    // Update is called once per frame
    void Update()
    {
        _curSpawnDelay += Time.deltaTime;

        if (_curSpawnDelay > _nextSpawnDelay && _spawnEnd == false)
        {
            SpawnEnemy();
            // _nextSpawnDelay = UnityEngine.Random.Range(0.5f, 3.0f);
            _curSpawnDelay = 0;
        }
        
        _txtScore.text = string.Format("{0:n0}", player._score);
    }

    void InitSpawnFile()
    {
        // 1.변수 초기화
        _spawnList.Clear();
        _spawnIndex = 0;
        _spawnEnd = false;

        // 2.파일 읽기
        TextAsset textFile = Resources.Load<TextAsset>(RESOURCES_DATA_PATH + "/Stage01");
        StringReader _strR = new StringReader(textFile.text);

        while(_strR != null)
        {
            string line = _strR.ReadLine();
            // Debug.Log(line);
            if (line == null)
                break;

            Spawn spawnData = new Spawn();
            spawnData._delay = Convert.ToSingle(line.Split(',')[0]);
            spawnData._type  = line.Split(',')[1];
            spawnData._point = Convert.ToInt32(line.Split(',')[2]);
            _spawnList.Add(spawnData);
        }

        _strR.Close();
        _nextSpawnDelay = _spawnList[0]._delay;
    }
    
    void InitSpawnPoints()
    {
        // 상단에 위치
        MakeSpawnPoints("Point0", new Vector3(-1.8f, _spawnGroup.transform.position.y, 0.0f));
        MakeSpawnPoints("Point1", new Vector3(-0.9f, _spawnGroup.transform.position.y, 0.0f));
        MakeSpawnPoints("Point2", new Vector3(-0.0f, _spawnGroup.transform.position.y, 0.0f));
        MakeSpawnPoints("Point3", new Vector3(0.9f, _spawnGroup.transform.position.y, 0.0f));
        MakeSpawnPoints("Point4", new Vector3(1.8f, _spawnGroup.transform.position.y, 0.0f));
        MakeSpawnPoints("Point5", new Vector3(-5.0f, 3.0f, 0.0f));
        MakeSpawnPoints("Point6", new Vector3(-5.0f, 4.0f, 0.0f));
        MakeSpawnPoints("Point7", new Vector3(5.0f, 3.0f, 0.0f));
        MakeSpawnPoints("Point8", new Vector3(5.0f, 4.0f, 0.0f));

        _spawnPoints = _spList.ToArray();
    }

    void MakeSpawnPoints(string gObjNm, Vector3 trPos)
    {
        GameObject gObj0 = new GameObject(gObjNm);
        gObj0.transform.parent = _spawnGroup.transform;
        gObj0.transform.position = trPos;
        _spList.Add(gObj0.transform);
    }

    [Obsolete("This Method is deprecated")]
    void InitEnemyArr()
    {
        GameObject enemyL = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/EnemyL");
        GameObject enemyM = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/EnemyM");
        GameObject enemyS = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/EnemyS");
        _enemyArr = new GameObject[] { enemyL, enemyM, enemyS };
        // _enemyArr = new GameObject[] { enemyS, enemyS , enemyS };
    }

    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch (_spawnList[_spawnIndex]._type)
        {
            case "L":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "S":
                enemyIndex = 2;
                break;
            case "B":
                enemyIndex = 3;
                break;
        }

        // int ranEnemy = UnityEngine.Random.Range(0, 3);
        // int ranEnemy = 2;
        // int ranPoint = UnityEngine.Random.Range(0, 9);
        int enemyPoint = _spawnList[_spawnIndex]._point;

        /* 
        GameObject gObjEenemy = Instantiate(_enemyArr[ranEnemy], _spawnPoints[ranPoint].position, _spawnPoints[ranPoint].rotation);
        switch (ranEnemy) {
            case 0:
                gObjEenemy.name = "EnemyL";
                break;
            case 1:
                gObjEenemy.name = "EnemyM";
                break;
            case 2:
                gObjEenemy.name = "EnemyS";
                break;
        }
        */

        POOLING_OBJECT[] enemyObject = new POOLING_OBJECT[]{ POOLING_OBJECT.EnemyL, POOLING_OBJECT.EnemyM, POOLING_OBJECT.EnemyS, POOLING_OBJECT.Boss };
        // POOLING_OBJECT[] enemyObject = new POOLING_OBJECT[] { POOLING_OBJECT.EnemyS, POOLING_OBJECT.EnemyS, POOLING_OBJECT.EnemyS }; // TEST

        GameObject gObjEenemy = _objMgr.MakeObject(enemyObject[enemyIndex]);
        gObjEenemy.transform.position = _spawnPoints[enemyPoint].position;
        gObjEenemy.transform.rotation = _spawnPoints[enemyPoint].rotation;

        Rigidbody2D rigid = gObjEenemy.GetComponent<Rigidbody2D>();
        Enemy enemy = gObjEenemy.GetComponent<Enemy>();
        enemy._gObjPlayer = _gObjPlayer;
        enemy._objMgr = _objMgr;
        // Debug.Log($"{gObjEenemy.name} : {enemy._speed}");

        if (enemyPoint < 5)
        {
            rigid.velocity = new Vector2(0, enemy._speed * -1.0f);
        }
        else if (enemyPoint == 5 || enemyPoint == 6)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemy._speed, -1.0f);
        }
        else if (enemyPoint == 7 || enemyPoint == 8)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemy._speed * -1.0f, -1.0f);
        }

        // 인덱스증가
        _spawnIndex++;
        if (_spawnIndex == _spawnList.Count)
        {
            _spawnEnd = true;
            return;
        }

        // 딜레이 갱신
        _nextSpawnDelay = _spawnList[_spawnIndex]._delay;
    }


    public void UpdateImgLife(int life)
    {
        for (int i = 0; i < Player.PLAYER_BASIC_LIFE; i++)
        {
            _imgLifeArr[i].color = new Color(1, 1, 1, 0);
        }

        for (int i = 0; i < life; i++) {
            _imgLifeArr[i].color = new Color(1, 1, 1, 1);
        }
    }

    public void UpdateImgBomb(int bomb)
    {
        for (int i = 0; i < Player.PLAYER_MAX_BOMB; i++)
        {
            _imgBombArr[i].color = new Color(1, 1, 1, 0);
        }

        for (int i = 0; i < bomb; i++)
        {
            _imgBombArr[i].color = new Color(1, 1, 1, 1);
        }
    }

    public void ReSpawnPlayer()
    {
        Invoke("ReSpawnPlayerExe", 2.0f);
    }

    void ReSpawnPlayerExe()
    {
        _gObjPlayer.transform.position = Vector3.down * 3.5f;
        _gObjPlayer.SetActive(true);
        player._isHit = false;
    }
    public void GameOver()
    {
        _groupGameOver.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
}
