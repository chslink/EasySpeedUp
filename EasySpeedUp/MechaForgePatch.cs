using HarmonyLib;

namespace EasySpeedUp;

[HarmonyPatch(typeof(MechaForge), nameof(MechaForge.GameTick))]
public class MechaForgePatch
{
    public static void Prefix(ref MechaForge __instance, out int __state)
    {
        __state = 0;
        if (__instance.tasks == null)
        {
            Assert.NotNull(__instance.tasks);
        }
        else
        {
            if (__instance.tasks.Count > 0)
            {
                var task1 = __instance.tasks[0];
                __state = task1.tickSpend;
                task1.tickSpend /= EasySpeedUpLib.MechaScale;
            }
        }
    }

    public static void Postfix(ref MechaForge __instance, int __state)
    {
        if (__state == 0)
            return;
        if (__instance.tasks.Count > 0)
        {
            var task1 = __instance.tasks[0];
            task1.tickSpend = __state;
            __instance.totalTime /= EasySpeedUpLib.MechaScale;
        }
    }
}