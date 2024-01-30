using System.Collections.Generic;
using UnityEngine;

public enum StatusState
{
    none, onlist
}

[System.Serializable]
public class Item
{
    [SerializeField]
    private int id;

    [SerializeField]
    private string name;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private StatusState status;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public StatusState Status { get => status; set => status = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }

    public Item(Item other)
    {
        this.Id = other.Id;
        this.Name = other.Name;
        this.Sprite = other.Sprite;
        this.Status = other.Status;
    }
}


[CreateAssetMenu(fileName = "ItemScriptable", menuName = "Items")]

public class ItemScriptable : ScriptableObject
{
    [SerializeField] private List<Item> level1;
    [SerializeField] private List<Item> level2;
    [SerializeField] private List<Item> level3;
    [SerializeField] private List<Item> level4;

    public List<Item> Level1 { get => level1; set => level1 = value; }
    public List<Item> Level2 { get => level2; set => level2 = value; }
    public List<Item> Level3 { get => level3; set => level3 = value; }
    public List<Item> Level4 { get => level4; set => level4 = value; }
}
