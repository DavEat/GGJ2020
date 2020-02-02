using System.Collections;
using UnityEngine;

public class FadeAndSwitchScenes : MonoBehaviour
{
    public float timeUntilFadeAndSwitch;
    public CameraFaderGroup faderGroup;
    public GameManager gameManeger;

    // Start is called before the first frame update
    void Start() {
        Invoke("_fadeAndSwitch", timeUntilFadeAndSwitch);
    }

    private void _fadeAndSwitch() {
        StartCoroutine(FadeAndSwitch());
    }

    // Update is called once per frame
    IEnumerator FadeAndSwitch() {
        yield return faderGroup.FadeOut();
        gameManeger.NextScene();
    }
}
