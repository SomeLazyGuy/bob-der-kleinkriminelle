using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTutorialManagerButton : MonoBehaviour{
    private TutorialManager tutorialManager;    
    private void Awake() {
        // Find the TutorialManager component in the scene
        tutorialManager = FindObjectOfType<TutorialManager>();
    }
    
    [ContextMenu("Execute Challenge Finished")]
    private void ExecuteChallengeFinished(){
        if (tutorialManager != null){
            Debug.Log("Executing Challenge Finished");
            tutorialManager.ChallengeFinished();
        } else{
            Debug.LogError("Tutorial Manager is null"); 
        }
    }

}
