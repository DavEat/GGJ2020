using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMeshScale : MeshScale
{
    public float scale = 1f;

    public override float GetScale(Vector3 pos) {
        Debug.Log("static " + scale);
        return scale;
    }
}
