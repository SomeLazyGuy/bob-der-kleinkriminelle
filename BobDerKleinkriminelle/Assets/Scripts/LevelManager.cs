using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get; private set; }

    [SerializeField] private float speedScale = 1f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private Color fadeColor = Color.black;
    [SerializeField] private AnimationCurve curve = new (new Keyframe(0, 1), new Keyframe(0.5f, 0.5f, -1.5f, -1.5f), new Keyframe(1, 0));
    
    private float _alpha;
    private Texture2D _texture;
    private int _direction;
    private float _time = 1f;
    private string _nextScene;
    
    private void Awake() {
        DontDestroyOnLoad(gameObject);

        if (Instance != null) {
            Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;
    }

    private void Start() {
        _texture = new Texture2D(1, 1);
    }

    private void OnGUI() {
        if (_alpha > 0f) GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);

        if (_direction == 0) return;
        
        _time += _direction * Time.deltaTime * speedScale;
        _alpha = curve.Evaluate(_time);
        _texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, _alpha));
        _texture.Apply();

        if (_alpha >= 1f) FadeOutComplete();
        else if (_alpha <= 0f) FadeInComplete();
    }

    public void SwitchScene(string nextScene) {
        if (nextScene.Length == 0) return;

        _nextScene = nextScene;
        _direction = -1;
    }

    private void FadeOutComplete() {
        StartCoroutine(LoadNextScene());
    }

    private void FadeInComplete() {
        _alpha = 0f;
        _time = 1f;
        _direction = 0;
    }

    private IEnumerator LoadNextScene() {
        _direction = 0;
        SceneManager.LoadScene(_nextScene);
        
        yield return new WaitForSeconds(waitTime);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) {
            player.GetComponent<PlayerController>().StartLevelTransition(Vector2.up);
        }
        
        _nextScene = "";
        _alpha = 1f;
        _time = 0f;
        _direction = 1;
    }
}
