using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuUIController : MonoBehaviour {
    [SerializeField] private VideoPlayer introPlayer;
    [SerializeField] private Image introThumb;
    [SerializeField] private Button playButton;

    private void Start() {
        introPlayer.loopPointReached += EndReached;
    }
    
    public void StartGame() {
        introPlayer.Play();
        introThumb.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
    }

    private void EndReached(VideoPlayer vp) {
        LevelManager.Instance.SwitchScene("Level1");
    }
}
