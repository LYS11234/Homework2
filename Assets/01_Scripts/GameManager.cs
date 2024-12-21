using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IObserver
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private Image hpImage;
    [SerializeField]
    private Image staminaImage;


    void Start()
    {
        playerController.RegisterObserver(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDead()
    {

    }

    public void HPCheck(float _hpPer)
    {
        hpImage.fillAmount = _hpPer;
    }

    public void StaminaCheck(float _staminaPer)
    {
        staminaImage.fillAmount = _staminaPer;
    }
    public void SettingButton()
    {
        GameObject prefab = Resources.Load<GameObject>("UIPrefab/Settings");

        Instantiate(prefab);
    }
}
