using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntity : MonoBehaviour {
    public ItemData itemData;
    private GameObject selectIndicator;
    private DisplayItemInfo displayItemInfo;
    public Vector3 itemPosition;

    private int _weight;
    private int _value;
    
    private void Awake() {
        _weight = itemData.weight;
        _value = itemData.value;
        
        selectIndicator = transform.Find("Highlight").gameObject;
        selectIndicator.SetActive(false);
        displayItemInfo = GameObject.FindWithTag("DisplayItemInfoTag").GetComponent<DisplayItemInfo>();
    }

    public Sprite GetSprite() => transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;

    public int GetWeight() => _weight;

    public int GetValue() => _value;

    public void SetWeight(int weight) {
        _weight = weight;
    }

    public void SetValue(int value) {
        _value = value;
    }
    
    public void SelectItem() {
        selectIndicator.SetActive(true);
        if (displayItemInfo != null) {
            // Debug.Log($"[SelectItem]: Logging pos: {transform.position} for  + {gameObject.name}");
            displayItemInfo.UpdateText(_weight, _value, transform.position);
        }
    }

    public void DeselectItem() {
        selectIndicator.SetActive(false);
        if(displayItemInfo != null){
            displayItemInfo.HideText();
        }
    }

    public void PickupItem() {
        gameObject.SetActive(false);
    }
}