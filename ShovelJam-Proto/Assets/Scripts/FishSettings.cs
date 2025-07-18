using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Flyweight/Fish Settings")]
public class FishSettings : FlyweightSettings
{
    public SkyType skyType;

    [SerializeField] public List<FishAndAnim> FishTypes;

    public override Flyweight Create()
    {
        var go = Instantiate(prefab);

        go.SetActive(false);
        go.name = prefab.name;

        var flyweight = go.GetOrAddComponent<Fish>();
        flyweight.settings = this;

        return flyweight;
    }
}
 
