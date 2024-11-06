using System.Collections.Generic;
using UnityEngine;

public class GetItems : MonoBehaviour
{
    public GameObject Items;
    private List<ItemDataInfo> itemsData = new List<ItemDataInfo>();

    void Start()
    {
        FetchItems();
    }

    public void FetchItems()
    {
        itemsData.Clear(); // Clear the list before fetching new items

        if (Items != null)
        {
            for (int i = 0; i < Items.transform.childCount; i++)
            {
                Transform child = Items.transform.GetChild(i);
                ItemEntity itemEntity = child.GetComponent<ItemEntity>();

                if (itemEntity != null)
                {
                    // Create a new ItemData and add it to the list
                    ItemDataInfo data = new ItemDataInfo
                    {
                        Sprite = itemEntity.GetSprite(),
                        Weight = itemEntity.GetWeight(),
                        Value = itemEntity.GetValue()
                    };
                    itemsData.Add(data);

                    Debug.Log("Item added: " + data.Sprite.name + ", Weight: " + data.Weight + ", Value: " + data.Value);  // Debugging line
                }
                else
                {
                    Debug.LogWarning($"ItemEntity component not found on child: {child.name}");
                }
            }
        }
        else
        {
            Debug.LogWarning("Items GameObject not found.");
        }
    }

    public List<ItemDataInfo> GetItemsData()
    {
        return new List<ItemDataInfo>(itemsData); // Return a copy of the list
    }
}

// Class to store item data
[System.Serializable]
public class ItemDataInfo
{
    public Sprite Sprite;
    public float Weight;
    public float Value;
}



