using UnityEngine;



[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons")]
public class Weapon : ScriptableObject
{
    new public string name = "New Weapon";
    public int damageModifier = 1;
    public SkinnedMeshRenderer mesh;
}
