using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using static Table;
using System.Linq;
using System.Data.Common;

public class TutorialManager : MonoBehaviour{
    //public GameObject tutorialCanvas;
    //public Button tutorialCloseButton;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private GameObject quizPrefab;
    [SerializeField] private GameObject tablePrefab;
    [SerializeField] private GameObject tablePrefab2;
    [SerializeField] private Transform contentContainer; 
    [SerializeField] private Button answerButton0;
    [SerializeField] private Button answerButton1;
    [SerializeField] private Button answerButton2;
    [SerializeField] private Button answerButton3;
    [SerializeField] private int correctAnswerIndex;



    public Button nextButton;
    public TextMeshProUGUI tutorialText;
    private int contentPage;
    private Boolean isFinished = false;
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

    public void ChallengeFinished(){
        isFinished = true;
        nextButton.interactable = true;
    }

    public void CloseTable(){
        tutorialCanvas.SetActive(false);
    }
    private void OnNextButtonClicked(){
        //tutorialCanvas.SetActive(false);
        Debug.Log($"Next Button Clicked. Incrementing content page from {contentPage} to {contentPage+1}.");
        contentPage++;
        if(isFinished){
            Debug.Log("Tutorial is finished. Closing tutorial canvas.");
            tutorialCanvas.SetActive(false);
        }else{
            SetTutorialContent();
        }
        
    }

    public void SetTutorialContent(){
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log($"[SetTutorialText] Current Scene is: {currentScene.name}");
        string tutorialMessage = "";
        

        switch(currentScene.name){
            case "Level1":
                if(contentPage == 0){
                    //StartCoroutine(AnimateText(Level_1_Message_Part1, 0.075f));
                    //StartCoroutine(AnimateText(Level_1_Message_Part1, 0.00000075f));    // TESTING
                    
                    // TESTING
                    InstantiateQuiz(quizPrefab);    // TESTING
                    nextButton.interactable = false;    // TESTING
                    

                }else if(contentPage == 1){
                    //StartCoroutine(AnimateText(Level_1_Message_Part2, 0.075f)); 
                    StartCoroutine(AnimateText(Level_1_Message_Part2, 0.00000075f));    // TESTING    
                }else if(contentPage == 2){
                    nextButton.interactable = false;
                    ClearText();
                    InstantiateTable(tablePrefab);
                }else{
                    Debug.LogError($"Unknown content page: {contentPage}");
                }
                break;
            case "Level2":
                ClearText();
                InstantiateTable(tablePrefab);
            break;
            case "Level3":
                ClearText();
                InstantiateTable(tablePrefab);
                break;
            case "Level4":
                ClearText();
                InstantiateTable(tablePrefab);
                break;
            case "Level5":
                switch(contentPage){
                    case 0:
                        InstantiateTable(tablePrefab);
                    break;
                    case 1:
                        InstantiateTable(tablePrefab2);
                    break;
                    default:
                        Debug.LogError($"Unknown content page: {contentPage}");
                    break;
                }    
            break;
            default:
                Debug.LogError($"Unknown scene: {currentScene.name}");
            break;

        }
    }

    private IEnumerator AnimateText(string message, float delay){
        nextButton.interactable = false;
        tutorialText.text = "";
        foreach(char letter in message.ToCharArray()){
            tutorialText.text += letter;
            if(letter == ' ' || letter == '\n'){
                continue;
            }
            yield return new WaitForSeconds(delay);
        }
        nextButton.interactable = true;
    }

    private void InstantiateTable(GameObject tablePrefab){
        GameObject tableInstance = Instantiate(tablePrefab, contentContainer);
        tableInstance.transform.localScale = Vector3.one;
        tableInstance.transform.localPosition = Vector3.zero;
        tableInstance.transform.localRotation = Quaternion.identity;
        tableInstance.GetComponent<Table>().tutorialManager = this;
        tableInstance.SetActive(true);
        
        RectTransform rectTransform = tableInstance.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.localScale = Vector3.one;
    }

    private void InstantiateQuiz(GameObject quizPrefab) {
        SetupQuizButtons();
        GameObject quizInstance = Instantiate(quizPrefab, contentContainer);
        RectTransform rectTransform = quizInstance.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.sizeDelta = new Vector2(800, 600); // Adjust size as needed
        rectTransform.localScale = Vector3.one;

        quizInstance.SetActive(true);
    }

    private void SetupQuizButtons(){
        answerButton0.onClick.AddListener(() => OnAnswerButtonClicked(answerButton0));
        answerButton1.onClick.AddListener(() => OnAnswerButtonClicked(answerButton1));
        answerButton2.onClick.AddListener(() => OnAnswerButtonClicked(answerButton2));
        answerButton3.onClick.AddListener(() => OnAnswerButtonClicked(answerButton3));
    }

    private void OnAnswerButtonClicked(Button clickedButton) {
        Debug.Log($"Answer button clicked: {clickedButton.name}");
        switch(clickedButton.name){
            case "answerButton0":
                Debug.Log("Answer 0 clicked");
                CheckAnswer(clickedButton, 0);
                break;
            case "answerButton1":
                Debug.Log("Answer 1 clicked");
                CheckAnswer(clickedButton, 1);
                break;
            case "answerButton2":
                Debug.Log("Answer 2 clicked");
                CheckAnswer(clickedButton, 2);
                break;
            case "answerButton3":
                Debug.Log("Answer 3 clicked");
                CheckAnswer(clickedButton, 3);
                break;
            default:
                Debug.LogError("Unknown answer button clicked");
                break;
        }
    }

    private void CheckAnswer(Button clickedButton, int index){
        if(correctAnswerIndex == 0){
            Debug.Log("Correct Answer");
            clickedButton.GetComponent<Image>().color = Color.green;
            nextButton.interactable = true;
        } else {
            Debug.Log("Wrong Answer");
            clickedButton.GetComponent<Image>().color = Color.red;
        }
    }

    private void ClearText(){
        foreach (Transform child in contentContainer) {
            Destroy(child.gameObject);
        }
    }

    private String Level_1_Message_Part1 = "Bei dem Knapsack- bzw. Rucksackproblem geht es darum, eine Auswahl " + 
    "von Objekten mit bestimmten Gewichten und Werten in einen Rucksack mit begrenzter Tragfähigkeit zu packen." + 
    " Ziel ist es, den Gesamtwert der ausgewählten Objekte zu maximieren, ohne das Gewichtslimit zu " + 
    "überschreiten.\n\nIm 0/1 Knapsack-Problem sind die Objekte nicht teilbar, was bedeutet, dass man jedes " + 
    "Objekt entweder vollständig in den Rucksack packt oder gar nicht. Du wirst zur Lösung den Greedy-Algorithmus " + 
    "und die dynamische Programmierung kennenlernen.";

    private String Level_1_Message_Part2 = "Beim Greedy-Algorithmus berechnest du das " + 
    "Wert-zu-Gewicht-Verhältnis, genau wie das Preis-Leistungs-Verhältnis bei Lebensmitteln. Es wird solange " + 
    "der Rucksack mit den günstigsten Objekten gefüllt, bis das nächste nicht mehr passt. Dieser Ansatz führt " + 
    "jedoch nicht immer zur optimalen Lösung, da er nicht alle möglichen Kombinationen berücksichtigt.\n\nDie " + 
    "dynamische Programmierung ist eine systematischere Methode, die alle möglichen Kombinationen von Objekten " + 
    "analysiert und die optimalen Lösungen für Teilprobleme speichert. Dieser Ansatz garantiert eine optimale " + 
    "Lösung, ist jedoch rechenintensiver.\n\nBobs Aufgabe ist es in diesem Level, einen möglichst hohen Wert in " + 
    "seinen Beutel zu stopfen. Bedenke, dass er nur begrenzt Beute tragen kann, da er noch fliehen muss!";

    private String Level_2_Message = "Okay, jetzt sollst du gem. dem Greedy-Algorithmus die Objekte mit der besten " +
    "Wertigkeit auswählen, bis der Rucksack voll ist. Fange mit dem wertvollsten Objekt an, gefolgt von dem" + 
    " zweitwertvollsten etc.\n\n" + "In der folgenden Tabelle siehst du die Objekte mit ihren Werten und Gewichten. " +
    "Die Wertigkeit kannst du einfach berechnen, indem du den Wert durch das Gewicht teilst.";
}
