using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {
    [SerializeField] private bool needsToBeClicked = true;
    [SerializeField] private bool needsToBeFilled = true;
    [SerializeField] private String correctValue;
    
    [HideInInspector] public Button button;
    [HideInInspector] public TMP_InputField inputField;

    private void Awake() {
        button = transform.GetChild(0).GetComponent<Button>();
        inputField = transform.GetChild(1).GetComponent<TMP_InputField>();
    }
}
