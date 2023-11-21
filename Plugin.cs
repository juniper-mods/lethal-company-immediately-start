using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx;
using HarmonyLib;



namespace TeleportRevamp
{
    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "ImmediatelyEnterHostedGame";
        public const string PLUGIN_NAME = "ImmediatelyEnterHostedGame";
        public const string PLUGIN_VERSION = "1.0.0";
    }

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]

    public class Plugin : BaseUnityPlugin
    {
        void Awake()
        {
            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.Patch(typeof(PreInitSceneScript).GetMethod("Start", BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic), postfix: new HarmonyMethod(typeof(Skip_Patches).GetMethod("Step1", BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic)));
            harmony.Patch(typeof(MenuManager).GetMethod("Awake", BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic), postfix: new HarmonyMethod(typeof(Skip_Patches).GetMethod("Step2", BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic)));
        }

        class Skip_Patches
        {
            static void Step1(ref PreInitSceneScript __instance)
            {
                __instance.ChooseLaunchOption(true);
            }
            static void Step2(ref MenuManager __instance)
            {
                GameNetworkManager.Instance.StartHost();
            }
        }
    }
}
