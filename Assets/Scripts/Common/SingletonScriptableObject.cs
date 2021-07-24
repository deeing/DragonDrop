using UnityEngine;

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    static T _instance = null;
    public static T instance
    {
        get
        {
            if (!_instance)
            {
                T[] otherInstances = GetAllExistingT();
                //error messages cannot be made through Logger, because if Logger is the offender, it will not yet have a instance at this point
                //which of course would prevent it from working altogether
                if(otherInstances.Length == 0)
                {
                    string errorMsg = "==MISSING SINGLETON ERROR== No singleton scriptable objects of type " + typeof(T) + " exist!";
                    Debug.LogError(errorMsg);
                    return null;
                }
                else if(otherInstances.Length > 1)
                {
                    string errorMsg = "==MULTIPLE SINGLETON ERROR== Multiple instances of " + typeof(T) + " exist!  Ensure only one instance ever exists!";
                    Debug.LogError(errorMsg);
                }

                _instance = otherInstances[0];
                // DISCUSS: He had this. Do we need this?
                _instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
            }
            return _instance;
        }
    }

    private void Awake()
    {
        T[] otherInstances = GetAllExistingT();
        if (otherInstances.Length > 1)
        {
            //we can log this error through Logger because if Logger is the multiple singleton offender, at this point, we know it has at least once instance
            //so we can just use that one to do this action
            Debug.LogError( "Multiple instances of " + typeof(T) + " exist!  Ensure only one instance ever exists!");
        }
    }

    private static T[] GetAllExistingT()
    {
        return Resources.LoadAll<T>("");
    }
}
