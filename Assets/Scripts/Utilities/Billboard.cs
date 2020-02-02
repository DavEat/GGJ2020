using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform target;

    void Update() {
        transform.LookAt(target);
    }
}
