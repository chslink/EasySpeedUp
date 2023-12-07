using HarmonyLib;

namespace EasySpeedUp;

public static class InserterComponentPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(InserterComponent), nameof(InserterComponent.InternalUpdate))]
    public static void UpdatePrefix(ref InserterComponent __instance, out int __state)
    {
        __state = __instance.stt;
        __instance.stt /= EasySpeedUpLib.SpeedScale;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(InserterComponent), nameof(InserterComponent.InternalUpdate))]
    public static void UpdatePostfix(ref InserterComponent __instance, int __state)
    {
        __instance.stt = __state;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(InserterComponent), nameof(InserterComponent.InternalUpdateNoAnim))]
    public static void UpdateNoAnimPrefix(ref InserterComponent __instance, out int __state)
    {
        __state = __instance.stt;
        __instance.stt /= EasySpeedUpLib.SpeedScale;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(InserterComponent), nameof(InserterComponent.InternalUpdateNoAnim))]
    public static void UpdateNoAnimPostfix(ref InserterComponent __instance, int __state)
    {
        __instance.stt = __state;
    }
}