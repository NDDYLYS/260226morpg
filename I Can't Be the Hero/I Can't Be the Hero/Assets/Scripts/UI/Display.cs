using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = string.Format("{0}:{1}", Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
