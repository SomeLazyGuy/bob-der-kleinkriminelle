using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DisplayItemInfo : MonoBehaviour
{
    private TextMeshProUGUI textMeshProComponent;
    
    void Start(){
        DontDestroyOnLoad(gameObject);
        
        Debug.Log($"[Start]: Initializing DisplayItemInfo for {gameObject.name}");

        // Create a Canvas
        GameObject canvasObject = new GameObject("ItemInfoCanvas");
        canvasObject.transform.SetParent(transform);
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();

        // Create a TextMeshProUGUI object
        GameObject textObject = new GameObject("ItemInfoText");
        textObject.transform.SetParent(canvasObject.transform);
        textMeshProComponent = textObject.AddComponent<TextMeshProUGUI>();

        // Set text properties
        textMeshProComponent.fontSize = 10f;
        textMeshProComponent.alignment = TextAlignmentOptions.Center;
        RectTransform rectTransform = textMeshProComponent.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(100, 50);

        // Do not call the UpdateText() on startup to avoid text always being visible
        //UpdateText();
    }

    public void UpdateText(ItemData itemData, Vector3 itemPosition) {
        if (textMeshProComponent == null){
            Debug.LogError($"[UpdateText]: textMeshProComponent is null for {gameObject.name}");
            return;
        }
        
        textMeshProComponent.gameObject.SetActive(true);
        
        // Log the world position of the object
        Vector3 objectWorldPosition = Camera.main.WorldToScreenPoint(transform.position);
        //Debug.Log($"[UpdateText] My object is {gameObject.name}, located at {objectWorldPosition}; real object is {itemData.itemName}");
        Debug.Log($"[UpdateText] Position {itemPosition} derived for {itemData.itemName}");
        const float smallItemHeightOffset = 1.6f;
        const float bigItemHeightOffset = 4.5f;

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

        Vector3 textPosition = itemPosition + new Vector3(0, autoHeightOffset, 0);
        textMeshProComponent.transform.position = Camera.main.WorldToScreenPoint(textPosition);
        Debug.Log($"[Update][textMeshProComponent]: Text position is {textMeshProComponent.transform.position}");

        textMeshProComponent.text = $"<sprite=10> Weight: {itemData.weight}kg\n<sprite=11> Value: {itemData.value}€";
    }

    public void HideText(){
        textMeshProComponent.gameObject.SetActive(false);
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
