using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<Cinemachine.CinemachineVirtualCamera>().m_Lens.OrthographicSize *= ((16/9) / Camera.main.aspect);

    }
}
