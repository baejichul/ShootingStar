using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] _enemyArr;
    public Transform[] _spawnPoints;    //소환 위치
    public List<Transform> _spList;
    public GameObject _spawnGroup;
    public GameObject _gObjPlayer;

    public float _maxSpawnDelay;
    public float _curSpawnDelay;

    const string RESOURCES_PREFABS_PATH = "Prefabs";

    void Awake()
    {
        _spawnGroup = GameObject.FindGameObjectWithTag("SpawnGroup");
        _spList = new List<Transform>();
        _gObjPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        InitEnemyArr();
        InitSpawnPoints();

        _maxSpawnDelay = 2;

    }

    // Update is called once per frame
    void Update()
    {
        _curSpawnDelay += Time.deltaTime;

        if (_curSpawnDelay > _maxSpawnDelay)
        {
            SpawnEnemy();
            _maxSpawnDelay = Random.Range(0.5f, 3.0f);
            _curSpawnDelay = 0;
        }
    }

    void InitSpawnPoints()
    {
        // 상단에 위치
        MakeSpawnPoints("Point0", new Vector3(-1.8f, _spawnGroup.transform.position.y, 0.0f));
        MakeSpawnPoints("Point1", new Vector3(-0.9f, _spawnGroup.transform.position.y, 0.0f));
        MakeSpawnPoints("Point2", new Vector3(-0.0f, _spawnGroup.transform.position.y, 0.0f));
        MakeSpawnPoints("Point3", new Vector3(0.9f, _spawnGroup.transform.position.y, 0.0f));
        MakeSpawnPoints("Point4", new Vector3(1.8f, _spawnGroup.transform.position.y, 0.0f));
        MakeSpawnPoints("Point5", new Vector3(-5.0f, 2.0f, 0.0f));
        MakeSpawnPoints("Point6", new Vector3(-5.0f, 3.0f, 0.0f));
        MakeSpawnPoints("Point7", new Vector3(5.0f, 2.0f, 0.0f));
        MakeSpawnPoints("Point8", new Vector3(5.0f, 3.0f, 0.0f));

        _spawnPoints = _spList.ToArray();
    }

    void MakeSpawnPoints(string gObjNm, Vector3 trPos)
    {
        GameObject gObj0 = new GameObject(gObjNm);
        gObj0.transform.parent = _spawnGroup.transform;
        gObj0.transform.position = trPos;
        _spList.Add(gObj0.transform);
    }

    void InitEnemyArr()
    {
        GameObject enemyL = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/EnemyL");
        GameObject enemyM = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/EnemyM");
        GameObject enemyS = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/EnemyS");
        _enemyArr = new GameObject[] { enemyL, enemyM, enemyS };
    }

    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        // int ranEnemy = 2;
        int ranPoint = Random.Range(0, 9);

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

        Rigidbody2D rigid = gObjEenemy.GetComponent<Rigidbody2D>();
        Enemy enemy = gObjEenemy.GetComponent<Enemy>();
        enemy._gObjPlayer = _gObjPlayer;
        // Debug.Log($"{gObjEenemy.name} : {enemy._speed}");

        if (ranPoint < 5)
        {
            rigid.velocity = new Vector2(0, enemy._speed * -1.0f);
        }
        else if (ranPoint == 5 || ranPoint == 6)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemy._speed, -1.0f);
        }
        else if (ranPoint == 7 || ranPoint == 8)
        {
            enemy.transform.Rotate(Vector3.back*90);
            rigid.velocity = new Vector2(enemy._speed * -1.0f, -1.0f);
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
    }
}
