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
    [SerializeField] private int maxWeight;
    [SerializeField] private bool isGreedy;

    private PlayerController player;
    private int _currentWeight = 0;
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

            if (inventoryManager.gameObject.activeSelf) {
                player.DisableMovement();
            } else {
                player.EnableMovement();
            }
        }
    }

    public bool PickupItem(ItemEntity itemEntity) {
        if (isGreedy) {
            return PickUpItemGreedy(itemEntity);
        }
        
        return PickUpItem01(itemEntity);
    }

    public void DropItem(ItemEntity itemEntity) {
        itemEntity.gameObject.SetActive(true);

        int weightDifference = itemEntity.itemData.weight - itemEntity.GetWeight();
        int valueDifference = itemEntity.itemData.value - itemEntity.GetValue();

        if (weightDifference == 0 && valueDifference == 0) {
            _currentWeight -= itemEntity.GetWeight();
            _currentValue -= itemEntity.GetValue();
        } else {
            _currentWeight -= weightDifference;
            _currentValue -= valueDifference;
            
            itemEntity.SetWeight(itemEntity.GetWeight() + weightDifference);
            itemEntity.SetValue(itemEntity.GetValue() + valueDifference);

            itemEntity.transform.localScale = Vector3.one;
        }
        
        weightText.text = "Weight: " + _currentWeight;
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

    private bool PickUpItemGreedy(ItemEntity itemEntity) {
        if (_currentWeight == maxWeight) {
            if (!tooHeavyText.IsActive()) StartCoroutine(ShowTooHeavyText());
            return false;
        }

        if (_currentWeight + itemEntity.GetWeight() <= maxWeight) {
            return PickUpItem01(itemEntity);
        }

        int oldItemWeight = itemEntity.GetWeight();
        int oldItemValue = itemEntity.GetValue();

        int pickupWeight = maxWeight - _currentWeight;
        int pickupValue = oldItemValue / oldItemWeight * pickupWeight;

        int remainingWeight = oldItemWeight - pickupWeight;
        int remainingValue = oldItemValue - pickupValue;

        itemEntity.SetWeight(remainingWeight);
        itemEntity.SetValue(remainingValue);
        itemEntity.SelectItem();
        
        if (!inventoryManager.AddItem(itemEntity, pickupWeight, pickupValue)) {
            itemEntity.SetWeight(oldItemWeight);
            itemEntity.SetValue(oldItemValue);
            itemEntity.SelectItem();
            
            if (!inventoryFullText.IsActive()) StartCoroutine(ShowInventoryFullText());
            return false;
        }
        
        /*
         * Das scalen ist erstmal provisorisch.
         * Müssen wir mal entscheiden, ob wir das so lassen wollen, oder ob man das lieber durch einen Text oder so ersetzt.
         * Text wäre wahrscheinlich bischen mehr Aufwand.
         */
        itemEntity.transform.localScale *= 100f / oldItemWeight * remainingWeight / 100f;
        
        _currentWeight += pickupWeight;
        weightText.text = "Weight: " + _currentWeight;
        
        _currentValue += pickupValue;
        valueText.text = "Value: " + _currentValue;
        
        return true;
    }

    private bool PickUpItem01(ItemEntity itemEntity) {
        if (_currentWeight + itemEntity.GetWeight() > maxWeight) {
            if (!tooHeavyText.IsActive()) StartCoroutine(ShowTooHeavyText());
            return false;
        }
        
        if (!inventoryManager.AddItem(itemEntity, itemEntity.GetWeight(), itemEntity.GetValue())) {
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
}
