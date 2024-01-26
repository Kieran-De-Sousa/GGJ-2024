using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents a game event.
/// </summary>
/// <remarks>
/// It is used for event-based communication in the game.
/// </remarks>
/// <seealso cref="https://www.youtube.com/watch?v=7_dyDmF0Ktw"/>
/// <seealso cref="https://www.youtube.com/watch?v=6NRqwL3N5Go"/>
/// <seealso cref="https://www.gamedevbeginner.com/events-and-delegates-in-unity/"/>
/// <author>Kieran</author>
[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listeners = new List<GameEventListener>();

    /// <summary>
    /// Loops through all Listeners and calls their 'OnEventRaised' method
    /// </summary>
    public void Raise(Component sender, params object[] data)
    {
        foreach (GameEventListener listener in listeners)
        {
            listener.OnEventRaised(sender, data);
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}