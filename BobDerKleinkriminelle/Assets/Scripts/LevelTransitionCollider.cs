using System;
using System.Collections;
using UnityEngine;

public class LevelTransitionCollider : MonoBehaviour {
    [SerializeField] private string nextScene;

    private bool _active = false;
    
    private void Start() {
        StartCoroutine(Activate());
    }    
     
    private IEnumerator Activate() {
        yield return new WaitForSeconds(2);
        _active = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!_active) return;
        
        other.gameObject.GetComponent<PlayerController>().StartLevelTransition(transform.position.y < 0 ? Vector2.down : Vector2.up);
        
        LevelManager.Instance.SwitchScene(nextScene);
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        _active = true;
    }
}
