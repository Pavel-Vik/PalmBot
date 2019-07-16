using UnityEngine;

public class SelectCommand : MonoBehaviour
{
    public Command command;

    public void Select()
    {
        Debug.Log("Selected command: " + command.name);
        bool wasSelected = CommandPanel.instance.Add(command);
        //bool wasSelected1 = CommandPanel.instanceProc1.Add(command);
    }
}
