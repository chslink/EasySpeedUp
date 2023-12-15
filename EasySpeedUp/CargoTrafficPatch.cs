using HarmonyLib;

namespace EasySpeedUp;

public static class CargoTrafficPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(CargoTraffic), nameof(CargoTraffic.CargoPathsGameTickSync))]
    public static bool CargoPathSyncPrefix(CargoTraffic __instance)
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
    public static bool CargoPathAsyncPrefix(CargoTraffic __instance,
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

    [HarmonyPrefix]
    [HarmonyPatch(typeof(CargoTraffic), nameof(CargoTraffic.SpraycoaterGameTick))]
    public static bool SplitterGameTickPrefix(CargoTraffic __instance)
    {
        var entityAnimPool = __instance.factory.entityAnimPool;
        var consumeRegister =
            GameMain.statistics.production.factoryStatPool[__instance.factory.index].consumeRegister;
        PerformanceMonitor.BeginSample(ECpuWorkEntry.Belt);
        for (var i = 0; i < EasySpeedUpLib.SpeedScale; i++)
        {
            for (var index = 1; index < __instance.spraycoaterCursor; ++index)
            {
                if (__instance.spraycoaterPool[index].id == index)
                    __instance.spraycoaterPool[index].InternalUpdate(__instance, entityAnimPool, consumeRegister);
            }
        }

        PerformanceMonitor.EndSample(ECpuWorkEntry.Belt);
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(CargoTraffic), nameof(CargoTraffic.PilerGameTick))]
    public static bool PilerGameTickPrefix(CargoTraffic __instance)
    {
        PerformanceMonitor.BeginSample(ECpuWorkEntry.Belt);
        var entityAnimPool = __instance.factory.entityAnimPool;
        var powerRatio = 0.0f;
        const float num1 = 0.016666668f;
        for (var index = 1; index < __instance.pilerCursor; ++index)
        {
            if (__instance.pilerPool[index].id == index)
            {
                for (var i = 0; i < EasySpeedUpLib.SpeedScale; i++)
                {
                    var num2 = __instance.pilerPool[index].InternalUpdate(__instance, entityAnimPool, out powerRatio);
                    var entityId = __instance.pilerPool[index].entityId;
                    entityAnimPool[entityId].state =
                        (uint)((int)num2 + __instance.pilerPool[index].slowlyBeltSpeed * 10 +
                               (__instance.pilerPool[index].isWorkForward ? 100 : 0));
                    entityAnimPool[entityId].power = powerRatio;
                    var time = entityAnimPool[entityId].time;
                    entityAnimPool[entityId].time = __instance.pilerPool[index].isWorkForward
                        ? (float)((time + num1 * (double)powerRatio *
                            __instance.pilerPool[index].slowlyBeltSpeed) % 10.0)
                        : (float)((time - num1 * (double)powerRatio *
                            __instance.pilerPool[index].slowlyBeltSpeed) % 10.0);
                }
            }
        }

        PerformanceMonitor.EndSample(ECpuWorkEntry.Belt);
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(CargoTraffic), nameof(CargoTraffic.SplitterGameTick))]
    public static bool SplitterGameTickPrefix(CargoTraffic __instance, long time)
    {
        PerformanceMonitor.BeginSample(ECpuWorkEntry.Splitter);
        for (var i = 0; i < EasySpeedUpLib.SpeedScale; i++)
        {
            for (var index = 1; index < __instance.splitterCursor; ++index)
            {
                if (__instance.splitterPool[index].id == index)
                    __instance.UpdateSplitter(ref __instance.splitterPool[index], time + i);
            }
        }

        PerformanceMonitor.EndSample(ECpuWorkEntry.Splitter);
        return false;
    }
}