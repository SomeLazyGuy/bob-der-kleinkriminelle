using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject gridContainer;

    private ItemSlot[] _itemSlots = new ItemSlot[10];
    private int _firstFreeSlot = 0;

    public void Initialize() {
        for (int i = 0; i < 10; i++) {
            int j = i;
            ItemSlot itemSlot = Instantiate(itemSlotPrefab, gridContainer.transform).GetComponent<ItemSlot>();
            itemSlot.dropButton.onClick.AddListener(() => {
                DropItem(j);
            });
            _itemSlots[i] = itemSlot;
        }
    }

    public bool AddItem(ItemEntity itemEntity) {
        if (_firstFreeSlot >= _itemSlots.Length) return false;
        
        _itemSlots[_firstFreeSlot].SetItem(itemEntity);
        _itemSlots[_firstFreeSlot].gameObject.SetActive(true);
        _firstFreeSlot++;
        
        return true;
    }

    public void DropItem(int index) {
        gameController.DropItem(_itemSlots[index].itemEntity);
        _itemSlots[index].RemoveItem();
        _itemSlots[index].gameObject.SetActive(false);
        _firstFreeSlot--;
    }
}
