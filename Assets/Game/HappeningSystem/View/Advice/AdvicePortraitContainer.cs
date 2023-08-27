using UnityEngine;

public class AdvicePortraitContainer : MonoBehaviour
{
    [SerializeField] private int number;
    public int Number => number;

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
    public void SetPortrait(PortraitButton portrait)
    {
        portrait.transform.parent = this.transform;
        portrait.SetTransformSettings(Vector3.zero);
        portrait.SetAcivate(true);
    }

}
