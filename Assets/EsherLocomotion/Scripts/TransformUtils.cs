using UnityEngine;

public static class TransformUtils {
    public static Vector3 GetAveragePos(Vector3[] positions) {
        if(positions.Length == 0)
            return Vector3.zero;

        Vector3 sum = Vector3.zero;
        foreach (var pos in positions) {
            Debug.Log(pos);
            sum += pos;
        }

        return sum / positions.Length;
    }
}
