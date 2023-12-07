using HarmonyLib;

namespace EasySpeedUp;

[HarmonyPatch(typeof(MinerComponent), nameof(MinerComponent.InternalUpdate))]
public class MinerComponentPatch
{
    public static void Prefix(ref MinerComponent __instance, out int __state)
    {
        __state = __instance.period;
        __instance.period /= EasySpeedUpLib.SpeedScale;
    }

    public static void Postfix(ref MinerComponent __instance, int __state)
    {
        __instance.period = __state;
    }
}