using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour {
    [SerializeField] private Transform itemParent;
    [SerializeField] private GameObject itemTemplate;
    [SerializeField] private Transform list;
    
    private ItemEntity[] _items;

    private void Start() {
        _items = itemParent.GetComponentsInChildren<ItemEntity>();

        foreach (var item in _items) {
            GameObject itemObject = Instantiate(itemTemplate, list);

            itemObject.transform.GetChild(0).GetComponent<Image>().sprite = item.GetSprite();
            itemObject.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = item.GetValue().ToString();
            itemObject.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = item.GetWeight().ToString();
        }
    }
}
