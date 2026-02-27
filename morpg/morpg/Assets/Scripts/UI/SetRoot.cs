using UnityEngine;



public class SetRoot : MonoBehaviour
{
    [SerializeField] private Transform Dimmed;

    private void Awake()
    {
        UIPrefabManager.Instance.SetRoot(this.transform, Dimmed);
    }
}