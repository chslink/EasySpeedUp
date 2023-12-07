using HarmonyLib;

namespace EasySpeedUp;

[HarmonyPatch(typeof(AssemblerComponent), nameof(AssemblerComponent.InternalUpdate))]
public class AssemblerComponentPatch
{
  

    public static void Prefix(ref AssemblerComponent __instance, out PatchState __state)
    {
        __state = new PatchState
        {
            TimeSpend = __instance.timeSpend,
            ExtraTimeSpend = __instance.extraTimeSpend
        };
        __instance.timeSpend /= EasySpeedUpLib.SpeedScale;
        __instance.extraTimeSpend /= EasySpeedUpLib.SpeedScale;
    }

    public static void Postfix(ref AssemblerComponent __instance, PatchState __state)
    {
        __instance.timeSpend = __state.TimeSpend;
        __instance.extraTimeSpend = __state.ExtraTimeSpend;
    }
}