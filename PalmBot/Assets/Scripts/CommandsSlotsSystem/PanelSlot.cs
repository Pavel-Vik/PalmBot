using UnityEngine;
using UnityEngine.UI;

/* Sits on all InventorySlots. */
public class PanelSlot : MonoBehaviour
{
    public Image icon;

    Command command; // Current command in the slot

    // Add command to the slot
    public void AddCommand (Command newCommand)
    {
        command = newCommand;

        icon.sprite = command.icon;
        icon.enabled = true;
    }

    // Clear the slot
    public void ClearSlot()
    {
        command = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    // Remove the command from the slot
    public void OnRemoveButton ()
    {
        CommandPanel.instance.Remove(command);
    }
}
