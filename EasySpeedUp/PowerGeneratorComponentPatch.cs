using HarmonyLib;

namespace EasySpeedUp;

[HarmonyPatch(typeof(PowerGeneratorComponent), nameof(PowerGeneratorComponent.GameTick_Gamma))]
public class PowerGeneratorComponentPatch
{
    public static void Prefix(ref PowerGeneratorComponent __instance, out long __state)
    {
        __state = __instance.capacityCurrentTick;
        __instance.capacityCurrentTick *= EasySpeedUpLib.SpeedScale;
    }

    public static void Postfix(ref PowerGeneratorComponent __instance, long __state)
    {
        __instance.capacityCurrentTick = __state;
    }
}