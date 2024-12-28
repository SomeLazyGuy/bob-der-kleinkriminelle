using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {
    public bool needsToBeClicked = true;
    [SerializeField] private bool needsToBeFilled = true;
    [SerializeField] private String correctValue;
    
    [HideInInspector] public Button button;
    [HideInInspector] public TMP_InputField inputField;

    private bool completed = false;
    
    private void Awake() {
        button = transform.GetChild(0).GetComponent<Button>();
        button.onClick.AddListener(delegate {
            if (completed) return;
            
            if (needsToBeClicked) {
                completed = true;
                button.GetComponent<Image>().color = Color.green;
                transform.parent.GetComponent<Table>().SolvedCell();
            } else {
                StartCoroutine(HighlightRed());
            }
        });
        
        inputField = transform.GetChild(1).GetComponent<TMP_InputField>();
        inputField.contentType = TMP_InputField.ContentType.Standard;
        inputField.characterLimit = 4;
        inputField.onValueChanged.AddListener(delegate {
            if (completed) return;
            
            if (inputField.text == correctValue) {
                completed = true;
                
                inputField.gameObject.SetActive(false);
                
                button.transform.GetComponentInChildren<TextMeshProUGUI>().text = correctValue;
                button.GetComponent<Image>().color = Color.green;
                button.gameObject.SetActive(true);

                transform.parent.GetComponent<Table>().SolvedCell();
            }
        });
        
        if (needsToBeFilled) {
			button.gameObject.SetActive(false);
            inputField.gameObject.SetActive(true);
		}
    }

    private IEnumerator HighlightRed() {
        Image image = button.GetComponent<Image>();
        
        image.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        image.color = Color.white;
    }
}
