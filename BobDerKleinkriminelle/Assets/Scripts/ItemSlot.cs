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
    
    public void SetItem(ItemEntity itemEntity) {
        this.itemEntity = itemEntity;
        itemImage.sprite = itemEntity.GetSprite();
        weightText.text = itemEntity.itemData.weight.ToString(CultureInfo.CurrentCulture);
        valueText.text = itemEntity.itemData.value.ToString(CultureInfo.CurrentCulture);
    }

    public void RemoveItem() {
        gameObject.SetActive(false);
        itemEntity = null;
    }
}
