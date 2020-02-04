using UnityEngine;
using System;
using System.Collections;

public class CameraFader : MonoBehaviour
{
    private Material fadeMaterial = null;

    //private float fadeTime = 0.5f;
    private bool isFading = false;
    private YieldInstruction fadeInstruction = new WaitForEndOfFrame();

    private bool _isSetup;

    public void Setup(Material sharedMaterial) {
        fadeMaterial = sharedMaterial;
        fadeMaterial.color = new Color(0f, 0f, 0f, 0f);

        _isSetup = true;
    }

    private void OnDestroy() {
        if (fadeMaterial != null) {
            Destroy(fadeMaterial);
        }
    }

    public void FadeOutIn(
        float fadeTime, Color fadeColor, Action midAction = null, Action finalAction = null,
        bool stopPreviousFade = false) {
        if (!isActiveAndEnabled || !_isSetup) {
            return;
        }

        if (stopPreviousFade) {
            Debug.Log("Stopping fader coroutines");
            StopAllCoroutines();
        }
        StartCoroutine(
            _FadeOut(
                fadeTime, fadeColor,
                () => {
                    if (midAction != null)
                        midAction();
                    StartCoroutine(_FadeIn(fadeTime, fadeColor, finalAction));
                }));
    }

    public void FadeOut(float fadeTime, Color color) {
        if (isActiveAndEnabled && _isSetup)
            StartCoroutine(_FadeOut(fadeTime, color));
    }

    public void FadeIn(float fadeTime, Color color) {
        if (isActiveAndEnabled && _isSetup)
            StartCoroutine(_FadeIn(fadeTime, color));
    }

    public void SetColorUntilFadeIn(Color color) {
        fadeMaterial.color = color;
        isFading = true;
    }

    private IEnumerator _FadeIn(float fadeTime, Color fadeColor, Action callback = null) {
        //Debug.LogError("Fade In");
        float elapsedTime = 0.0f;
        fadeMaterial.color = fadeColor;
        Color color = fadeColor;
        isFading = true;
        while (elapsedTime < fadeTime) {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            fadeMaterial.color = color;
        }
        isFading = false;

        if (callback != null) {
            callback();
        }
    }

    IEnumerator _FadeOut(float fadeTime, Color fadeColor, Action callback = null) {
        if (isFading) {
            //Debug.Log("Already black, no need to fade out");
            if (callback != null) {
                callback();
            }
            yield break;
        }
        float elapsedTime = 0.0f;
        float startAlpha = fadeMaterial.color.a;
        isFading = true;
        while (elapsedTime < fadeTime) {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            float a = Mathf.Lerp(startAlpha, fadeColor.a, elapsedTime / fadeTime);
            fadeMaterial.color = new Color(fadeMaterial.color.r, fadeMaterial.color.g, fadeMaterial.color.b, a);
        }
        if (callback != null) {
            callback();
        }
    }

    private void OnPostRender() {
        if (isFading) {
            fadeMaterial.SetPass(0);
            GL.PushMatrix();
            GL.LoadOrtho();
            GL.Color(fadeMaterial.color);
            GL.Begin(GL.QUADS);
            GL.Vertex3(0f, 0f, -12f);
            GL.Vertex3(0f, 1f, -12f);
            GL.Vertex3(1f, 1f, -12f);
            GL.Vertex3(1f, 0f, -12f);
            GL.End();
            GL.PopMatrix();
        }
    }

    public bool IsFading() {
        return isFading;
    }
}