using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntity : MonoBehaviour {
    public ItemData itemData;
    private GameObject selectIndicator;
    private DisplayItemInfo displayItemInfo;
    public Vector3 itemPosition;

    private void Awake() {
        selectIndicator = transform.Find("Highlight").gameObject;
        selectIndicator.SetActive(false);
        displayItemInfo = FindObjectOfType<DisplayItemInfo>();
    }

    public Sprite GetSprite() => transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;

    public float GetWeight() => itemData.weight;

    public float GetValue() => itemData.value;
    
    public void SelectItem() {
        selectIndicator.SetActive(true);
        if (displayItemInfo != null)
        {
            displayItemInfo.itemData = itemData;
            displayItemInfo.itemPosition = transform.position;
            Debug.Log($"[SelectItem]: Logging pos: {transform.position} for  + gameObject.name");
            displayItemInfo.UpdateText();
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

    public ItemData GetItemData() => itemData;
}