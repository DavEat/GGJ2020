using UnityEngine;

public class InteractableTag : MonoBehaviour
{
    public enum Tag
    {
        none = 0,
        key,
    }

    public Tag tag;
}
