using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : MonoBehaviour {

    private static bool bGameStart = false;

    private static bool bIsScrollingUp = false;
    private void Start()
    {
        bIsScrollingUp = false;
        bGameStart = false;
    }
    public static bool GetGameStart()
    {
        return bGameStart;
    }

    private void SetGameStart()
    {
        //SetIsScrollingUp(false);
        bGameStart = true;
    }

    public static void SetIsScrollingUp()
    {
        bIsScrollingUp = true;
    }

    public static bool GetIsScrollingUp()
    {
       return bIsScrollingUp;
    }
}
