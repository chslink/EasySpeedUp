using HarmonyLib;

namespace EasySpeedUp;

[HarmonyPatch(typeof(StationComponent), nameof(StationComponent.UpdateCollection))]
public class StationComponentPatch
{
    public static void Prefix(
        PlanetFactory factory,
        ref float collectSpeedRate,
        int[] productRegister)
    {
        collectSpeedRate *= EasySpeedUpLib.SpeedScale;
    }
}