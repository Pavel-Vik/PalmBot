using UnityEngine;

public class CommandsUI : MonoBehaviour
{
    public Transform commandsParent;

    CommandsPanel commandsPanel;

    PanelSlot[] slots;

    void Start()
    {
        commandsPanel = CommandsPanel.instance;
        commandsPanel.onCommandChangedCallback += UpdateUI;

        slots = commandsParent.GetComponentsInChildren<PanelSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
