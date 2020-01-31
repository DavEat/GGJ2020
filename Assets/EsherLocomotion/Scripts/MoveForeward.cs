using UnityEngine;

public class MoveForeward : MonoBehaviour
{
    public float speed;

	void Update () {
		transform.Translate(Vector3.right * Time.deltaTime * speed);
	}
}
