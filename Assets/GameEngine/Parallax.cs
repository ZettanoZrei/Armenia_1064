using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed;
    private float lastCameraX;
    public Transform cameraTransform;
    bool locker = true;
    void Start()
    {
        StartCoroutine(Delay());
    }

    void Update()
    {
        //это костыль, дл€ решени€ проблемы того что при включении игры из сцены не путешестви€, первые несколько кадров
        //координаты х камеры приход€т неверные. ƒл€ этого задержка в получении координат камеры
        if (locker) 
            return;

        float deltaX = cameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * (deltaX * speed);
        lastCameraX = cameraTransform.position.x;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        lastCameraX = cameraTransform.position.x;
        locker = false;
    }
}
