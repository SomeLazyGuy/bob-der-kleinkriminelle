using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour {
    [SerializeField] private int missingCount;

    [HideInInspector] public TutorialManager tutorialManager;
    
    private bool _finished = false;
    private int _solvedCount = 0;
    
    private void Update() {
        if (_finished) return;

        if (missingCount == _solvedCount) {
            _finished = true;
            tutorialManager.ChallengeFinished();
        }
    }

    public void SolvedCell() {
        _solvedCount++;
    }
}