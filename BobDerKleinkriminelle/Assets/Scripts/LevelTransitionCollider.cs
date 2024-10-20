using UnityEngine;

public class LevelTransitionCollider : MonoBehaviour {
    [SerializeField] private string nextScene;
    
    private void OnTriggerEnter2D(Collider2D other) {
        LevelManager.Instance.SwitchScene(nextScene);
    }
}
