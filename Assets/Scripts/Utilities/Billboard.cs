using UnityEngine;

[ExecuteInEditMode]
public class Billboard : MonoBehaviour
{
    public Transform target;

    void Update() {
        if(target != null)
            transform.LookAt(target);
    }
}
