using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FishAndAnim")]
public class FishAndAnim : ScriptableObject
{
    public List<FishType> fishType;
    public List<AnimatorOverrideController> fishAnim;
}
 
