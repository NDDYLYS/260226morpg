using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RepeatButton : MonoBehaviour
{
    public UnityEvent onLongPress = new UnityEvent();

    private bool isPointerDown = false;
    private float timePressStarted = 1f;
    private float RepeatDelay = 0.16f;
    private float Delay = 0f;

    private int RepeatCount { get; set; }
    private float NewDelay { get; set; }

    private void OnDisable()
    {
        isPointerDown = false;
    }

    public void OnPointerDown()
    {
        timePressStarted = .5f;
        Delay = 0f;
        isPointerDown = true;

        RepeatCount = 0;
        NewDelay = RepeatDelay;
    }

    public void OnPointerUp()
    {
        isPointerDown = false;
    }


    public void OnPointerExit()
    {
        isPointerDown = false;
    }
    
    void Update()
    {
        if (isPointerDown)
        {
            if (0f < timePressStarted)
            {
                timePressStarted -= Time.deltaTime;
            }
            else
            {
                Delay += Time.deltaTime;
                RepeatCount++;
                if (12 <= RepeatCount)
                {
                    NewDelay *= .9f;
                    RepeatCount = 0;
                }
                
                if (NewDelay <= Delay)
                {
                    Delay = 0f;
                    onLongPress.Invoke();
                }
            }
        }
    }
}
