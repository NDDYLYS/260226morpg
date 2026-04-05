using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCamera : MonoBehaviour
{
    [SerializeField] private Transform CameraLookPoint;
    [SerializeField] private float CameraTraceDistance;

    private void Awake()
    {
        if (CameraLookPoint == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                CameraLookPoint = player.transform;
        }
    }

    private void Update()
    {
        UpdateNormalCamera();
    }



    private void UpdateNormalCamera()
    {
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