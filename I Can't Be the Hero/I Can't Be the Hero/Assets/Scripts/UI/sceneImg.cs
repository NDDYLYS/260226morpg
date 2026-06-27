using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sceneImg : MonoBehaviour
{
    private void Awake()
    {
        var img = GetComponent<Image>();
        if (img == null )
            return;
        StartCoroutine(GameManager.Instance.setImg(img));
    }
}