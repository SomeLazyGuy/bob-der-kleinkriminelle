using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntity : MonoBehaviour {
    [SerializeField] private ItemData itemData;
    
    private GameObject selectIndicator;
    
    private void Awake() {
        selectIndicator = transform.Find("Highlight").gameObject;
        selectIndicator.SetActive(false);
    }

    public float GetWeight() {
        return itemData.weight;
    }

    public float GetValue() {
        return itemData.value;
    }
    
    public void SelectItem() {
        selectIndicator.SetActive(true);
    }

    public void DeselectItem() {
        selectIndicator.SetActive(false);
    }

    public void PickupItem() {
        gameObject.SetActive(false);
    }
}
