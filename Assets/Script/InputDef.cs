using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action
{
    public string inputCall;
    public string alternateInputCall;
    public float value;
}

// public class BaseActionList{
//     List<Action> actionList = new List<Action>() {
//         new Action() horizontalMovement,
//         new verticalMovement(),
//         new jump(),
//         new baseAttack(),
//         new special1(),
//         new special2()
//     };
// }