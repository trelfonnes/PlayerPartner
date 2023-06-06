using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public static class CameraSwitcher
{
    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    public static CinemachineVirtualCamera ActiveCamera = null;
    
    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == ActiveCamera;
    }

    public static void SwitchCamera(CinemachineVirtualCamera camera)
    {
        camera.Priority = 10;
        ActiveCamera = camera;
        Debug.Log("Camera Switched");
        foreach (CinemachineVirtualCamera c  in cameras)
        {
            if(c != camera)
            {
                c.Priority = 0;
            }
        }
    }

    public static void Register(CinemachineVirtualCamera cameraInUse)
    {
        cameras.Add(cameraInUse);
        Debug.Log("Camera Registered" + cameraInUse);
    }
    public static void UnRegister(CinemachineVirtualCamera cameraNotInUse)
    {

    }
    
   
}
