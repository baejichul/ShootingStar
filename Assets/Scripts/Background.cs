using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float _speed;
    public int _startIndex;
    public int _endIndex;
    public Transform[] _tfArr;

    float _cameraHeight;
    float _cameraWidth;

    void Awake()
    {
        // 패럴렉스 구현(원근감 표시)
        if (gameObject.name.EndsWith("A"))
            _speed = 4;
        else if (gameObject.name.EndsWith("B"))
            _speed = 2;
        else if (gameObject.name.EndsWith("C"))
            _speed = 1;

        _startIndex = 2;    // BackgroundC index
        _endIndex = 0;      // BackgroundA index

        // 카메라 크기
        _cameraHeight = 2 * Camera.main.orthographicSize;
        _cameraWidth = _cameraHeight * Camera.main.aspect;
        // Debug.Log($"Camera = {_cameraWidth} * {_cameraHeight}");

        
        _tfArr = new Transform[] {
            gameObject.transform.GetChild(0)
            , gameObject.transform.GetChild(1)
            , gameObject.transform.GetChild(2)
        };

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 curPos  = transform.position;
        Vector3 nextPos = Vector3.down * _speed * Time.deltaTime;
        // Debug.Log($"nextPos = {nextPos}");

        transform.position = curPos + nextPos;

        if (_tfArr[_endIndex].position.y < _cameraHeight * -1.0f)
        {
            Vector3 backPos  = _tfArr[_startIndex].localPosition;    // 제일 위 localPosition
            // Vector3 frontPos = _tfArr[_endIndex].localPosition;   // 제일 아래 localPosition
            _tfArr[_endIndex].localPosition = backPos + Vector3.up * _cameraHeight;

            int startIndexSave = _startIndex;
            _startIndex = _endIndex;
            _endIndex = (startIndexSave - 1 == -1) ? _tfArr.Length-1 : startIndexSave - 1;

            // 2,0
            // 0,1(2-1=2)
            // 1,2(0-1=-1 => 3-1=2)
            // 2,0(1-1=0)
            // ...
        }
    }
}
