using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsPanel : MonoBehaviour
{
    #region Singelton

    public static CommandsPanel instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of CommandsPannel found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnCommandChanged();
    public OnCommandChanged onCommandChangedCallback;

    public int space = 12;

    public List<Command> commands = new List<Command>();

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

    public void Remove (Command command)
    {
        commands.Remove(command);

        if (onCommandChangedCallback != null)
            onCommandChangedCallback.Invoke();
    }
}
