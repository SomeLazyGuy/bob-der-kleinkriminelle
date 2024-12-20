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
        // nicht ideal, aber passt schon
        player.DisableMovement();
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
                        StartCoroutine(AnimateText(Level_1_Message_Part1, 0.00000075f));    // TODO: Adjust speed after testing phase
                        break;
                    case 1:
                        StopAudioSource();
                        StartCoroutine(AnimateText(Level_1_Message_Part2, 0.00000075f));    // TODO: Adjust speed after testing phase
                        break;
                    case 2:
                        ClearText();
                        nextButton.interactable = false;
                        InstantiateTable(tablePrefab);
                        break;
                    case 3:
                        // "Okay, bereit? Wähle zuerst die Gegenstände mit dem höchsten Wert pro Gewicht aus";
                        ClearGameObject(tablePrefab);
                        PlayAudioClip(audioClip2); 
                        StartCoroutine(AnimateText(Level_1_Message_Part3, 0.05f));    // TODO: Adjust speed after testing phase
                        break;
                    case 4:
                        // "Welchen Gegenstand packst du zuerst ein?"
                        StopAudioSource();
                        nextButton.interactable = false;
                        ClearGameObject(tablePrefab);
                        InstantiateQuiz(quizPrefab);
                        PlayAudioClip(audioClip3);
                        break;
                    case 5:
                        // "Welchen Gegenstand nimmst du als nächstes auf?"
                        StopAudioSource();
                        ClearGameObject(quizPrefab);  
                        quizQuestion = "Welche Gegenstand nehmen wir als nächstes?";
                        quizAnswer0 = "Geld";
                        quizAnswer1 = "Schmuck";
                        quizAnswer2 = "Goldmünzen"; // <- Correct Answer
                        quizAnswer3 = "Edelsteine";
                        correctAnswerIndex = 2;
                        InstantiateQuiz(quizPrefab);
                        ClearText();
                        PlayAudioClip(audioClip4);
                        break;
                    case 6:
                        // Wie viele Anteile kannst du vom letzten Gegenstand einpacken? 
                    case 7:
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
                        StartCoroutine(AnimateText(Level_2_Message_Part1, 0.00000075f));    // TODO: Adjust speed after testing phase
                        break;
                    case 1:
                        ClearText();
                        nextButton.interactable = false;
                        InstantiateTable(tablePrefab);
                        break;
                    case 2:
                        nextButton.interactable = false;
                        ClearGameObject(tablePrefab);
                        InstantiateQuiz(quizPrefab);
                        break;
                    case 3:
                        nextButton.interactable = false;
                        ClearGameObject(quizPrefab);    
                        quizQuestion = "Welche Gegenstand nehmen wir als nächstes?";
                        quizAnswer0 = "Geld";
                        quizAnswer1 = "Schmuck";
                        quizAnswer2 = "Goldmünzen"; // <- Correct Answer
                        quizAnswer3 = "Edelsteine";
                        correctAnswerIndex = 2;
                        InstantiateQuiz(quizPrefab);
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
                        break;
                    case 1:
                        CloseTutorialCanvas();
                        break;
                }
                break;
            case "Level4":
                ClearText();
                InstantiateTable(tablePrefab);
                break;
            case "Level5":
                switch(contentPage){
                    case 0:
                        nextButton.interactable = false;
                        InstantiateTable(tablePrefab);
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
            yield return new WaitForSeconds(delay);
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
    private String Level_2_Message = "Okay, jetzt sollst du gem. dem Greedy-Algorithmus die Objekte mit der besten " +
    "Wertigkeit auswählen, bis der Rucksack voll ist. Fange mit dem wertvollsten Objekt an, gefolgt von dem" + 
    " zweitwertvollsten etc.\n\n" + "In der folgenden Tabelle siehst du die Objekte mit ihren Werten und Gewichten. " +
    "Die Wertigkeit kannst du einfach berechnen, indem du den Wert durch das Gewicht teilst.";
    private String Level_2_Message_Part1 = "Okay, jetzt haben die Goldmünzen eine  Wertigkeit von 2 ,die Edelsteine von 2/3 " +
    "und das Geld hat eine Wertigkeit von 1/6. Jetzt fehlen nur noch die Wertigkeiten von unseren Keksen und von unserem Schmuck."+
    "\n\nTrage sie in der folgenden Tabelle ein.\n\nTipp für Kleinkriminelle: Notiere dir auf einem Zettel die Wertigkeiten der "+
    "Gegenstände, beor du die Tabelle schließt.";
}