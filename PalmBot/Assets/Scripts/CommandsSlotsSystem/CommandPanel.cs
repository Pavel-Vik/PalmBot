using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandPanel : MonoBehaviour
{
    #region Singelton

    public static CommandPanel instance;


    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of CommandPannel found!");
            return;
        }
        instance = this;
        selectedPanel = 1;
        SelectPanel(selectedPanel);
    }

    #endregion

    public delegate void OnCommandChanged();
    public OnCommandChanged onCommandChangedCallback;

    public int space = 12; // Max amount of commands in the panel
    public int spaceProc1 = 8;
    public int spaceProc2 = 8;

    public Color32 selectedColor;
    public Color32 unselectedColor;

    public GameObject Proc1Panel;
    public GameObject Proc2Panel;


    [Range(1, 3)]
    public int selectedPanel = 1;

    // Our current list of items in the ComandPanel
    public List<Command> commands = new List<Command>();
    public List<Command> commandsProc1 = new List<Command>();
    public List<Command> commandsProc2 = new List<Command>();

    // Add a new command if enough room
    public bool Add (Command command)
    {
        if (!command.isDefaultCommand)
        {
            // MAIN PANEL
            if (selectedPanel == 1)
            {
                if (commands.Count >= space)
                {
                    Debug.Log("Not enough space for new commands.");
                    return false;
                }
                commands.Add(command);
            }

            // PROC1 PANEL
            if (selectedPanel == 2)
            {
                if (commandsProc1.Count >= spaceProc1)
                {
                    Debug.Log("Not enough space for new commands.");
                    return false;
                }
                commandsProc1.Add(command);
            }

            // PROC2 PANEL
            if (selectedPanel == 3)
            {
                if (commandsProc2.Count >= spaceProc2)
                {
                    Debug.Log("Not enough space for new commands.");
                    return false;
                }
                commandsProc2.Add(command);
            }

            if (onCommandChangedCallback != null)
                onCommandChangedCallback.Invoke();
        }
        return true;
    }

    // Remove a command
    public void Remove (int slotNumber)
    {
        if (selectedPanel == 1)
            commands.RemoveAt(slotNumber);

        else if (selectedPanel == 2)
            commandsProc1.RemoveAt(slotNumber);

        else if (selectedPanel == 3)
            commandsProc2.RemoveAt(slotNumber);

        if (onCommandChangedCallback != null)
            onCommandChangedCallback.Invoke();
    }

    public void SelectPanel (int panelNumber)
    {
        if (panelNumber == 1)
        {
            selectedPanel = 1;
            //gameObject.GetComponent<Image>().color = selectedColor;
            //Proc1Panel.GetComponent<Image>().color = unselectedColor;
            //Proc2Panel.GetComponent<Image>().color = unselectedColor;
        }
        else if (panelNumber == 2)
        {
            selectedPanel = 2;
            //gameObject.GetComponent<Image>().color = unselectedColor;
            //Proc1Panel.GetComponent<Image>().color = selectedColor;
            //Proc2Panel.GetComponent<Image>().color = unselectedColor;
        }
        else if (panelNumber == 3)
        {
            selectedPanel = 3;
            //gameObject.GetComponent<Image>().color = unselectedColor;
            //Proc1Panel.GetComponent<Image>().color = unselectedColor;
            //Proc2Panel.GetComponent<Image>().color = selectedColor;
        }
    }
}
