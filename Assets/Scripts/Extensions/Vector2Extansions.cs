using UnityEngine;

static class Vector2Extansions
{
    public static Vector3 ToVector3(this Vector2 vec2) => new(vec2.x, 0, vec2.y);
    public static Vector3 ToVector3(this Vector2 vec2, float z) => new(vec2.x, z, vec2.y);
}