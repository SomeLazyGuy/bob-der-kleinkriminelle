using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour {
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI maxWeightText;
    [SerializeField] private TextMeshProUGUI tooHeavyText;
    [SerializeField] private TextMeshProUGUI inventoryFullText;
    [SerializeField] private float maxWeight;

    private PlayerController player;
    private float _currentWeight = 0;
    private float _currentValue = 0;

    private void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        maxWeightText.text = "Max weight: " + maxWeight;
        inventoryManager.Initialize();
        inventoryManager.gameObject.SetActive(false);
    }
    
    private void Update() {
        // toggle inventory
        if (Input.GetKeyDown(KeyCode.Q)) {
            inventoryManager.gameObject.SetActive(!inventoryManager.gameObject.activeSelf);
            player.canMove = !inventoryManager.gameObject.activeSelf;
        }
    }
    
    public bool PickupItem(ItemEntity itemEntity) {
        if (_currentWeight + itemEntity.GetWeight() > maxWeight) {
            if (!tooHeavyText.IsActive()) StartCoroutine(ShowTooHeavyText());
            return false;
        }
        
        if (!inventoryManager.AddItem(itemEntity)) {
            if (!inventoryFullText.IsActive()) StartCoroutine(ShowInventoryFullText());
            return false;
        }
        
        itemEntity.PickupItem();
        
        _currentWeight += itemEntity.GetWeight();
        weightText.text = "Weight: " + _currentWeight;
        
        _currentValue += itemEntity.GetValue();
        valueText.text = "Value: " + _currentValue;

        return true;
    }

    public void DropItem(ItemEntity itemEntity) {
        itemEntity.gameObject.SetActive(true);
        
        _currentWeight -= itemEntity.GetWeight();
        weightText.text = "Weight: " + _currentWeight;
        
        _currentValue -= itemEntity.GetValue();
        valueText.text = "Value: " + _currentValue;
    }

    private IEnumerator ShowTooHeavyText() {
        tooHeavyText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        tooHeavyText.gameObject.SetActive(false);
    }
    
    private IEnumerator ShowInventoryFullText() {
        inventoryFullText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        inventoryFullText.gameObject.SetActive(false);
    }
}
