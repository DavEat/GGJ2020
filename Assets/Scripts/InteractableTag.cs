using UnityEngine;

public class InteractableTag : MonoBehaviour
{
    public enum Tag
    {
        none = 0,
        key,
        plank
    }

    public Tag tag;
}
