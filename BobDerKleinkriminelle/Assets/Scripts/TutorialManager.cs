using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
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

        switch(currentScene.name){
            case "Level1":
                tutorialText.text = "Oft ist es nicht möglich, Objekte zu teilen. Das nennt man 0/1 Problem.";
                break;
            case "Level2":
                tutorialText.text = "Das hier ist ein Platzhaltertext für die Einführung in Level 2.";
                break;
            default:
                Debug.LogError($"Unknown scene: {currentScene.name}");
                break;
        }
    }
}
