using UnityEngine;

public class LinearMeshScale : MeshScale
{
    public float minScale;
    public float maxScale;

    public Transform _minRef;
    public Transform _maxRef;

    [Header("Debug")]
    public Transform _debugProjectedPos;

    public override float GetScale(Vector3 pos) {

        var projectedPos = _maxRef.position + Vector3.Project(pos - _maxRef.position, _minRef.position - _maxRef.position);

        if (_debugProjectedPos != null)
            _debugProjectedPos.position = projectedPos;

        var maxToProj = projectedPos - _maxRef.position;
        var maxToMin = _minRef.position - _maxRef.position;

        if (Vector3.Dot(maxToMin, maxToProj) >= 0) {
            var x = Mathf.Clamp(maxToProj.sqrMagnitude, 0, maxToMin.sqrMagnitude);
            var f = x / maxToMin.sqrMagnitude;

            var value = Mathf.Lerp(maxScale, minScale, f);
            Debug.Log("linear " + value);
            return value;
        } else {
            Debug.Log("linear " + maxScale);
            return maxScale;
        }
    }
}
