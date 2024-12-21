using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Database : MonoBehaviour
{
    public static Database Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string Destination;
    public int GraphicOption;

    [SerializeField]
    private List<RenderPipelineAsset> rendererPipelineAsset;




    public void SetGraphicQuality()
    {
        QualitySettings.SetQualityLevel(GraphicOption);
    }
}
