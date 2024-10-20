using UnityEngine;

public class MenuUIController : MonoBehaviour {
    public void StartGame() {
        LevelManager.Instance.SwitchScene("Level1");
    }
}
