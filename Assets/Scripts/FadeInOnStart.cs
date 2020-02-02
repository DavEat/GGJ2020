using UnityEngine;

public class FadeInOnStart : MonoBehaviour
{
    public CameraFaderGroup fader;

    void Start() {
        StartCoroutine(fader.FadeIn());
    }
}
