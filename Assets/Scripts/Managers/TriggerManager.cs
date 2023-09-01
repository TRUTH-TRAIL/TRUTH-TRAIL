using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TriggerValue
{

}

public class TriggerManager
{
    private static TriggerManager _instance;
    public static TriggerManager Instance { get { Init(); return _instance; } }

    public bool[] triggerArray = new bool[1001];

    private static void Init()
    {
        if (_instance == null)
        {
            _instance = new TriggerManager();
        }
    }


    public void SetTrigger(TriggerValue trig ,bool on_off)
    {
        int value = (int)trig;
        if (value < 0 || value >= triggerArray.Length)
        {
            Debug.Log("trigger is None");
            return;
        }
            
        triggerArray[value] = on_off;
    }

    public bool GetTrigger(TriggerValue trig)
    {
        int value = (int)trig;
        if (value < 0 || value >= triggerArray.Length)
        {
            Debug.Log("trigger is None");
            return false;
        }

        return triggerArray[value];
    }

    public bool IsTriggerOn(List<TriggerValue> trigList)
    {
        bool isTrig = true;
        foreach(TriggerValue trig in trigList)
        {
            if (!triggerArray[(int)trig])
            {
                isTrig = false;
                break;
            }
        }
        return isTrig;
    }
}
