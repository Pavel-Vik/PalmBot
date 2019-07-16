using UnityEngine;

/* This object manages the CommandPanel UI. */
public class CommandPanelUI : MonoBehaviour
{
    public Transform commandsParent; // The parent object of all the commands
    public Transform commandsParentProc1;
    public Transform commandsParentProc2;


    CommandPanel commandsPanel; // Our current panel
    //CommandPanel commandsPanelProc1;

    PanelSlot[] slots;
    PanelSlot[] slotsProc1;
    PanelSlot[] slotsProc2;

    void Start()
    {
        commandsPanel = CommandPanel.instance;
        //commandsPanelProc1 = CommandPanel.instanceProc1;

        commandsPanel.onCommandChangedCallback += UpdateUI;
        //commandsPanelProc1.onCommandChangedCallback += UpdateUI;

        slots = commandsParent.GetComponentsInChildren<PanelSlot>();
        slotsProc1 = commandsParentProc1.GetComponentsInChildren<PanelSlot>();
        slotsProc2 = commandsParentProc2.GetComponentsInChildren<PanelSlot>();
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
        // UPDATE FOR MAIN PANEL
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

        // UPDATE FOR PROC1 PANEL
        for (int i = 0; i < slotsProc1.Length; i++)
        {
            if (i < commandsPanel.commandsProc1.Count)
            {
                slotsProc1[i].AddCommand(commandsPanel.commandsProc1[i]);
            }
            else
            {
                slotsProc1[i].ClearSlot();
            }
        }

        // UPDATE FOR PROC2 PANEL
        for (int i = 0; i < slotsProc2.Length; i++)
        {
            if (i < commandsPanel.commandsProc2.Count)
            {
                slotsProc2[i].AddCommand(commandsPanel.commandsProc2[i]);
            }
            else
            {
                slotsProc2[i].ClearSlot();
            }
        }
        //Debug.Log("Updating UI");
    }
}
