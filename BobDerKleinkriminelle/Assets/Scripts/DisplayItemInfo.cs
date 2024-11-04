using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DisplayItemInfo : MonoBehaviour
{
    public ItemData itemData;
    [SerializeField] private float heightOffset = 1.0f;
    private TextMeshProUGUI textMeshProComponent;

    void Start(){
        Debug.Log($"[Start]: Initializing DisplayItemInfo for {gameObject.name}");

        if (itemData == null)
        {
            Debug.LogError($"[Start]: itemData is not assigned for {gameObject.name}");
            return;
        }

        // Create a Canvas
        GameObject canvasObject = new GameObject("ItemInfoCanvas");
        canvasObject.transform.SetParent(transform);
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();

        // Create a TextMeshProUGUI object
        GameObject textObject = new GameObject("ItemInfoText");
        textObject.transform.SetParent(canvasObject.transform);
        textMeshProComponent = textObject.AddComponent<TextMeshProUGUI>();

        // Set text properties
        textMeshProComponent.fontSize = 0.5f;
        textMeshProComponent.alignment = TextAlignmentOptions.Center;
        RectTransform rectTransform = textMeshProComponent.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(100, 50);

        // Do not call the UpdateText() on startup to avoid text always being visible
        //UpdateText();
    }

    public void UpdateText(){
        if (itemData == null){
            Debug.LogError($"[UpdateText]: itemData is null for {gameObject.name}");
            textMeshProComponent.text = "Error :(";
            return;
        }

        if (textMeshProComponent == null){
            Debug.LogError($"[UpdateText]: textMeshProComponent is null for {gameObject.name}");
            //textMeshProComponent.text = "";   // DEBUGGING
            return; // DEBUGGING
        }

        // Log the world position of the object
        Vector3 objectWorldPosition = transform.position;
        //Debug.Log($"[Update][textMeshProComponent]: My object is {transform.name}, located at {objectWorldPosition}");       // DEBUGGING
        Debug.Log($"[Update][textMeshProComponent]: My object is {gameObject.name}, located at {objectWorldPosition}; real object is {itemData.itemName}");       // DEBUGGING 

        // Apply the height offset and set the position of the text component
        //float autoHeightOffset = 2.5f;
        const float smallItemHeightOffset = 1.6f;
        const float bigItemHeightOffset = 3.0f;

        Dictionary<string,float> itemHeightOffsets = new Dictionary<string, float>(){
            {"Moneybag", smallItemHeightOffset},
            {"Cup", smallItemHeightOffset},
            {"Cashregister", smallItemHeightOffset},
            {"Vault", bigItemHeightOffset},
            {"Painting", bigItemHeightOffset}
        };

        float autoHeightOffset = itemHeightOffsets.TryGetValue(
            gameObject.name, out float heightOffset) ? heightOffset : 2.5f;
        Debug.Log($"[Update][textMeshProComponent]: Height offset of {autoHeightOffset} for {gameObject.name}");

        Vector3 textPosition = objectWorldPosition + new Vector3(0, autoHeightOffset, 0);  // TODO
        textMeshProComponent.transform.position = textPosition;
        Debug.Log($"[Update][textMeshProComponent]: Text position is {textMeshProComponent.transform.position}");


        Debug.Log($"[UpdateText]: Successfully fetched itemData for {gameObject.name} ({itemData.weight}kg, {itemData.value}€)");
        textMeshProComponent.text = $"Weight: {itemData.weight}\nValue: {itemData.value}";
    }

    public void hideText(){
        if (itemData == null){
            Debug.LogError($"[hideText]: itemData is null for {gameObject.name}");
            textMeshProComponent.text = "Error :(";
            return;
        }

        if (textMeshProComponent == null)
        {
            Debug.LogError($"[UpdateText]: textMeshProComponent is null for {gameObject.name}");
            return;
        }

        Debug.Log($"[hideText]: Hiding text for {gameObject.name}");
        textMeshProComponent.text = "";
    }

    /*
    void Update(){
        if (textMeshProComponent != null)
        {
            // Log the world position of the object
            Vector3 objectWorldPosition = transform.position;
            Debug.Log($"[Update][textMeshProComponent]: My object is {transform.name}, located at {objectWorldPosition}");

            // Apply the height offset and set the position of the text component
            //float autoHeightOffset = 2.5f;
            const float smallItemHeightOffset = 1.6f;
            const float bigItemHeightOffset = 3.0f;

            Dictionary<string,float> itemHeightOffsets = new Dictionary<string, float>(){
                {"Moneybag", smallItemHeightOffset},
                {"Cup", smallItemHeightOffset},
                {"Cashregister", smallItemHeightOffset},
                {"Vault", bigItemHeightOffset},
                {"Painting", bigItemHeightOffset}
            };

            float autoHeightOffset = itemHeightOffsets.TryGetValue(
                gameObject.name, out float heightOffset) ? heightOffset : 2.5f;
            Debug.Log($"[Update][textMeshProComponent]: Height offset of {autoHeightOffset} for {gameObject.name}");

            Vector3 textPosition = objectWorldPosition + new Vector3(0, autoHeightOffset, 0);  // TODO
            textMeshProComponent.transform.position = textPosition;
            Debug.Log($"[Update][textMeshProComponent]: Text position is {textMeshProComponent.transform.position}");
        }
        else
        {
            Debug.LogError($"[Update]: textMeshProComponent is null for {gameObject.name}");
        }
    }
    */
}
