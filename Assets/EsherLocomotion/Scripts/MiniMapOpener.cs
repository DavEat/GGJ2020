using UnityEngine;

public class MiniMapOpener : MonoBehaviour
{
    public GameObject _minimap;

	void Update () {
	    _minimap.SetActive(OVRInput.Get(OVRInput.Button.Three));
    }
}
