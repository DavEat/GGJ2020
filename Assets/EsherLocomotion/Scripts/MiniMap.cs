using SuperFindPlugin;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform _origin;

    public Transform _miniAvatarWrapper;
    public Transform _mH; //head
    public Transform _mRH; //l hand
    public Transform _mLH; //r hand
    public Transform _mB; //body
    public Transform _H;
    public Transform _RH;
    public Transform _LH;
    public Transform _PA; //playarea

    private Transform _originalGeometry;

    void Start() {
        var geometryWrapper = SuperFind.Find("GeometryWrapper");
        _originalGeometry = geometryWrapper.transform;
        var duplicate = Instantiate(geometryWrapper) as GameObject;
        duplicate.SetActive(true);
        duplicate.transform.SetParent(_origin, false);
        duplicate.transform.localRotation = Quaternion.identity;
        _miniAvatarWrapper.SetParent(transform);

        var duplicateColliders = duplicate.GetComponentsInChildren<Collider>();
        foreach (var col in duplicateColliders) {
            Destroy(col);
        }

        var bounds = new Bounds();
        foreach (var rend in duplicate.GetComponentsInChildren<Renderer>()) {
            bounds.Encapsulate(rend.bounds);
        }

        transform.localPosition = -_origin.InverseTransformPoint(bounds.center) + Vector3.up * (bounds.extents.y / _origin.lossyScale.x);
    }

    void Update() {
        _mH.localPosition = _originalGeometry.InverseTransformPoint(_H.position);
        _mH.localRotation = Quaternion.Inverse(_originalGeometry.rotation) * _H.rotation;
        _mH.localScale = _H.lossyScale;

        _mLH.localPosition = _originalGeometry.InverseTransformPoint(_LH.position);
        _mLH.localScale = _LH.lossyScale;

        _mRH.localPosition = _originalGeometry.InverseTransformPoint(_RH.position);
        _mRH.localScale = _RH.lossyScale;

        _mB.position = _mH.position;
        _mB.localRotation = Quaternion.Inverse(_originalGeometry.rotation) *_PA.rotation;
        _mB.localScale = _H.lossyScale;
    }
}
