using System;
using BepInEx;
using UnityEngine;
using Utilla;
using WhoIsSpeeding;
using WhoIsTalking;

namespace WhoIsSpeeding
{
    // This is your mod's main class.
    [BepInDependency("Graze.WhoIsTalking")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        void Start()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }
    }
}