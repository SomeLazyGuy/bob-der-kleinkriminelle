using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuUIController : MonoBehaviour {
    [SerializeField] private VideoPlayer introPlayer;
    [SerializeField] private Image introThumb;
    [SerializeField] private GameObject text;

    private bool _starting;

    private void Start() {
        introPlayer.loopPointReached += EndReached;
    }

    private void Update() {
        if (_starting) return;
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            _starting = true;
            StartGame();
        }
    }
    
    public void StartGame() {
        introPlayer.Play();
        introThumb.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    private void EndReached(VideoPlayer vp) {
        LevelManager.Instance.SwitchScene("Level1");
    }
}
