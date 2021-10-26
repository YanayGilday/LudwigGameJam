using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeSetter : MonoBehaviour
{
    public Start start;
    public void set0()
    {
        GameMode.CgameMode = 0;
        start.loadWorld();
    }
    public void set1()
    {
        GameMode.CgameMode = 1;
        start.loadWorld();
    }
    public void set2()
    {
        GameMode.CgameMode = 2;
        start.loadWorld();
    }
    public void set3()
    {
        GameMode.CgameMode = 3;
        start.loadWorld();
    }

}
