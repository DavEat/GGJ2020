using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{
    public static Fade inst;
    void Awake() { inst = this; }

    public void FadeSound(AudioSource sourceToFade, float startVolume, float endVolume, float duration, float delay = 0f)
    {
        StartCoroutine(fadeSource(sourceToFade, startVolume, endVolume, duration, false, delay));
    }
    public void CrossFadeSound(AudioSource toUp, AudioSource toDown, float startVolume, float endVolume, float duration)
    {
        StartCoroutine(fadeSource(toDown, endVolume, startVolume, duration, true));
        StartCoroutine(fadeSource(toUp, startVolume, endVolume, duration));
    }

    static IEnumerator fadeSource(AudioSource sourceToFade, float startVolume, float endVolume, float duration, bool stopAtEnd = false, float delay = 0f)
    {
        if (delay > 0)
            yield return new WaitForSeconds(delay);

        float startTime = Time.time;

        while (true)
        {
            float elapsed = Time.time - startTime;

            sourceToFade.volume = Mathf.Clamp01(Mathf.Lerp(startVolume, endVolume, elapsed / duration));

            if (sourceToFade.volume == endVolume)
            {
                if (stopAtEnd) sourceToFade.Stop();

                break;
            }

            yield return null;
        }
    }

    public static IEnumerator fadeScale(Transform toFade, float startSize, float endSize, float duration, float delay = 0f)
    {
        if (delay > 0)
            yield return new WaitForSeconds(delay);

        float startTime = Time.time;

        while (true)
        {
            float elapsed = Time.time - startTime;

            float scale = Mathf.Lerp(startSize, endSize, elapsed / duration);

            toFade.localScale = Vector3.one * scale;

            if (elapsed >= duration)
                break;

            yield return null;
        }
    }
}