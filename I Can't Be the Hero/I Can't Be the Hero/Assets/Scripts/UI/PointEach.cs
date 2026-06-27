using UnityEngine;
using UnityEngine.UI;

public class PointEach : MonoBehaviour
{
    public Text Text;
    public Slider Slider;

    public string TextValue;
    public bool IsPercentage = false;

    private long TargetPoint;
    private long MaxTargetPoint;

    [Space(5)]
    public int Rate;
    public float EmergencyValue;
    public Color UsualColor;
    public Color EmergencyColor;

    private long FollowerPoint { get; set; }
    private bool IsUpdate { get; set; }
    
    public void SetPointEach(long _target, long _max)
    {
        TargetPoint = _target;
        MaxTargetPoint = _max;

        IsUpdate = true;
    }

    public void SetPointEach(string _target)
    {
        Color color = UsualColor;
        if (0 < FollowerPoint && 0 < EmergencyValue && ((float)FollowerPoint / (float)MaxTargetPoint) < (EmergencyValue / 100f))
        {
            color = EmergencyColor;
        }

        Text.color = color;
        Text.text = string.Format("{0} {1}", TextValue, _target);

        IsUpdate = false;
    }

    void Update()
    {
        if (!IsUpdate)
            return;

        long diff = TargetPoint - FollowerPoint;
        if (Rate <= 0)
            Rate = 10;

        if (0 <= diff)
        {
            // 늘어날 때
            int debugValue = (int)Mathf.Ceil(diff / Rate);
            if (1 <= debugValue)
                FollowerPoint += debugValue;
            else
                FollowerPoint += diff;
        }
        else
        {
            // 줄어들 때
            int debugValue = (int)Mathf.Ceil(diff / Rate);
            if (debugValue < 0)
                FollowerPoint += debugValue;
            else
                FollowerPoint += diff;
        }

        if (FollowerPoint < 0)
            FollowerPoint = 0;

        Color color = UsualColor;
        if (0 < FollowerPoint && 0 < EmergencyValue && ((float)FollowerPoint / (float)MaxTargetPoint) < (EmergencyValue / 100f))
        {
            color = EmergencyColor;
        }

        if (Text != null)
        {
            string followerPoint = Util.GetComma(FollowerPoint);
            string maxTargetPoint = Util.GetComma(MaxTargetPoint);

            string text = string.Empty;
            Text.color = color;

            text = string.Format("{0} {1}", TextValue, followerPoint);

            if (0 < MaxTargetPoint)
            {
                if (IsPercentage)
                    text = string.Format("{0}/{1} ({2:0}%)", text, maxTargetPoint, ((float)FollowerPoint / (float)MaxTargetPoint) * 100f);
                else
                    text = string.Format("{0}/{1}", text, maxTargetPoint);
            }

            Text.text = text;
        }
        if (Slider != null && 0 < MaxTargetPoint)
        {
            Slider.value = ((float)FollowerPoint / (float)MaxTargetPoint);
        }
    }
}