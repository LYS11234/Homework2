using UnityEngine;
using UnityEngine.SceneManagement;


public class StartSceneConstroller : MonoBehaviour
{
    

    public void StartButton()
    {
        SceneManager.LoadSceneAsync("01_LoadingScene");
        Database.Instance.Destination = "GameScene";
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SettingButton()
    {
        GameObject prefab = Resources.Load<GameObject>("UIPrefab/Settings");

        Instantiate(prefab);
    }
}
