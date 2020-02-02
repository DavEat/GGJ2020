using UnityEngine;

public class PlayAreaView : MonoBehaviour
{
    public TextMesh _textMesh;
    public Color _okCOlor;
    public Color _smallColor;
    public Transform _playAreaSquare;

    void Update()
    {
        Vector3 bounds = OVRManager.boundary.GetDimensions(OVRBoundary.BoundaryType.PlayArea);
        _textMesh.text = Round(bounds.x) + "x" + Round(bounds.z) + "m";
        bool bigEnough = bounds.x > 3f && bounds.z > 3f;
        _textMesh.color = bigEnough ? _okCOlor : _smallColor;
        _playAreaSquare.localScale = new Vector3(bounds.x / 10f, bounds.z / 10f, 1);
        _playAreaSquare.gameObject.SetActive(!bigEnough);
    }

    public float Round(float value) {
        return Mathf.Round(value * 10) / 10f;
    }
}
