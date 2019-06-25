using UnityEngine;
using UnityEngine.UI;

public class PanelSlot : MonoBehaviour
{
    public Image icon;

    Command command;

    public void AddCommand (Command newCommand)
    {
        command = newCommand;

        icon.sprite = command.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        command = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnRemoveButton ()
    {
        CommandsPanel.instance.Remove(command);
    }
}
