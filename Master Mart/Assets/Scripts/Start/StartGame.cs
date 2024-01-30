using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject panelSetting;

    public void Setting()
    {
        panelSetting.SetActive(true);

        Vector3 startPosition;

        startPosition = panelSetting.transform.position;
        panelSetting.transform.position = new Vector2(startPosition.x, startPosition.y + 25f);
        panelSetting.transform.DOMoveY(startPosition.y, 0.3f).SetEase(Ease.OutQuad);

    }

    public void ExitSetting()
    {
        panelSetting.SetActive(false);


    }
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void StartMenuGame()
    {
        SceneManager.LoadScene(0);
    }


}
