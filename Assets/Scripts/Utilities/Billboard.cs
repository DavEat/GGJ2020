using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update() {
        if (Camera.main && Camera.main.transform)
            transform.LookAt(Camera.main.transform);
    }
}
