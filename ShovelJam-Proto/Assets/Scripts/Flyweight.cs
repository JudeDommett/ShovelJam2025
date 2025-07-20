using UnityEngine;

public abstract class Flyweight : MonoBehaviour
{
    public FlyweightSettings settings; // Intrinsic state
}

public enum FlyweightType { Background, Fish, Cloud }