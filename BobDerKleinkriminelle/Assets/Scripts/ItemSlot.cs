using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private TextMeshProUGUI valueText;
    
    public Button dropButton;
    
    [HideInInspector] public ItemEntity itemEntity;
    
    public void SetItem(ItemEntity itemEntity, int weight, int value) {
        this.itemEntity = itemEntity;
        itemImage.sprite = itemEntity.GetSprite();
        weightText.text = weight.ToString(CultureInfo.CurrentCulture);
        valueText.text = value.ToString(CultureInfo.CurrentCulture);
    }

    public void RemoveItem() {
        gameObject.SetActive(false);
        itemEntity = null;
    }
}
