using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI maxWeightText;
    [SerializeField] private TextMeshProUGUI tooHeavyText;
    [SerializeField] private float maxWeight;
    
    private float _currentWeight = 0;
    private float _currentValue = 0;

    private void Start() {
        tooHeavyText.gameObject.SetActive(false);
        maxWeightText.text = "Max weight: " + maxWeight;
    }
    
    public bool AddItem(ItemEntity itemEntity) {
        if (_currentWeight + itemEntity.GetWeight() > maxWeight) {
            StartCoroutine(ShowTooHeavyText());
            return false;
        }
        
        itemEntity.PickupItem();
        
        _currentWeight += itemEntity.GetWeight();
        weightText.text = "Weight: " + _currentWeight;
        
        _currentValue += itemEntity.GetValue();
        valueText.text = "Value: " + _currentValue;

        return true;
    }

    private IEnumerator ShowTooHeavyText() {
        tooHeavyText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        tooHeavyText.gameObject.SetActive(false);
    }
}
