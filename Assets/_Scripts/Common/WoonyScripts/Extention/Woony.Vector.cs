using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
#endif

public static partial class Woony
{
    public static Vector3 GetDirection(this Vector3 from, Vector3 to)
    {
        return (to - from).normalized;
    }

    public static Vector3 GetDirectionWithNoY(this Vector3 from, Vector3 to)
    {
        var direction = from.GetDirection(to);
        direction.y = 0;
        return direction;
    }

    public static Vector3 GetDirectionWithNoZ(this Vector3 from, Vector3 to)
    {
        var direction = from.GetDirection(to);
        direction.z = 0;
        return direction;
    }

    public static Quaternion GetDirectionWith2D(this Vector2 from, Vector2 to)
    {
        return Quaternion.Euler(to.x > from.x ? new Vector2(0, 180) : Vector2.zero);
    }

    public static Vector3 AddVector2ToXY(this Vector3 baseVector, Vector2 addedFactor)
    {
        baseVector.x += addedFactor.x;
        baseVector.y += addedFactor.y;
        return baseVector;
    }

    public static Vector3 AddVector2ToXZ(this Vector3 baseVector, Vector2 addedFactor)
    {
        baseVector.x += addedFactor.x;
        baseVector.z += addedFactor.y;
        return baseVector;
    }

    public static Vector2 AddVector3(this Vector2 baseVector, Vector3 addedFactor)
    {
        baseVector.x += addedFactor.x;
        baseVector.y += addedFactor.y;
        return baseVector;
    }

    public static Vector3 RandomVector3(float min, float max)
    {
        return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
    }

    public static Vector3 RandomVector3(float value)
    {
        return new Vector3(Random.Range(-value, value), Random.Range(-value, value), Random.Range(-value, value));
    }

    public static Vector3 RandomVector3XY(float value)
    {
        return new Vector3(Random.Range(-value, value), Random.Range(-value, value), 0);
    }

    public static Vector3 RandomVector3XZ(float value)
    {
        return new Vector3(Random.Range(-value, value), 0, Random.Range(-value, value));
    }

    public static Vector2 RandomVector2(float min, float max)
    {
        return new Vector2(Random.Range(min, max), Random.Range(min, max));
    }

    public static Vector2 RandomVector2(float value)
    {
        return new Vector2(Random.Range(-value, value), Random.Range(-value, value));
    }

    public static Vector3 RandomDonutVector3(Vector3 centerPosition, float innnerRadius, float outterRadius)
    {
        var randomPosition = Random.insideUnitCircle.normalized
            * Random.Range(innnerRadius, outterRadius);
        var spawnPosition = centerPosition
            + new Vector3(randomPosition.x, 0, randomPosition.y);
        return spawnPosition;
    }

    public static Vector3 IncreasedVector3(Vector3 vec3)
    {
        return new Vector3(SetValue(vec3.x), SetValue(vec3.y), SetValue(vec3.z));

        float SetValue(float value)
        {
            if (value == 0)
                return 0;
            else
                return value > 0 ? value + 1 : value - 1;
        }
    }

    public static float GetAngleRange360By3D(this Vector3 fromDirection, Vector3 toDirection)
    {
        return Quaternion.FromToRotation(fromDirection, toDirection).eulerAngles.y;
    }

    public static float GetAngleRange360By2D(this Vector3 fromDirection, Vector3 toDirection)
    {
        return Quaternion.FromToRotation(fromDirection, toDirection).eulerAngles.z;
    }

    public static Vector3 Bezier(Vector3 P0, Vector3 P1, Vector3 P2, Vector3 P3, float t)
    {
        Vector3 M0 = Vector3.Lerp(P0, P1, t);
        Vector3 M1 = Vector3.Lerp(P1, P2, t);
        Vector3 M2 = Vector3.Lerp(P2, P3, t);
        Vector3 B0 = Vector3.Lerp(M0, M1, t);
        Vector3 B1 = Vector3.Lerp(M1, M2, t);
        return Vector3.Lerp(B0, B1, t);
    }
}