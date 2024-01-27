using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Custom Unity Event that allows for data to be passed in events.
/// </summary>
/// <seealso cref="https://www.youtube.com/watch?v=7_dyDmF0Ktw"/>
/// <seealso cref="https://www.youtube.com/watch?v=6NRqwL3N5Go"/>
/// <seealso cref="https://gamedevbeginner.com/events-and-delegates-in-unity/"/>
/// <author>Kieran</author>
[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> {}


/// <seealso cref="https://www.youtube.com/watch?v=7_dyDmF0Ktw"/>
/// <seealso cref="https://www.youtube.com/watch?v=6NRqwL3N5Go"/>
/// <seealso cref="https://www.gamedevbeginner.com/events-and-delegates-in-unity/"/>
/// <author>Kieran</author>
public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent = null;
    public CustomGameEvent response = null;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(Component sender, object data)
    {
        response?.Invoke(sender, data);
    }
}