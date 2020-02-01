using UnityEngine;
using UnityEngine.SceneManagement;

//Test script that reloads scene every 7s seconds
//use dto debug playarea
public class ReloadSceneAfterSeconds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        Invoke("ReloadScene", 7f);
    }

    private void ReloadScene() {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
