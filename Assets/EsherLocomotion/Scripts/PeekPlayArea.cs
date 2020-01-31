using UnityEngine;

public class PeekPlayArea : MonoBehaviour
{
    public GameObject _playArea;
    public GameObject _center;

    private const OVRInput.RawButton menuButton = OVRInput.RawButton.Start;

    void Update () {
        if (OVRInput.GetUp(menuButton)) {
            var visible = !_playArea.activeInHierarchy;
            _playArea.SetActive(visible);
            _center.SetActive(visible);
        }
    }
}
