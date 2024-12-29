using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.ComponentModel;
using static Table;
using System.Linq;
using System.Data.Common;
using Unity.VisualScripting;

public class TutorialManager : MonoBehaviour{
    //public GameObject tutorialCanvas;
    //public Button tutorialCloseButton;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private GameObject quizPrefab;
    [SerializeField] private String quizQuestion;
    [SerializeField] private String quizAnswer0;
    [SerializeField] private String quizAnswer1;
    [SerializeField] private String quizAnswer2;
    [SerializeField] private String quizAnswer3;
    [SerializeField] private GameObject tablePrefab;
    [SerializeField] private GameObject tablePrefab2;
    [SerializeField] private Transform contentContainer; 
    [SerializeField] private Button answerButton0;
    [SerializeField] private Button answerButton1;
    [SerializeField] private Button answerButton2;
    [SerializeField] private Button answerButton3;
    [SerializeField] private int correctAnswerIndex = 2;    // TODO: TESTING
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip1;
    [SerializeField] private AudioClip audioClip2;
    [SerializeField] private AudioClip audioClip3;
    [SerializeField] private AudioClip audioClip4;
    [SerializeField] private AudioClip audioClip5;
    [SerializeField] private AudioClip audioClip6; 

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
        
        SetTutorialContent();
    }

    private void Update() {
        if(player != null){ player.DisableMovement(); } // nicht ideal, aber passt schon
    }

    public void ChallengeFinished(){
        isFinished = true;
        nextButton.interactable = true;
    } 
    
    private void OnNextButtonClicked(){
        Debug.Log($"Next Button Clicked. Incrementing content page from {contentPage} to {contentPage+1}.");
        contentPage++;
        SetTutorialContent();        
    }

    private void CloseTutorialCanvas() {
        tutorialCanvas.SetActive(false);
        player.EnableMovement();
    }

    private void SetTutorialContent(){
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log($"[SetTutorialText] Current Scene is: {currentScene.name}");       
        switch(currentScene.name){
            case "Level1":
                switch(contentPage){
                    case 0:
                        PlayAudioClip(audioClip1);
                        StartCoroutine(AnimateText(Level_1_Message_Part1, 0.025f));
                        break;
                    case 1:
                        StopAudioSource();
                        ClearText();
                        nextButton.interactable = false;
                        InstantiateTable(tablePrefab);
                        PlayAudioClip(audioClip6);
                        break;
                    case 2:
                        // "Okay, bereit? Wähle zuerst die Gegenstände mit dem höchsten Wert pro Gewicht aus";
                        ClearGameObject(tablePrefab);
                        PlayAudioClip(audioClip2); 
                        StartCoroutine(AnimateText(Level_1_Message_Part3, 0.05f));
                        break;
                    case 3:
                        CloseTutorialCanvas();
                        break;
                    default:
                        Debug.LogError($"Unknown content page: {contentPage}");
                        break;
                }
                break;
            case "Level2":
                switch(contentPage){
                    case 0:
                        // "Jetzt fehlen nur noch die Wertigkeiten von unseren Keksen und von unserem Schmuck."
                        ClearText();
                        nextButton.interactable = false;
                        InstantiateTable(tablePrefab);
                        PlayAudioClip(audioClip2);
                        break;
                    case 1:
                        // "Was packst du zuerst ein?"
                        nextButton.interactable = false;
                        ClearGameObject(tablePrefab);
                        PlayAudioClip(audioClip3);
                        InstantiateQuiz(quizPrefab);
                        break;
                    case 2:
                        nextButton.interactable = false;
                        ClearGameObject(quizPrefab);    
                        quizQuestion = "Welche Gegenstand nehmen wir als nächstes?";
                        quizAnswer0 = "Geld";
                        quizAnswer1 = "Schmuck";
                        quizAnswer2 = "Goldmünzen"; // <- Correct Answer
                        quizAnswer3 = "Edelsteine";
                        correctAnswerIndex = 2;
                        InstantiateQuiz(quizPrefab);
                        PlayAudioClip(audioClip4);
                        break;
                    case 3:
                        nextButton.interactable = false;
                        ClearGameObject(quizPrefab);
                        quizQuestion = "Wie viele Anteile kannst du vom letzten Gegenstand einpacken?";
                        quizAnswer0 = "1/7";
                        quizAnswer1 = "2/7";
                        quizAnswer2 = "3/7";
                        quizAnswer3 = "4/7"; // <- Correct Answer
                        correctAnswerIndex = 3;
                        InstantiateQuiz(quizPrefab);
                        PlayAudioClip(audioClip5);  
                        break;
                    case 4:
                        CloseTutorialCanvas();
                        break;
                    default:
                        Debug.LogError($"Unknown content page: {contentPage}");
                        break;  
                }                
            break;
            case "Level3":
                switch(contentPage){
                    case 0:
                        ClearText();
                        nextButton.interactable = false;
                        InstantiateTable(tablePrefab);
                        PlayAudioClip(audioClip1);
                        break;
                    case 1:
                        nextButton.interactable = false;
                        ClearGameObject(tablePrefab);
                        quizQuestion = "Welche Gegenstand packst du zuerst ein?";
                        quizAnswer0 = "Geld";
                        quizAnswer1 = "Schmuck";
                        quizAnswer2 = "Kekse"; // <- Correct Answer
                        quizAnswer3 = "Edelsteine";
                        correctAnswerIndex = 2;
                        InstantiateQuiz(quizPrefab);
                        break;
                    case 2:
                        nextButton.interactable = false;
                        ClearGameObject(quizPrefab);    
                        quizQuestion = "Welche Gegenstand nehmen wir als nächstes?";
                        quizAnswer0 = "Goldmünzen"; // <- Correct Answer
                        quizAnswer1 = "Schmuck";
                        quizAnswer2 = "Geld"; 
                        quizAnswer3 = "Edelsteine";
                        correctAnswerIndex = 0;
                        InstantiateQuiz(quizPrefab);
                        PlayAudioClip(audioClip4);
                        break;
                    case 3:
                        nextButton.interactable = false;
                        ClearGameObject(quizPrefab);
                        quizQuestion = "Welchen Gegenstand packst du danach ein?";
                        quizAnswer0 = "Geld";
                        quizAnswer1 = "Goldmünzen";
                        quizAnswer2 = "Keinen";
                        quizAnswer3 = "Silbernuggets"; // <- Correct Answer
                        correctAnswerIndex = 3;
                        InstantiateQuiz(quizPrefab);
                        PlayAudioClip(audioClip5);  
                        break;                      
                    case 4:
                        nextButton.interactable = false;
                        ClearGameObject(quizPrefab);
                        quizQuestion = "Wie viel kannst du vom Schmuck mitnehmen?";
                        quizAnswer0 = "1/2";
                        quizAnswer1 = "1/3";
                        quizAnswer2 = "1/4";
                        quizAnswer3 = "1/5";
                        correctAnswerIndex = 0;
                        InstantiateQuiz(quizPrefab);
                        break;
                    case 5:
                        CloseTutorialCanvas();
                        PlayAudioClip(audioClip2);
                        break;
                }
                break;
            case "Level4":
                switch(contentPage){
                    case 0:
                        PlayAudioClip(audioClip1);
                        StartCoroutine(AnimateText(Level_4_Message, 0.01f));
                        break;
                    case 1:
                        ClearText();
                        InstantiateTable(tablePrefab);
                        PlayAudioClip(audioClip2);
                        break;
                    default:
                        Debug.LogError($"Unknown content page: {contentPage}");
                        break;
                }
                break;
            case "Level5":
                switch(contentPage){
                    case 0:
                        nextButton.interactable = false;
                        InstantiateTable(tablePrefab);
                        PlayAudioClip(audioClip1);
                    break;
                    case 1:
                        nextButton.interactable = false;
                        ClearGameObject(tablePrefab);
                        InstantiateTable(tablePrefab2);
                    break;
                    case 2:
                        CloseTutorialCanvas();
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
            yield return new WaitForSecondsRealtime(delay);
        }
        nextButton.interactable = true;
    }

    private void InstantiateTable(GameObject tablePrefab){
        GameObject tableInstance = Instantiate(tablePrefab, contentContainer);
        tableInstance.GetComponent<Table>().tutorialManager = this;
        tableInstance.SetActive(true);
    }

    private void InstantiateQuiz(GameObject quizPrefab) {
        GameObject quizInstance = Instantiate(quizPrefab, contentContainer);
        TextMeshProUGUI questionText = quizInstance.transform.Find("QuestionText").GetComponent<TextMeshProUGUI>();
        
        questionText.text = quizQuestion;
        SetupQuizButtons(quizInstance);
        quizInstance.SetActive(true);
    }

    private void SetupQuizButtons(GameObject quizInstance) {
        Transform buttonContainer = quizInstance.transform.Find("ButtonContainer");
        String Weight = $"<sprite name=\"WeightHD\">";
        String Dollar = $"<sprite name=\"DollarHD\">";
        Button[] answerButtons = new Button[4];
        String[] quizAnswers = new string[4];
        
        
        for (int i = 0; i <= 3; i++) {
            int index = i;
            answerButtons[i] = buttonContainer.Find("AnswerButton" + i).GetComponent<Button>();
            answerButtons[i].onClick.AddListener(() => OnAnswerButtonClicked(answerButtons[index]));
            switch (i) {
                case 0:
                    answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = quizAnswer0;
                    break;
                case 1:
                    answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = quizAnswer1;
                    break;
                case 2:
                    answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = quizAnswer2;
                    break;
                case 3:
                    answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = quizAnswer3;
                    break;
            }
        }   
    }

    private void OnAnswerButtonClicked(Button clickedButton) {
        Debug.Log($"Answer button clicked: {clickedButton.name}");
        switch(clickedButton.name){
            case "AnswerButton0":
                Debug.Log("Answer 0 clicked");
                CheckAnswer(clickedButton, 0);
                break;
            case "AnswerButton1":
                Debug.Log("Answer 1 clicked");
                CheckAnswer(clickedButton, 1);
                break;
            case "AnswerButton2":
                Debug.Log("Answer 2 clicked");
                CheckAnswer(clickedButton, 2);
                break;
            case "AnswerButton3":
                Debug.Log("Answer 3 clicked");
                CheckAnswer(clickedButton, 3);
                break;
            default:
                Debug.LogError("Unknown answer button clicked: " + clickedButton.name);
                break;
        }
    }

    private void CheckAnswer(Button clickedButton, int index){
        if(correctAnswerIndex == index){	
            Debug.Log("Correct Answer");
            clickedButton.GetComponent<Image>().color = Color.green;
            nextButton.interactable = true;
        } else {
            Debug.Log("Wrong Answer");
            clickedButton.GetComponent<Image>().color = Color.red;
        }
    }

    private void ClearText(){
        /*foreach (Transform child in contentContainer) {
            Destroy(child.gameObject);
        }*/
        tutorialText.text = "";
        StartCoroutine(AnimateText(tutorialText.text, 0f));
    }

    private void ClearGameObject(GameObject go){
        GameObject instance = GameObject.Find(go.name + "(Clone)");
        if (instance != null) {
            instance.SetActive(false);
        } else {
            Debug.LogError("Table instance not found");
        }
    }

    private void PlayAudioClip(AudioClip audioClip) {
        if (audioSource != null && audioClip != null) {
            audioSource.clip = audioClip;
            audioSource.Play();
        } else {
            Debug.LogError("AudioSource or AudioClip is not assigned.");
        }
    }

    public void PerformOnElapsedTime<T>(Func<T> func, int seconds) {
        if (audioSource != null) {
            StartCoroutine(PerformActionAfterElapsedTime(func, seconds));
        }
    }

    private IEnumerator PerformActionAfterElapsedTime<T>(Func<T> func, int seconds) {
        while (audioSource.isPlaying) {
            if (audioSource.time >= seconds) {
                Debug.Log($"Action called at {audioSource.time} ms, target: {seconds} ms");
                func();
                yield break;
            }
            yield return null;
        }
    }

    private void StopAudioSource() {
        if (audioSource != null) {
            audioSource.Stop();
        } else {
            Debug.LogError("AudioSource is not assigned.");
        }
    }

    /*
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
    */

    private String Level_1_Message_Part1 = "Okay, lass uns loslegen! Du möchtest möglichst wertvolle Beute klauen, "+
    "aber in deinen Rucksack passen nur 16 kg. Wie wählst du die Gegenstände aus, mit denen du den höchsten Wert "+
    "erzielen kannst? Du verwendest einen Greedy Ansatz, um teilbare Mengen auszuwählen.";

    private String Level_1_Message_Part2 = "Berechne die Wertigkeit pro Gewicht für jeden Gegenstand und sortiere "+
    "die Gegenstände in absteigender Reihenfolge. Nehme zuerst die Gegenstände mit der höchsten Wertigkeit auf und "+
    "nehme so lange weitere Gegenstände auf, bis deine Kapazität erreicht ist.\n\nIch zeig es dir an einem Beispiel. "+
    "Gib in der folgenden Tabelle die jeweilige Werigkeit für die Gegenstände an. Verwende für Brüche die Schreibweise "+
    "1/2 statt 0.5.";

    private String Level_1_Message_Part3 = "Okay, bereit? Wähle zuerst die Gegenstände mit dem höchsten Wert pro Gewicht aus";
    
    /*private String Level_2_Message = "Okay, jetzt sollst du gem. dem Greedy-Algorithmus die Objekte mit der besten " +
    "Wertigkeit auswählen, bis der Rucksack voll ist. Fange mit dem wertvollsten Objekt an, gefolgt von dem" + 
    " zweitwertvollsten etc.\n\n" + "In der folgenden Tabelle siehst du die Objekte mit ihren Werten und Gewichten. " +
    "Die Wertigkeit kannst du einfach berechnen, indem du den Wert durch das Gewicht teilst.";*/

    private String Level_4_Message = "Du weißt, was du machen sollst, wenn du Gegenstände nicht voll mitnehmen musst. Oft "+
    "ist es aber nicht möglich, Objekte zu teilen. Das nennt man 0/1-Problem, also entweder nehmen oder nicht nehmen.  " +
    "Daran muss man etwas anders rangehen, um die global beste Lösung zu erzielen. Versuche es aber erstmal selber.";
    private String Level_2_Message_Part2 = "Okay, jetzt haben die Goldmünzen eine  Wertigkeit von 2 ,die Edelsteine von 2/3 " +
    "und das Geld hat eine Wertigkeit von 1/6. Jetzt fehlen nur noch die Wertigkeiten von unseren Keksen und von unserem Schmuck."+
    "\n\nTrage sie in der folgenden Tabelle ein.\n\nTipp für Kleinkriminelle: Notiere dir auf einem Zettel die Wertigkeiten der "+
    "Gegenstände, beor du die Tabelle schließt.";
}