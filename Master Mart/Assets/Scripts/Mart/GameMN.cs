﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class ReadOnlyAttribute : PropertyAttribute { }
public class GameMN : MonoBehaviour
{
    [SerializeField] private Transform spawnItem;
    [SerializeField] private ItemScriptable itemScriptable;
    [SerializeField] private Transform parentObject;
    [SerializeField] private GameObject[] childObject;

    [SerializeField] private ItemPrefabs itemPrefabs;
    [SerializeField] private GameController gameController;
    [SerializeField] private PanelInGame panelInGame;
    private Dictionary<int, ItemPrefabs> itemDict;

    private int idChose = 0;
    [SerializeField] private int soLuong;

    [SerializeField] private List<Item> clonedItemsList;
    [SerializeField] private List<Item> listContents;

    private List<Item> itemlist;
    private List<Item> itemlist2;
    private List<Item> itemlist3;
    private List<Item> itemlist4;

    public int Level { get; private set; }
    private string key_level = "level";

    private int newId = 0; // Biến tạm để theo dõi ID mới

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetInt(key_level, 1);
        }
    }

    private void Awake()
    {
        itemlist = itemScriptable.Level1;
        itemlist2 = itemScriptable.Level2;
        itemlist3 = itemScriptable.Level3;
        itemlist4 = itemScriptable.Level4;
        Level = PlayerPrefs.GetInt(key_level, 1);
        CloneAndAddToNewList();
        panelInGame.StartCoroutine(panelInGame.StartCountdown(120f));
        Level = 1;
    }

    public void CloneAndAddToNewList()
    {
        panelInGame.LevelUpdate();

        int level = PlayerPrefs.GetInt(key_level, 1);

        switch (level)
        {
            case 1:
                ListLevel(itemlist);
                break;
            case 2:
                ListLevel(itemlist2);
                panelInGame.ItemPanel(true);
                break;
            case 3:
                ListLevel(itemlist3);
                panelInGame.ItemPanellv3(true);
                break;
            case 4:
                ListLevel(itemlist4);
                break;
            default:
                Debug.LogWarning("Unknown level: " + level);
                break;
        }


        /*
                panelInGame.LevelUpdate();

                if (PlayerPrefs.GetInt(key_level, 1) == 1)
                {
                    ListLevel1();
                }
                else if (PlayerPrefs.GetInt(key_level, 2) == 2)
                {
                    ListLevel2();
                    panelInGame.ItemPanel(true);
                }
                else if (PlayerPrefs.GetInt(key_level, 3) == 3)
                {
                    ListLevel3();
                    panelInGame.ItemPanellv3(true);


                }
                else if (PlayerPrefs.GetInt(key_level, 4) == 4)
                {
                    ListLevel4();
                }
        */
    }

    private void ListLevel(List<Item> itemList)
    {
        foreach (Item item in itemList)
        {
            for (int i = 0; i < soLuong; i++)
            {
                Item clonedItem = new Item(item);
                clonedItem.Id = newId++;
                clonedItemsList.Add(clonedItem);
            }
        }
        SpawnItem();
    }


    private void ListLevel1()
    {
        ListLevel(itemlist);
    }

    private void ListLevel2()
    {
        ListLevel(itemlist2);
    }

    private void ListLevel3()
    {
        ListLevel(itemlist3);
    }

    private void ListLevel4()
    {
        ListLevel(itemlist4);
    }
    private void SpawnItem()
    {
        itemDict = new();
        for (int i = 0; i < clonedItemsList.Count; i++)
        {
            Vector3 randomPosition = new Vector2(
                Random.Range(-spawnItem.position.x / 1.3f, spawnItem.position.x / 1.3f),
                Random.Range(-spawnItem.position.y / 2.2f, spawnItem.position.y / 2.2f));
            ItemPrefabs item = Instantiate(itemPrefabs, spawnItem.position + randomPosition, Quaternion.identity, spawnItem);
            itemDict.Add(clonedItemsList[i].Id, item);
            item.Init(clonedItemsList[i], ProcessItem);
            // Debug.Log(item.id);

        }
    }


    private void ProcessItem(Item item)
    {
        idChose = item.Id;
        item.Status = StatusState.onlist;

        if (listContents.Count < gameController.limit)
        {
            MoveItem(item);
        }
    }

    public void BackItem()
    {
        if (listContents.Count > 0)
        {
            Item itemLast = listContents[listContents.Count - 1];
            itemDict[idChose].gameObject.SetActive(true);
            listContents.Remove(itemLast);
            AddListContents();
        }
    }

    private void MoveItem(Item item)
    {
        listContents.Add(item);
        AddListContents();
        SoundMN.Instance.PlaySfx("AddList");
        StartCoroutine(RemoveItems(0.3f));
        if (itemDict.ContainsKey(idChose))
        {
            // remove item 
            /* GameObject gameObjectToRemove = itemDict[idChose].gameObject;
             itemDict.Remove(idChose); // Remove from the dictionary
             Destroy(gameObjectToRemove); // Destroy the game object*/

            itemDict[idChose].gameObject.SetActive(false);
            clonedItemsList.Remove(item);


        }
    }

    private void AddListContents()
    {
        for (int i = 0; i < listContents.Count; i++)
        {
            Image image = childObject[i].GetComponentInChildren<Image>();
            if (image != null)
            {
                image.sprite = listContents[i].Sprite;
            }
        }

        for (int i = listContents.Count; i < childObject.Length; i++)
        {
            Image image = childObject[i].GetComponentInChildren<Image>();
            if (image != null)
            {
                image.sprite = null;
            }
        }

    }

    IEnumerator RemoveItems(float time)
    {
        var grItem = listContents.GroupBy(item => item.Name);
        foreach (var group in grItem)
        {
            if (group.Count() >= 3)
            {
                listContents.RemoveAll(item => item.Name == group.Key);
                SoundMN.Instance.PlaySfx("ClearList");

            }
        }
        yield return new WaitForSeconds(time);
        AddListContents();
        LoadLevel();
    }

    public void LoadLevel()
    {
        if (clonedItemsList.Count == 0)
        {
            Level++;
            PlayerPrefs.SetInt(key_level, Level);
            CloneAndAddToNewList();
        }

    }
}

