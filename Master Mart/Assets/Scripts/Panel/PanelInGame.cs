using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class PanelInGame : MonoBehaviour
{
    public GameMN gameMN;
    public GameController gameController;
    [SerializeField] private TMP_Text textLevel;
    [SerializeField] private TMP_Text textCoins;
    [SerializeField] private TMP_Text textTimer;
    [SerializeField] private GameObject setting;
    [SerializeField] private GameObject newItemPanel;
    [SerializeField] private GameObject newItemPanellv2;
    [SerializeField] private GameObject newItemPanellv3;
    public Coroutine countdownCoroutine;
    public void LevelUpdate()
    {
        if (textLevel != null)
        {
            textLevel.text = "Level: " + gameMN.Level.ToString();
        }
    }

    public void Coins()
    {
        if (textCoins != null)
        {
            textCoins.text = "Coins: " + gameController.Coins.ToString();
        }
    }

    public IEnumerator StartCountdown(float countdownTime)
    {
        float currentTime = countdownTime;

        while (currentTime > 0)
        {
            UpdateCountdownText(currentTime);
            yield return new WaitForSeconds(1f);

            currentTime -= 1f;
        }

        CountdownFinished();
    }

    private void UpdateCountdownText(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);

        textTimer.text = "Time " + string.Format("{0:D1}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

    private void CountdownFinished()
    {
        // to do something :))
    }
    public void ItemPanel(bool isActive)
    {
        newItemPanel.SetActive(isActive);
        newItemPanellv2.SetActive(isActive);

    }
    public void ItemPanellv3(bool isActive)
    {
        newItemPanel.SetActive(isActive);
        newItemPanellv3.SetActive(isActive);

    }

    public void Claim()
    {
        newItemPanel.SetActive(false);
        newItemPanellv2.SetActive(false);
        SoundMN.Instance.PlaySfx("Click");


    }
    public void Setting()
    {
        setting.SetActive(true);

    }
    public void ExitSetting()
    {
        setting.SetActive(false);
    }


}

