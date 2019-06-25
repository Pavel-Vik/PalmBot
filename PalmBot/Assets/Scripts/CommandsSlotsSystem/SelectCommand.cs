using UnityEngine;

public class SelectCommand : MonoBehaviour
{
    public Command command;

    public void Select()
    {
        Debug.Log("Selected command: " + command.name);
        bool wasSelected = CommandsPanel.instance.Add(command);
    }
}
