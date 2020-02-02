using System.Collections;
using UnityEngine;

public class FadeAndSwitchScenes : MonoBehaviour
{
    public bool timer = true;
    public float timeUntilFadeAndSwitch;
    public CameraFaderGroup faderGroup;
    public GameManager gameManeger;

    void Start() {
        if (timer) {
            Invoke("_fadeAndSwitch", timeUntilFadeAndSwitch);
        }
    }

    void Update() {
        if (!timer) {
            if (OVRInput.GetDown(OVRInput.Button.Any)) {
                _fadeAndSwitch();
            }
        }
    }
    private void _fadeAndSwitch() {
        StartCoroutine(FadeAndSwitch());
    }

    IEnumerator FadeAndSwitch() {
        yield return faderGroup.FadeOut();
        gameManeger.NextScene();
    }
}
