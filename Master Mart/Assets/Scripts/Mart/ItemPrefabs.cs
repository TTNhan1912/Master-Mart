using UnityEngine;
using UnityEngine.UI;

public class ItemPrefabs : MonoBehaviour
{
    [SerializeField]
    private Image image;
    private Item item;
    private System.Action<Item> onClick;

    public void Init(Item item, System.Action<Item> onclick)
    {
        image.sprite = item.Sprite;
        this.item = item;
        this.onClick = onclick;
        item.Status = StatusState.none;

    }

    public void OnClick()
    {
        onClick(item);

    }



}
