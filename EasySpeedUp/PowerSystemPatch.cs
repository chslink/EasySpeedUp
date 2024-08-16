using HarmonyLib;

namespace EasySpeedUp;

[HarmonyPatch(typeof(PowerSystem), nameof(PowerSystem.GameTick))]
public class PowerSystemPatch
{
    public static void Prefix(ref PowerSystem __instance)
    {
        if (__instance.genPool == null) return;
        for (var i = 0; i < __instance.genPool.Length; i++)
        {
            __instance.genPool[i].genEnergyPerTick *= EasySpeedUpLib.MechaScale;
        }
    }

    public static void Postfix(ref PowerSystem __instance)
    {
        if (__instance.genPool == null) return;
        for (var i = 0; i < __instance.genPool.Length; i++)
        {
            __instance.genPool[i].genEnergyPerTick /= EasySpeedUpLib.MechaScale;
        }
    }
}