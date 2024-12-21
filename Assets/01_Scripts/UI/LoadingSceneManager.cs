using Unity.Loading;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private AsyncOperation async;
    private void Start()
    {
        async = SceneManager.LoadSceneAsync(Database.Instance.Destination);
    }

    void Update()
    {
        slider.value = async.progress;
    }
}
