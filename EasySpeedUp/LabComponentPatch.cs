using HarmonyLib;

namespace EasySpeedUp;

public static class LabComponentPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(LabComponent), nameof(LabComponent.InternalUpdateAssemble))]
    public static void AssemblePrefix(ref LabComponent __instance, out PatchState __state)
    {
        __state = new PatchState
        {
            TimeSpend = __instance.timeSpend,
            ExtraTimeSpend = __instance.extraTimeSpend
        };
        __instance.timeSpend /= EasySpeedUpLib.SpeedScale;
        __instance.extraTimeSpend /= EasySpeedUpLib.SpeedScale;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(LabComponent), nameof(LabComponent.InternalUpdateAssemble))]
    public static void AssemblePostfix(ref LabComponent __instance, PatchState __state)
    {
        __instance.timeSpend = __state.TimeSpend;
        __instance.extraTimeSpend = __state.ExtraTimeSpend;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(LabComponent), nameof(LabComponent.InternalUpdateResearch))]
    public static void ResearchPrefix(
        float power,
        ref float speed,
        int[] consumeRegister,
        ref TechState ts,
        ref int techHashedThisFrame,
        ref long uMatrixPoint,
        ref long hashRegister)
    {
        speed *= EasySpeedUpLib.SpeedScale;
    }
}