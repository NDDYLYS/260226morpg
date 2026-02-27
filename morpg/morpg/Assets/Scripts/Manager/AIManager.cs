using System.Collections.Generic;
using UnityEngine;



public class AIManager : SingletonGameObject<AIManager>
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}