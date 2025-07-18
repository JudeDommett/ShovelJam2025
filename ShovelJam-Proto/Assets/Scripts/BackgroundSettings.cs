using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Flyweight/Background Settings")]
public class BackgroundSettings : FlyweightSettings
{
    public override Flyweight Create()
    {
        var go = Instantiate(prefab);

        go.SetActive(false);
        go.name = prefab.name;

        var flyweight = go.GetOrAddComponent<Background>();
        flyweight.settings = this;

        return flyweight;
    }
}

