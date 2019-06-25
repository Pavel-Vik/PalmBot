using UnityEngine;

[CreateAssetMenu(fileName = "New Command", menuName = "Commands/Command")]
public class Command : ScriptableObject
{
    new public string name = "New Command";
    public Sprite icon = null;
    public bool isDefaultCommand = false;
}
