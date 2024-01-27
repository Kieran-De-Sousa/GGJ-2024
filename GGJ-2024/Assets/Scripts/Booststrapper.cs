using UnityEngine;

/// <summary>
/// Creates resources and marks them with don't destroy on load.
/// - Prefabs stored in Assets/Resources and listed in _ResourceNames will be instantiated 
///   on startup, in whichever scene you're in. This ensures that manager dependencies are always 
///   available in an elegant way. 
/// - A detailed explanation can be viewed at https://www.youtube.com/watch?v=zJOxWmVveXU 
/// </summary>
public static class Booststrapper // This should be Bootstrapper but I made a typo and now cba to change
{
    private static readonly string[] _ResourceNames =
    {
        "SingletonManagers"
    };

    // This function is called once on startup and creates all prefabs listed in _ResourceNames
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        foreach (string resourceName in _ResourceNames)
        {
            Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load(resourceName)));
        }
    }
}
