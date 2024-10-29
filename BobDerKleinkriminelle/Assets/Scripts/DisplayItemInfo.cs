using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayItemInfo : MonoBehaviour
{
    public ItemData itemData; // siehe ItemData.cs
    private GameObject textMesh;

    private GameObject textObject;
    private TextMeshProUGUI textMeshProComponent;

    [SerializeField] private int heightOffset;

    void Start()
    {
        if (textMesh == null)
        {
            Debug.LogError("[Start]: textPrefab is not assigned in the Inspector");
            return;
        }

        GameObject canvas = new();
        canvas.transform.SetParent(transform);
        canvas.AddComponent<Canvas>();


        GameObject textObject = new GameObject();
        textObject.transform.SetParent(canvas.transform);
        textMeshProComponent = textObject.AddComponent<TextMeshProUGUI>();


        if (textMeshProComponent == null)
        {
            Debug.LogWarning("[Start]: textPrefab does not have a TextMeshProUGUI component, adding one.");
            textMeshProComponent = textObject.AddComponent<TextMeshProUGUI>();
        }

        if (itemData == null)
        {
            Debug.LogError("[Start]: itemData is not assigned in the Inspector");
            return;
        }

        UpdateText();
    }

    public void UpdateText()
    {
        if (itemData == null)
        {
            Debug.LogError("[UpdateText]: itemData is null");
            textMeshProComponent.text = "Error :(";
            return;
        }

        if (textMeshProComponent == null)
        {
            Debug.LogError("[UpdateText]: textMeshProComponent is null");
            return;
        }

        Debug.Log("[UpdateText]: Successfully fetched itemData (" + itemData.weight + "kg, " + itemData.value + "€)");
        textMeshProComponent.text = $"Weight: {itemData.weight}\nValue: {itemData.value}\nTest";
    }

    void Update()
    {
        if (textObject != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            textObject.transform.position = screenPosition + new Vector3(0, heightOffset, -9);
        }
    }
}