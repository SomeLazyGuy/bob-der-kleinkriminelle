using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridContainer : MonoBehaviour {
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    private GridLayoutGroup gridLayoutGroup;

    private void Start() {
        gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
        ScaleCells();
    }

    private void OnRectTransformDimensionsChange() {
        ScaleCells();
    }

    // scale cell size according to grid container scale
    private void ScaleCells() {
        if (gridLayoutGroup == null) return;
        
        Vector2 gridContainerSize = gameObject.GetComponent<RectTransform>().rect.size;
        Vector2 spacingSize = new Vector2(gridLayoutGroup.spacing.x * (columns - 1), gridLayoutGroup.spacing.y * (rows - 1));
        Vector2 paddingSize = new Vector2(gridLayoutGroup.padding.left + gridLayoutGroup.padding.right, gridLayoutGroup.padding.top + gridLayoutGroup.padding.bottom);
        
        gridLayoutGroup.cellSize = (gridContainerSize - spacingSize - paddingSize) / new Vector2(columns, rows);
    }
}
