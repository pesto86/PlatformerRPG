using UnityEngine;

[CreateAssetMenu(fileName = "ShopkeeperDialogue", menuName = "Scriptable Objects/ShopkeeperDialogue")]
public class ShopkeeperDialogue : ScriptableObject
{
    [Header("Dialogue Lines")]
    public string[] lines;
}
