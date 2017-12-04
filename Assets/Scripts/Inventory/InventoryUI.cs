﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
    public static InventoryUI instance;
    public Transform itemsParent;
    Inventory inventory;
    public bool canChange { get; set; }
    public InventorySlot[] slots { get; set; }

    int sum;
    private void Start()
    {
        instance = this;
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        
    }

    private void Update()
    {
        

    }
   /* void Function()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isSelected)
                sum++;
        }
    }*/
    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
                slots[i].ClearSlot();
        }
        //Debug.Log("UpdateUI");
    }
    public void ChangeInventoryList()
    {
        Item[] arr = new Item[slots.Length];
        for(int i = 0; i < slots.Length; i++)
        {
            arr[i] = slots[i].GetItem(); 
        }
        inventory.ChangeItems(arr);
    }
    
}