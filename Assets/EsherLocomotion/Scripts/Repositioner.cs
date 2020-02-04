using UnityEngine;

public class Repositioner : MonoBehaviour
{
    public Transform _head;
    public Transform _playArea;

    private Ray _ray;
    private RaycastHit _hit;
    [SerializeField] float _distance = 3f;
    [SerializeField] LayerMask _layerMask = 0;

	void Update () {
        _ray = new Ray(_head.position, -_playArea.up);
	    if (Physics.Raycast(_ray, out _hit, _distance, _layerMask)) {
	        var cross = Vector3.Cross(_playArea.up, _hit.normal);

            //correct playarea angle if walking on walls
            if (cross != Vector3.zero && ReorientableSurface()) {
	            _playArea.RotateAround(_hit.point, cross, Vector3.Angle(_hit.normal, _playArea.up));
            }

	        //correct scale
	        if (_hit.collider.GetComponent<MeshScale>() != null) {
	            ScaleAround(_playArea, _hit.point, _hit.collider.GetComponent<MeshScale>().GetScale(_hit.point));
	        }

	        //position playarea on hitpoint to insure player is always standing on a surface
	        var hitLocal = _playArea.InverseTransformPoint(_hit.point);
            _playArea.Translate(Vector3.up * hitLocal.y * _playArea.localScale.x, Space.Self);
	    }
    }

    private bool ReorientableSurface() {
        return _hit.collider.gameObject.layer != LayerMask.NameToLayer("Ramp");
    }

    private void ScaleAround(Transform target, Vector3 pivot, float newScale) {
        
        Vector3 pivotLocal = target.InverseTransformPoint(pivot);
        target.localScale = newScale * Vector3.one;
        Vector3 pivotPostScale = target.TransformPoint(pivotLocal);
        target.position += pivot - pivotPostScale;  
    }
}
