using HarmonyLib;

namespace EasySpeedUp;

[HarmonyPatch(typeof(MechaLab), nameof(MechaLab.GameTick))]
public class MechaLabPatch
{
    public static void Prefix(MechaLab __instance, out int __state)
    {
        __state = __instance.gameHistory.techSpeed;
        __instance.gameHistory.techSpeed *= EasySpeedUpLib.MechaScale;
    }

    public static void Postfix(MechaLab __instance, int __state)
    {
        __instance.gameHistory.techSpeed = __state;
    }
}