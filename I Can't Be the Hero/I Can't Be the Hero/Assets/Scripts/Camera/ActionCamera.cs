using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCamera : MonoBehaviour
{
    [SerializeField] private float CameraTraceDistance;

    [SerializeField] private Transform cameraLookPoint;
    public Transform CameraLookPoint
    {
        get
        {
            if (cameraLookPoint == null)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                    cameraLookPoint = player.transform;
                return cameraLookPoint;
            }
            return null;
        }
        set
        {
            cameraLookPoint = value;
        }
    }

    private void Awake()
    {
    }

    private void Update()
    {
        UpdateNormalCamera();
    }



    private void UpdateNormalCamera()
    {
        if (CameraLookPoint == null)
            return;

        if (CameraLookPoint != null)
        {
            var diff = CameraLookPoint.position - transform.position;
            if (CameraTraceDistance <= diff.x)
                diff.x = 0f;
            if (CameraTraceDistance <= diff.y)
                diff.y = 0f;

            diff.z = 0f;

            transform.position += diff;
        }
    }
}