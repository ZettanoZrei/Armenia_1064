using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;
using Cinemachine;
using System.Collections;

//из за того что камеры смотрят иногда в разные стороны другие ui элементы переворачиваются
class RotationCauseCameraComponent : MonoBehaviour
{
    public void Rotate()
    {
        var cameras = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
        var liveCamera = cameras.FirstOrDefault(x => CinemachineCore.Instance.IsLive(x));
        if (liveCamera is null)
            Logger.WriteLog("active camera not found");
        var angle = liveCamera != null ? liveCamera.transform.eulerAngles.y : 0;
        
        //this.transform.Rotate(0, angle, 0);
        //transform.rotation*= new Quaternion(0, angle, 0, 0);
    }
}

