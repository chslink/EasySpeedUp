using HarmonyLib;

namespace EasySpeedUp;

[HarmonyPatch(typeof(PlanetTransport), nameof(PlanetTransport.GameTick))]
public class PlanetTransportPatch
{
    public class TransportState
    {
        // 行星运输船
        public float logisticDroneSpeedScale;
        // public int logisticDroneCarries;
        // 星际运输船
        public float logisticShipSpeedScale;
        // public int logisticShipCarries;
        // ?
        public float logisticCourierSpeedScale;
        // public int logisticCourierCarries;
    }

    public static void Prefix(ref PlanetTransport __instance, out TransportState __state)
    {
        var history = __instance.gameData.history;
        __state = new TransportState
        {
            logisticDroneSpeedScale = history.logisticDroneSpeedScale,
            // logisticDroneCarries = history.logisticDroneCarries,
            logisticCourierSpeedScale = history.logisticCourierSpeedScale,
            // logisticCourierCarries = history.logisticCourierCarries,
            logisticShipSpeedScale = history.logisticShipSpeedScale,
            // logisticShipCarries = history.logisticShipCarries,
        };

        history.logisticDroneSpeedScale = EasySpeedUpLib.MechaScale;
        // history.logisticDroneCarries *= EasySpeedUpLib.MechaScale;
        history.logisticCourierSpeedScale = EasySpeedUpLib.MechaScale;
        // history.logisticCourierCarries *= EasySpeedUpLib.MechaScale;
        history.logisticShipSpeedScale = EasySpeedUpLib.MechaScale;
        // history.logisticShipCarries *= EasySpeedUpLib.MechaScale;
    }

    public static void Postfix(ref PlanetTransport __instance, TransportState __state)
    {
        var history = __instance.gameData.history;
        history.logisticDroneSpeedScale = __state.logisticDroneSpeedScale;
        // history.logisticDroneCarries = __state.logisticDroneCarries;
        history.logisticCourierSpeedScale = __state.logisticCourierSpeedScale;
        // history.logisticDroneCarries = __state.logisticCourierCarries;
        history.logisticShipSpeedScale = __state.logisticShipSpeedScale;
        // history.logisticShipCarries = __state.logisticShipCarries;
    }
}