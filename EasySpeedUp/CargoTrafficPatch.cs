using HarmonyLib;

namespace EasySpeedUp;

public static class CargoTrafficPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(CargoTraffic), nameof(CargoTraffic.CargoPathsGameTickSync))]
    public static bool SyncPrefix(CargoTraffic __instance)
    {
        PerformanceMonitor.BeginSample(ECpuWorkEntry.Belt);
        for (var i = 0; i < EasySpeedUpLib.SpeedScale; i++)
        {
            for (var index = 1; index < __instance.pathCursor; ++index)
            {
                if (__instance.pathPool[index] != null && __instance.pathPool[index].id == index)
                    __instance.pathPool[index].Update();
            }
        }

        PerformanceMonitor.EndSample(ECpuWorkEntry.Belt);
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(CargoTraffic), nameof(CargoTraffic.CargoPathsGameTickAsync))]
    public static bool AsyncPrefix(CargoTraffic __instance,
        int _usedThreadCnt,
        int _curThreadIdx,
        int _minimumMissionCnt)
    {
        int _start;
        int _end;
        if (!WorkerThreadExecutor.CalculateMissionIndex(1, __instance.pathCursor - 1, _usedThreadCnt, _curThreadIdx,
                _minimumMissionCnt, out _start, out _end))
            return false;

        for (var i = 0; i < EasySpeedUpLib.SpeedScale; i++)
        {
            for (var index = _start; index < _end; ++index)
            {
                if (__instance.pathPool[index] != null && __instance.pathPool[index].id == index)
                    __instance.pathPool[index].Update();
            }
        }

        return false;
    }
}