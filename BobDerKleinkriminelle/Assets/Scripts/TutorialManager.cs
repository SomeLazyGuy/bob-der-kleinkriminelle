using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TutorialManager : MonoBehaviour{
    public GameObject tutorialCanvas;
    public Button tutorialCloseButton;
    public TextMeshProUGUI tutorialText;
    void Start()
    {
        if(tutorialCloseButton != null){
            tutorialCloseButton.onClick.AddListener(OnCloseButtonClicked);
        }else{
            Debug.LogError("TutorialCloseButton is null");
        }
        SetTutorialText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCloseButtonClicked(){
        tutorialCanvas.SetActive(false);
    }

    public void SetTutorialText(){
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log($"[SetTutorialText] Current Scene is: {currentScene.name}");
        string tutorialMessage = "";

        switch(currentScene.name){
            case "Level1":
                tutorialMessage = Level_1_Message;
                break;
            case "Level2":
                tutorialMessage = "Das hier ist ein Platzhaltertext für die Einführung in Level 2.";
                break;
            default:
                Debug.LogError($"Unknown scene: {currentScene.name}");
                break;
        }

        //StartCoroutine(AnimateText(tutorialMessage, 0.075f));
        StartCoroutine(AnimateText(tutorialMessage, 0.0001f));
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

    private String Level_1_Message = "Bei dem Knapsack- bzw. Rucksackproblem geht es darum, eine Auswahl " + 
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
