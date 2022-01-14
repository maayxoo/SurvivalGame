using UnityEngine;

//Weapon is a scriptable object meaning many different weapons with different values can be made

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons")]
public class Weapon : ScriptableObject
{
    new public string name = "New Weapon";
    public int damageModifier = 1;
}
