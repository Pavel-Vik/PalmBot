using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    #endregion

    public delegate void OnCommandChanged();
    public OnCommandChanged onCommandChangedCallback;

    public int space = 12; // Max amount of commands in the panel

    // Our current list of items in the ComandPanel
    public List<Command> commands = new List<Command>();

    // Add a new command if enough room
    public bool Add (Command command)
    {
        if (!command.isDefaultCommand)
        {
            if (commands.Count >= space)
            {
                Debug.Log("Not enough space for new commands.");
                return false;
            }
            commands.Add(command);

            if(onCommandChangedCallback != null)
                onCommandChangedCallback.Invoke();
        }
        return true;
    }

    // Remove a command
    public void Remove (Command command)
    {
        commands.Remove(command);

        if (onCommandChangedCallback != null)
            onCommandChangedCallback.Invoke();
    }
}
