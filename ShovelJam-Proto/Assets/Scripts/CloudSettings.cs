using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Flyweight/Cloud Settings")]
public class CloudSettings : FlyweightSettings
{
    public override Flyweight Create()
    {
        var go = Instantiate(prefab);

        go.SetActive(false);
        go.name = prefab.name;

        var flyweight = go.GetOrAddComponent<Cloud>();
        flyweight.settings = this;

        return flyweight;
    }
}

