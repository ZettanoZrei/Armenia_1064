using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenuCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float speed;
    [SerializeField] private float offset;
    private float screenWidth;
    private float screenHeight;
    private Vector3 cameraPoint;

    public float left;
    public float right;
    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        print($"{screenWidth}x{screenHeight}");
    }

    // Update is called once per frame
    void Update()
    {
        cameraPoint = Input.mousePosition;
        if (screenWidth - cameraPoint.x < offset)
            MoveCamera(1);
        else if (cameraPoint.x - 0 < offset)
            MoveCamera(-1);
    }
    void MoveCamera(float xOffset)
    {
        _camera.transform.Translate(new Vector2(xOffset, 0) * speed * Time.deltaTime);

        //var x = _camera.transform.position.x + xOffset * Time.deltaTime;
        //_camera.transform.position = new Vector3(x, _camera.transform.position.y, -10);
    }
    //void MoveCamera(float xOffset)
    //{
    //    var x = Mathf.Clamp(_camera.transform.position.x + xOffset*Time.deltaTime, left, right);
    //    print(x);
    //    _camera.transform.position = new Vector3(x, _camera.transform.position.y, -10);
    //}

    

}
