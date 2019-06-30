using UnityEngine;

/* This object manages the CommandPanel UI. */
public class CommandPanelUI : MonoBehaviour
{
    public Transform commandsParent; // The parent object of all the commands

    CommandPanel commandsPanel; // Our current panel

    PanelSlot[] slots;

    void Start()
    {
        commandsPanel = CommandPanel.instance;
        commandsPanel.onCommandChangedCallback += UpdateUI;

        slots = commandsParent.GetComponentsInChildren<PanelSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update the ComandPanel UI by:
    //		- Adding commands
    //		- Clearing empty slots
    // This is called using a delegate on the CommandPanel.
    void UpdateUI ()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < commandsPanel.commands.Count)
            {
                slots[i].AddCommand(commandsPanel.commands[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        //Debug.Log("Updating UI");
    }
}
