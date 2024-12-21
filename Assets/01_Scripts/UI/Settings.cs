using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    Dropdown dropdown;
    public void SetPipeline(int value) 
    { 

        Database.Instance.GraphicOption = value;
        Debug.Log(value);
        Database.Instance.SetGraphicQuality();
    }

    public void Close()
    {
        Destroy(GetComponentInParent<Canvas>().gameObject);
    }
}
