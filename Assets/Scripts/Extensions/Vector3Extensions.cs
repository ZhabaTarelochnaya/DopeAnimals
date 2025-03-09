using UnityEngine;
internal static class Vector3Extensions
{
    public static void AddVector2(this Vector3 vec3, Vector2 vec2)
    {
        vec3 += new Vector3(vec2.x, vec3.y, vec2.y);
    }
}
