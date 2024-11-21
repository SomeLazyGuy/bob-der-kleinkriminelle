using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour {
    [SerializeField] private Transform cells;
    [SerializeField] private Button closeButton;
    [SerializeField] private int indexOffset;
    [SerializeField] private bool insertMode;
    
    // nur wichtig wenn insert mode == false    
    [SerializeField] private int correctCellIndex; // startet nach dem offset wieder bei 0
    
    // nur wichtig wenn inser mode == true
    [SerializeField] private int insertCellIndex; // startet auch nach dem offset wieder bei 0
    [SerializeField] private string correctValue;
    
    private void Start() {
        closeButton.onClick.AddListener(delegate {
            gameObject.SetActive(false);
        });

        if (insertMode) {
            GameObject insertCell = cells.GetChild(insertCellIndex + indexOffset).gameObject;
            
            // button ausschalten und Textfeld einschalten
            insertCell.transform.GetChild(1).gameObject.SetActive(false);
            insertCell.transform.GetChild(0).gameObject.SetActive(true);

            
            // input überprüfen
            TMP_InputField inputField = insertCell.transform.GetChild(0).GetComponent<TMP_InputField>();
            Image image = insertCell.transform.GetChild(0).GetComponent<Image>();
            
            inputField.onValueChanged.AddListener(delegate {
                if (inputField.text == correctValue) {
                    image.color = Color.green;
                    closeButton.interactable = true;
                } else {
                    image.color = Color.red;
                    closeButton.interactable = false;
                }
            });
        } else {
            for (int i = indexOffset; i < cells.childCount; i++) {
                int j = i;
                cells.GetChild(i).GetChild(1).GetComponent<Button>().onClick.AddListener(delegate {
                    ButtonPressed(j);
                });
            }
        }
    }

    private void ButtonPressed(int index) {
        if (index - indexOffset == correctCellIndex) {
            closeButton.interactable = true;
            cells.GetChild(index).transform.GetChild(1).GetComponent<Image>().color = Color.green;
            return;
        }
        
        StartCoroutine(HighlightCell(index, Color.red));
    }

    private IEnumerator HighlightCell(int index, Color color) {
        GameObject cell = cells.GetChild(index).gameObject;
        Image image = cell.transform.GetChild(1).GetComponent<Image>();
        
        image.color = color;

        yield return new WaitForSeconds(0.5f);

        image.color = Color.white;
    }
}