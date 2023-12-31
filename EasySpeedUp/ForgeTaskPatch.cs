using HarmonyLib;

namespace EasySpeedUp;

[HarmonyPatch(typeof(ForgeTask), nameof(ForgeTask.Produce))]
public class ForgeTaskPatch
{
    public static void Prefix(ref ForgeTask __instance, out int __state)
    {
        __state = __instance.tickSpend;
        __instance.tickSpend /= EasySpeedUpLib.MechaScale;
    }

    public static void Postfix(ref ForgeTask __instance, int __state)
    {
        __instance.tickSpend = __state;
    }
}