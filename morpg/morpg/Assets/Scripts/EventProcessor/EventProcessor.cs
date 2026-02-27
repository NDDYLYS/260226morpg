using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class EventProcessor : MonoBehaviour
{
    public GameObject Container;

    [HideInInspector]
    public List<EventKind> EventTypeList { get; set; }

    public virtual void AddAction()
    {
        GameManager.Instance.EventAction += EventProcessorMethod;
    }

    public virtual void RemoveAction()
    {
        GameManager.Instance.EventAction -= EventProcessorMethod;
    }

    public virtual void EventProcessorMethod(EventKind _eventType)
    {
    }

    public virtual void OpenPage(bool _not = false)
    {
        GameManager.Instance.AddUI(this);
        Container.SetActive(true);

        if (_not)
        {
            UIPrefabManager.Instance.UIPage(true);
            Container.transform.localScale = Vector3.one * 0.8f;
            Container.transform.DOScale(1f, .5f).SetEase(Ease.OutExpo).onComplete += () =>
            {
                Container.transform.localScale = Vector3.one;
                UIPrefabManager.Instance.UIPage(false);
            };
        }
    }

    public virtual void ClosePage()
    {
        GameManager.Instance.RemoveUI(this);
        Container.SetActive(false);
    }

    public virtual void EscapeKeyDown()
    {
        // None
    }
}
