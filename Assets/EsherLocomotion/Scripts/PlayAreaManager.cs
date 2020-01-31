using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class PlayAreaManager : MonoBehaviour
{
    public Vector2 paddingLowerRightCorner;
    public float startYRotation;

    public GameObject _geometryWrapper;
    public GameObject _startGeomWrapper;
    public Transform _playArea;
    public Transform _head;
    public Transform _startPoint;

    private Vector3 _playAreaBounds;

    private bool _playerInCorrectPosition;

    [Header("Debug")]
    public Transform _debugPlayArea;
    public GameObject _debugBoundsCenter;

    void Start() {
        _geometryWrapper.SetActive(false);
        _startGeomWrapper.SetActive(true);

        SetPlayArea();
    }
	
	void Update () {
	    if (!_playerInCorrectPosition) 
	        CheckPlayerPos();
	}

    private void CheckPlayerPos() {
        if(HeadOvertarget()){
            _geometryWrapper.SetActive(true);
            _startGeomWrapper.SetActive(false);

            _playerInCorrectPosition = true;
        }
    }

    private bool HeadOvertarget(){
        var headPosFloor = new Vector3(_head.position.x, 0, _head.position.z);
        return Vector3.Distance(headPosFloor, _startPoint.position) <= 0.25f;
    }

    private void SetPlayArea() {
        CalcBoundsPos();
        PositionGeometry();

        _startPoint.position = _geometryWrapper.transform.position;
    }

    private void CalcBoundsPos() {
        _playAreaBounds = OVRManager.boundary.GetDimensions(OVRBoundary.BoundaryType.PlayArea);

        if (_debugPlayArea != null && _debugBoundsCenter != null) {
            _debugPlayArea.localScale = new Vector3(_playAreaBounds.x, 0.01f, _playAreaBounds.z);
            _debugBoundsCenter.transform.position = Vector3.zero;
        }
    }

    private void PositionGeometry() {
        _geometryWrapper.transform.position = Vector3.zero;
        _geometryWrapper.transform.rotation = Quaternion.identity;

        _geometryWrapper.transform.Translate(new Vector3(
            _playAreaBounds.x / 2f - paddingLowerRightCorner.x,
            0 , 
            _playAreaBounds.z / 2f - paddingLowerRightCorner.y));
        _geometryWrapper.transform.Rotate(new Vector3(0, startYRotation, 0));
    }
}
