using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    public void onClickNewGameButton() 
    { 
    }

    public void onClickContinueButton() 
    {
        UIPrefabManager.Instance.SaveLoadPageProperty.OnClickOpenPageButton();
    }

    public void onClickOptionButton() 
    { 
        UIPrefabManager.Instance.OptionPageProperty.OnClickOpenPageButton();
    }

    public void onClickExitButton() 
    {
        UIPrefabManager.Instance.MessageBoxProperty.OnClickOpenPageButton("GameExit", delegate (MessageBoxClick click)
        {
            if (click == MessageBoxClick.Confirm)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
            }
        });
    }
}
