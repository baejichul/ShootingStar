using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] _enemyArr;
    public Transform[] _spawnPoints;    //소환 위치
    public GameObject _spawnGroup;

    public float _maxSpawnDelay;
    public float _curSpawnDelay;

    const string RESOURCES_PREFABS_PATH = "Prefabs";

    void Awake()
    {
        _spawnGroup = GameObject.FindGameObjectWithTag("SpawnGroup");
    }

    // Start is called before the first frame update
    void Start()
    {
        InitSpawnPoints();
        InitEnemyArr();

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
        GameObject gObj0 = new GameObject("Point0");        
        gObj0.transform.parent = _spawnGroup.transform;
        gObj0.transform.position = new Vector3(-1.8f, _spawnGroup.transform.position.y, 0.0f);

        GameObject gObj1 = new GameObject("Point1");
        gObj1.transform.parent = _spawnGroup.transform;
        gObj1.transform.position = new Vector3(-0.9f, _spawnGroup.transform.position.y, 0.0f);

        GameObject gObj2 = new GameObject("Point2");
        gObj2.transform.parent = _spawnGroup.transform;
        gObj2.transform.position = new Vector3(-0.0f, _spawnGroup.transform.position.y, 0.0f);

        GameObject gObj3 = new GameObject("Point3");
        gObj3.transform.parent = _spawnGroup.transform;
        gObj3.transform.position = new Vector3(0.9f, _spawnGroup.transform.position.y, 0.0f);

        GameObject gObj4 = new GameObject("Point4");
        gObj4.transform.parent = _spawnGroup.transform;
        gObj4.transform.position = new Vector3(1.8f, _spawnGroup.transform.position.y, 0.0f);

        _spawnPoints = new Transform[]{ gObj0.transform, gObj1.transform, gObj2.transform, gObj3.transform, gObj4.transform};
    }

    void InitEnemyArr()
    {
        GameObject enemyL = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/EnemyL");
        GameObject enemyM = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/EnemyM");
        GameObject enemyS = Resources.Load<GameObject>(RESOURCES_PREFABS_PATH + "/EnemyS");
        _enemyArr = new GameObject[]{ enemyL, enemyM, enemyS };
    }

    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 5);

        GameObject enemy = Instantiate(_enemyArr[ranEnemy], _spawnPoints[ranPoint].position, _spawnPoints[ranPoint].rotation);
        switch(ranEnemy){
            case 0:
                enemy.name = "EnemyL";
                break;
            case 1:
                enemy.name = "EnemyM";
                break;
            case 2:
                enemy.name = "EnemyS";
                break;
        }
    }
}
