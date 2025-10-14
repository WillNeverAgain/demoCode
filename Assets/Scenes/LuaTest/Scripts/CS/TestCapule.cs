using System;
using System.Collections;
using System.Collections.Generic;
using Lua.Utility;
using UnityEngine;
using XLuaTest.GameLuaTest;

public class TestCapule : MonoBehaviour
{
    public Actor actor;
    private void OnDestroy()
    {
        Map.GetCurrentMapInfo().EventBusCore.Publish(new OnActorDestroyEvent.ActorDestroyInfo()
        {
            uuid = actor.Uuid
        });
    }
}
