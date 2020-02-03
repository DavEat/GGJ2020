using UnityEngine;

public class PlayAreaManager : MonoBehaviour
{
    public GameObject _geometryWrapper;
    public CalibrationStartPosition _startPosition;
    public float _startYRotation;

    [Space(30)]
    public GameObject _calibrationWrapper;
    public Transform _head;
    public Transform _startPoint;
    public GameObject _playAreaReference;

    private Vector3 _playAreaBounds;

    private bool _playerInCorrectPosition;

    void Start() {
        ShowCalibrationArea(true);
        SetPlayArea();
    }
	
	void Update () {
	    if (!_playerInCorrectPosition) 
	        CheckPlayerPos();

#if UNITY_EDITOR
        //debug
        if (Input.GetKeyDown(KeyCode.Space)) {
            ShowCalibrationArea(false);
            _playerInCorrectPosition = true;
        }
#endif
    }

    private void ShowCalibrationArea(bool show) {
        if(_geometryWrapper != null)
            _geometryWrapper.SetActive(!show);
        _calibrationWrapper.SetActive(show);
    }

    private void CheckPlayerPos() {
        if(HeadOvertarget()) {
            ShowCalibrationArea(false);
            _playerInCorrectPosition = true;
        }
    }

    private bool HeadOvertarget(){
        var headPosFloor = new Vector3(_head.position.x, 0, _head.position.z);
        return Vector3.Distance(headPosFloor, _startPoint.position) <= 0.25f;
    }

    private void SetPlayArea() {
        _startPoint.localPosition = GetStartPosPosition(_startPosition);
        _startPoint.localEulerAngles = new Vector3(0, _startYRotation, 0);

        if(_playAreaReference != null)
            _playAreaReference.SetActive(false);
    }

    private Vector3 GetStartPosPosition(CalibrationStartPosition startPos) {
        switch (startPos) {
            case CalibrationStartPosition.A1:
                return new Vector3(-1f, 0 , 1f);
            case CalibrationStartPosition.A2:
                return new Vector3(0, 0, 1f);
            case CalibrationStartPosition.A3:
                return new Vector3(1f, 0, 1f);
            case CalibrationStartPosition.B1:
                return new Vector3(-1f, 0, 0);
            case CalibrationStartPosition.B3:
                return new Vector3(1f, 0, 0);
            case CalibrationStartPosition.C1:
                return new Vector3(-1f, 0, -1f);
            case CalibrationStartPosition.C2:
                return new Vector3(0, 0, -1f);
            case CalibrationStartPosition.C3:
                return new Vector3(1f, 0, -1f);
            default:
            case CalibrationStartPosition.B2:
                return new Vector3(0, 0, 0);
        }
    }
}

public enum CalibrationStartPosition {
    A1,A2,A3,B1,B2,B3,C1,C2,C3
}
