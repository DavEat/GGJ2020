using System.Collections;
using UnityEngine;

public class GrowOnEnable : MonoBehaviour
{
    public bool X = true;
    public bool Y;
    public bool Z;
    public float initialDelay = 0;
    public float growTime;
    public bool addDelayBasedOnIndex;
    public float delayFactor;

    private Vector3 _origScale;

    void Awake()
    {
        _origScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(X ? 0.01f : _origScale.x, Y ? 0.01f : _origScale.y, Z ? 0.01f : _origScale.z);
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        yield return new WaitForSeconds(initialDelay);
        yield return null;

        float delay = addDelayBasedOnIndex ? delayFactor * transform.GetSiblingIndex() : 0;
        yield return new WaitForSeconds(delay);

        Vector3 vel = Vector3.zero;

        while (ContinueLerping())
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, _origScale, ref vel, growTime);
            yield return null;
        }

        transform.localScale = _origScale;
    }

    private bool ContinueLerping()
    {
        return (Mathf.Abs(_origScale.x - transform.localScale.x) > 0.02f || !X) &&
               (Mathf.Abs(_origScale.y - transform.localScale.y) > 0.02f || !Y) &&
               (Mathf.Abs(_origScale.z - transform.localScale.y) > 0.02f || !Z);
    }
}