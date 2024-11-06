using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListUI : MonoBehaviour
{
    public GameObject itemTemplate;     // The ItemTemplate prefab
    public Transform gridPanel;         // The GridLayout panel

    private GetItems getItemsScript;    // Reference to GetItems script

    void Start()
    {
        // Zuweisung des GetItems-Skripts über GetComponent, da beide Skripte auf dem selben GameObject liegen
        getItemsScript = GetComponent<GetItems>();

        if (getItemsScript != null)
        {
            UpdateDisplay();
        }
        else
        {
            Debug.LogError("GetItems script not found on this GameObject.");
        }
    }

    public void UpdateDisplay()
    {
        // Clear existing items in the grid panel
        foreach (Transform child in gridPanel)
        {
            Destroy(child.gameObject);
        }

        // Get the item data from GetItems
        List<ItemDataInfo> itemsData = getItemsScript.GetItemsData();  // Changed to ItemDataInfo

        // Create a UI element for each item in the data list
        foreach (ItemDataInfo data in itemsData)  // Changed to ItemDataInfo
        {
            GameObject itemUI = Instantiate(itemTemplate, gridPanel);

            // Set the Icon, Weight, and Value in the UI
            Image iconImage = itemUI.transform.Find("Icon").GetComponent<Image>();
            Text weightText = itemUI.transform.Find("Weight").GetComponent<Text>();
            Text valueText = itemUI.transform.Find("Value").GetComponent<Text>();

            iconImage.sprite = data.Sprite;
            weightText.text = "Weight: " + data.Weight.ToString();
            valueText.text = "Value: " + data.Value.ToString();
        }
    }
}


