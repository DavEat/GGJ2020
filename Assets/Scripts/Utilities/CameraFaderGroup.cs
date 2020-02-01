using System;
using System.Collections;
using UnityEngine;

public class CameraFaderGroup : MonoBehaviour
{
    public bool startBlackUntilFadeIn;

    public CameraFaderData[] _faders;

    private Color _defaultFadeColor = new Color(0.01f, 0.01f, 0.01f);
    private float _defaultFadeTime = 0.5f;

    private void Start() {
        var sharedMat = new Material(Shader.Find("Oculus/Unlit Transparent Color"));

        foreach (var fader in _faders) {
            fader.fader.Setup(sharedMat);
        }

        if (startBlackUntilFadeIn) {
            DefaultColorUntilFadeIn();
        }
    }

#if UNITY_EDITOR
    public bool testFadeInOut;
    public bool testFadeOut;
    public bool testFadeIn;
#endif


#if UNITY_EDITOR
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(FadeOut(false));
        }

        if (Input.GetMouseButtonDown(1)) {
            StartCoroutine(FadeIn(false));
        }
    }
#endif

    public void FadeOutIn(Action midAction = null, Action finalAction = null, bool stopPreviousFade = false) {
        //Debug.LogError("Fade out in");
        Debug.Log("fadeout");
        bool doneActions = false;
        for (int i = 0; i < _faders.Length; i++) {
            if (!doneActions && _faders[i].fader.gameObject.activeInHierarchy) {
                doneActions = true;
                _faders[i].fader.FadeOutIn(
                    _defaultFadeTime, _defaultFadeColor, midAction, finalAction, stopPreviousFade);
            } else {
                _faders[i].fader.FadeOutIn(_defaultFadeTime, _defaultFadeColor, null, null, stopPreviousFade);
            }
        }
    }

    public IEnumerator FadeOut(bool vrOnly = false, Color? color = null, float? fadeTime = null) {
        var fadingTime = fadeTime ?? _defaultFadeTime;
        var fadingColor = color ?? _defaultFadeColor;

        foreach (var fader in _faders) {
            if (!vrOnly || fader.isVrCamera)
                fader.fader.FadeOut(fadingTime, fadingColor);
        }

        //allows other coroutines to wait for fading to finish
        yield return new WaitForSeconds(fadingTime);
    }

    public IEnumerator FadeIn(bool vrOnly = false, Color? color = null, float? fadeTime = null) {
        var fadingTime = fadeTime ?? _defaultFadeTime;
        var fadingColor = color ?? _defaultFadeColor;

        foreach (var fader in _faders) {
            if (!vrOnly || fader.isVrCamera)
                fader.fader.FadeIn(fadingTime, fadingColor);
        }

        //allows other coroutines to wait for fading to finish
        yield return new WaitForSeconds(fadingTime);
    }

    public void DefaultColorUntilFadeIn() {
        for (int i = 0; i < _faders.Length; i++) {
            _faders[i].fader.SetColorUntilFadeIn(_defaultFadeColor);
        }
    }

    public bool IsFading() {
        if (_faders.Length > 1) {
            //_faders[0] refers to desktop cam which isn't used on android
#if UNITY_ANDROID
            return _faders[1].fader.IsFading();
#else
            return _faders[0].fader.IsFading() || _faders[1].fader.IsFading();
#endif
        } else if (_faders.Length > 0) {
            return _faders[0].fader.IsFading();
        }
        return false;
    }
}

[System.Serializable]
public class CameraFaderData
{
    public CameraFader fader;
    public bool isVrCamera;
}