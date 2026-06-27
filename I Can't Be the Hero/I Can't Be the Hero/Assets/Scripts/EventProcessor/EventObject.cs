

public class EventObject : EventProcessor
{
    void OnEnable()
    {
        base.AddAction();
    }

    void OnDisable()
    {
        base.RemoveAction();
    }

    public override void EventProcessorMethod(EventKind _eventType)
    {
        if (EventTypeList.Contains(_eventType))
            LogManager.Instance.DebugLogCategory(LogCategoryEnum.Etc, string.Format("<color=blue>{0} is True. {1}</color>", name, _eventType));
        else
            LogManager.Instance.DebugLogCategory(LogCategoryEnum.Etc, string.Format("<color=red>{0} is False. {1}</color>", name, _eventType));
    }
}