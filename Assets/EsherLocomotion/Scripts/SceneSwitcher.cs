using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
    public string sceneToLoad;

	void Update () {
	    if (NextSceneDown()) {
	        SceneManager.LoadScene(sceneToLoad);
        }
	}

    private bool NextSceneDown()
    {
        return OVRInput.GetDown(OVRInput.Button.One);
    }
}
