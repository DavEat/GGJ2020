using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityScale = -9.81f; 
    public static Vector3 orientation = Vector3.down;

    public Transform gravityTarget = null;

    void FixedUpdate()
    {
        if (gravityTarget)
            SetOrientation(gravityTarget);
    }
    public void SetOrientation(Transform transform)
    {
        orientation = transform.up * gravityScale;
    }
}
