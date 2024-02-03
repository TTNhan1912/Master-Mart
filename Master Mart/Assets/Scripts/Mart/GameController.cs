using DG.Tweening;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Coins { get; set; }
    public string key_coin;
    [SerializeField] private PanelInGame panelInGame;
    [SerializeField] private GameObject plus;
    [SerializeField] private GameObject panelAddSlot;
    public int limit = 7;

    private void Awake()
    {
        Coins = PlayerPrefs.GetInt(key_coin, 10000);
        panelInGame.Coins();

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            PlayerPrefs.SetInt(key_coin, 10000);
        }
    }

    public void PanelAddSlot()
    {
        panelAddSlot.SetActive(true);
        SoundMN.Instance.PlaySfx("Click");

        Vector3 startPosition;

        startPosition = panelAddSlot.transform.position;
        panelAddSlot.transform.position = new Vector2(startPosition.x, startPosition.y + 25f);
        panelAddSlot.transform.DOMoveY(startPosition.y, 0.3f).SetEase(Ease.OutQuad);
    }

    public void ExitPanel()
    {
        panelAddSlot.SetActive(false);
        SoundMN.Instance.PlaySfx("Click");

    }
    public void AddSlot()
    {
        if (PlayerPrefs.GetInt(key_coin, 10000) >= 500)
        {
            plus.SetActive(false);
            SoundMN.Instance.PlaySfx("Click");

            limit = 8;
            Coins -= 500;
            PlayerPrefs.SetInt(key_coin, Coins);
        }
        ExitPanel();
        panelInGame.Coins();

    }

}
