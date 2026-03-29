using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Text;

public class StartManager : MonoBehaviour
{
    private static StartManager _Instance = null;
    public static StartManager Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = FindObjectOfType(typeof(StartManager)) as StartManager;
            return _Instance;
        }
    }

    [SerializeField] private GameObject continueBtn;

    private void Awake()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        UGSManager.Instance.create();
        GameManager.Instance.create();
    }

    public void refreshContinueBtn() 
    {
        continueBtn.SetActive(UGSManager.Instance.getReturnBool());
    }

    public void onClickNewGameButton() 
    { 
        GameManager.Instance.SaveData = new SaveData();
        GameManager.Instance.MovingScene("2Village");
        
    }

    public void onClickContinueButton() 
    {
        UIPrefabManager.Instance.SaveLoadPageProperty.OnClickOpenPageButton(SaveLoadEnum.Load);
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
