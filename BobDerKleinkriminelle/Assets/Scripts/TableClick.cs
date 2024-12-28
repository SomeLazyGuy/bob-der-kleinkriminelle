using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableClick : Table {
    [SerializeField] private Cell[] uncompletedCells;

    private int _currentIndex;

    private void Start() {
        foreach (var cell in uncompletedCells) {
            cell.needsToBeClicked = false;
        }

        uncompletedCells[0].needsToBeClicked = true;
    }
    
    public override void SolvedCell() {
        base.SolvedCell();

        _currentIndex++;
        if (_currentIndex < uncompletedCells.Length) {
            uncompletedCells[_currentIndex].needsToBeClicked = true;
        }
    }
}
