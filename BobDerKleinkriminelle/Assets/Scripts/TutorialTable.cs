using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTable : MonoBehaviour {
    [SerializeField] private int highlightSeconds;
    
    private TutorialManager _tutorialManager;

    private void Start() {
        _tutorialManager = transform.parent.parent.parent.GetComponent<TutorialManager>();
        _tutorialManager.PerformOnElapsedTime(HighlightCapacities, 1);
        _tutorialManager.PerformOnElapsedTime(HighlightItems, 4);
    }

    private int HighlightCapacities() {
        Image[] cells = new Image[9];
        
        for (int i = 1; i < 10; i++) {
            cells[i - 1] = transform.GetChild(i).GetComponent<Image>();
        }

        StartCoroutine(HighlightCells(cells, highlightSeconds));
        
        return 0;
    }

    private int HighlightItems() {
        Image[] cells = new Image[3];

        cells[0] = transform.GetChild(10).GetComponent<Image>();
        cells[1] = transform.GetChild(20).GetComponent<Image>();
        cells[2] = transform.GetChild(30).GetComponent<Image>();

        StartCoroutine(HighlightCells(cells, highlightSeconds));
        
        return 0;
    }

    private IEnumerator HighlightCells(Image[] cells, int time) {
        foreach (var cell in cells) {
            cell.color = new Color32(255, 209, 57, 255);
        }

        yield return new WaitForSeconds(time);
        
        foreach (var cell in cells) {
            cell.color = Color.white;
        }
    }
}
