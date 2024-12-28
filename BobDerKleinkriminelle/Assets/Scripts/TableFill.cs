using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TableFill : Table {
    [SerializeField] private Cell[] uncompletedCells;

    private int _currentIndex;
    private TMP_InputField[] _inputFields;

    private void Start() {
        _inputFields = new TMP_InputField[uncompletedCells.Length];
        
        for (int i = 0; i < uncompletedCells.Length; i++) {
            _inputFields[i] = uncompletedCells[i].transform.GetChild(1).GetComponent<TMP_InputField>();
            _inputFields[i].interactable = false;
        }

        _inputFields[0].interactable = true;
    }
    
    public override void SolvedCell() {
        base.SolvedCell();
        
        _currentIndex++;
        if (_currentIndex < uncompletedCells.Length) {
            uncompletedCells[_currentIndex].transform.GetChild(1).GetComponent<TMP_InputField>().interactable = true;
        }
    }
}
