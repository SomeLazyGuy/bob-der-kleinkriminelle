using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using static Table;
using System.Linq;

public class TutorialManager : MonoBehaviour{
    //public GameObject tutorialCanvas;
    //public Button tutorialCloseButton;
    [SerializeField] private GameObject tablePrefab;
    [SerializeField] private Transform contentContainer; 
    public Button nextButton;
    public TextMeshProUGUI tutorialText;
    private int contentPage;
    void Start()
    {
        if(nextButton != null){
            nextButton.onClick.AddListener(OnNextButtonClicked);
        }else{
            Debug.LogError("NextButton is null");
        }
        tutorialText.fontSize = 9.5f;
        SetTutorialContent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnNextButtonClicked(){
        //tutorialCanvas.SetActive(false);
        Debug.Log($"Next Button Clicked. Incrementing content page from {contentPage} to {contentPage+1}.");
        contentPage++;
        SetTutorialContent();
    }

    public void SetTutorialContent(){
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log($"[SetTutorialText] Current Scene is: {currentScene.name}");
        string tutorialMessage = "";

        switch(currentScene.name){
            case "Level1":
                if(contentPage == 0){
                    String lvl1TODO = "Hier kommt noch der Text für die Einführung in Level 1.";
                    StartCoroutine(AnimateText(lvl1TODO, 0.075f));
                }else if(contentPage == 1){
                    GameObject tableInstance = Instantiate(tablePrefab, contentContainer);
                    StartCoroutine(AnimateText("", 0f));
                    tableInstance.SetActive(true);
                    
                    RectTransform rectTransform = tableInstance.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = Vector2.zero;
                    rectTransform.offsetMin = Vector2.zero;
                    rectTransform.offsetMax = Vector2.zero;
                    rectTransform.localScale = Vector3.one;
                }else{
                    Debug.LogError($"Unknown content page: {contentPage}");
                }
                break;
            case "Level2":
                tutorialMessage = "Das hier ist ein Platzhaltertext für die Einführung in Level 2.";
                break;
            default:
                Debug.LogError($"Unknown scene: {currentScene.name}");
                break;
        }
    }

    private IEnumerator AnimateText(string message, float delay){
        tutorialText.text = "";
        foreach(char letter in message.ToCharArray())
        {
            tutorialText.text += letter;
            if(letter == ' ' || letter == '\n'){
                continue;
            }
            yield return new WaitForSeconds(delay);
        }
    }

    private String Level_1_Message = "BBei dem Knapsack- bzw. Rucksackproblem geht es darum, eine Auswahl " + 
    "von Objekten mit bestimmten Gewichten und Werten in einen Rucksack mit begrenzter Tragfähigkeit zu packen." + 
    " Ziel ist es, den Gesamtwert der ausgewählten Objekte zu maximieren, ohne das Gewichtslimit zu " + 
    "überschreiten.\n\nIm 0/1 Knapsack-Problem sind die Objekte nicht teilbar, was bedeutet, dass man jedes " + 
    "Objekt entweder vollständig in den Rucksack packt oder gar nicht. Du wirst zur Lösung den Greedy-Algorithmus " + 
    "und die dynamische Programmierung kennenlernen.\n\n Beim Greedy-Algorithmus berechnest du das " + 
    "Wert-zu-Gewicht-Verhältnis, genau wie das Preis-Leistungs-Verhältnis bei Lebensmitteln. Es wird solange " + 
    "der Rucksack mit den günstigsten Objekten gefüllt, bis das nächste nicht mehr passt. Dieser Ansatz führt " + 
    "jedoch nicht immer zur optimalen Lösung, da er nicht alle möglichen Kombinationen berücksichtigt.\n\n Die " + 
    "dynamische Programmierung ist eine systematischere Methode, die alle möglichen Kombinationen von Objekten " + 
    "analysiert und die optimalen Lösungen für Teilprobleme speichert. Dieser Ansatz garantiert eine optimale " + 
    "Lösung, ist jedoch rechenintensiver.\n\nBobs Aufgabe ist es in diesem Level, einen möglichst hohen Wert in " + 
    "seinen Beutel zu stopfen. Bedenke, dass er nur begrenzt Beute tragen kann, da er noch fliehen muss!";
}
