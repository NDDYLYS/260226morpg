using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Shadow : MonoBehaviour
{
    public GameObject Image;
    public TextMeshProUGUI Text;

    void Awake()
    {
        PlayShadow();
    }

    public void PlayShadow()
    {
        StartCoroutine("ShadowCoroutine");
    }

    private IEnumerator ShadowCoroutine()
    {
        //int chapter = (GameManager.Instance.SaveData.InstantDungeon == null) ? 0 : GameManager.Instance.SaveData.InstantDungeon.Chapter;
        //Image.SetActive(true);
        //Text.gameObject.SetActive(true);
        //Text.text = string.Empty;

        //Table_Map map = TableDataManager.Instance.GetMapForChater(chapter);
        //yield return new WaitForSeconds(2f);
        //Text.text = TableDataManager.Instance.GetTableText(map.Name);

        yield return new WaitForSeconds(1.5f);

        //Text.text = string.Empty;
        //Text.gameObject.SetActive(false);
        //Image.SetActive(false);

        //GameManager.Instance.OccurEvent(EventKind.MonsterAppear);
        //GameManager.Instance.OccurEvent(EventKind.PlayTimeStart);

        //GameManager.Instance.ChangedGameState(GameState.Play);
    }
}
