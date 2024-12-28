using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour {
    [SerializeField] private int missingCount;

    [HideInInspector] public TutorialManager tutorialManager;
    
    private int _solvedCount = 0;

    public virtual void SolvedCell() {
        _solvedCount++;
        
        if (missingCount == _solvedCount) {
            tutorialManager.ChallengeFinished();
        }
    }
}