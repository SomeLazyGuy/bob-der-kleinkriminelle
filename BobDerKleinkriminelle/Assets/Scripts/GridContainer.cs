using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridContainer : MonoBehaviour {
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private float spacingPercent;
    [SerializeField] private bool usePadding;

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
        float spacingSize = gridContainerSize.x * spacingPercent;
        Vector2 spacing = new Vector2(spacingSize * (columns - 1), spacingSize * (rows - 1));

        gridLayoutGroup.cellSize = (gridContainerSize - spacing) / new Vector2(columns, rows);
        gridLayoutGroup.spacing = new Vector2(spacingSize, spacingSize);
        if (usePadding) gridLayoutGroup.padding = new RectOffset((int)spacingSize, (int)spacingSize, (int)spacingSize, (int)spacingSize);
    }
}
