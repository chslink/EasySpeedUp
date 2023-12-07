using BepInEx.Configuration;
using Path = NGPT.Path;

namespace EasySpeedUp;

using BepInEx;
using HarmonyLib;

[BepInPlugin("EasySpeedUp", "Easy Speed Up", Version)]
[BepInProcess("DSPGAME.exe")]
public class EasySpeedUpLib : BaseUnityPlugin
{
    // private static bool _enable = false;
    public static int SpeedScale
    {
        get
        {
            return _speedScale.Value switch
            {
                < 1 => 1,
                > 10 => 10,
                _ => _speedScale.Value
            };
        }
    }

    private static ConfigEntry<int> _speedScale;

    // 机甲加速相关 加速减半
    public static int MechaScale
    {
        get
        {
            var v = SpeedScale / 2;
            if (v < 1)
                return 1;
            return v > 4 ? 4 : v;
        }
    }

    // private static KeyboardShortcut _enableKey = new(KeyCode.F1, KeyCode.LeftControl);
    private const string Version = "1.0.0";


    private void Start()
    {
        // Harmony.CreateAndPatchAll(typeof(EasySpeedUpLib));
        InitConfig();
        Logger.LogInfo($"EasySpeedUp Enable: speedup {_speedScale.Value}x ");

        var harmony = new Harmony(nameof(EasySpeedUpLib));
        // 采集加速
        harmony.PatchAll(typeof(MinerComponentPatch));
        // 合成加速
        harmony.PatchAll(typeof(AssemblerComponentPatch));
        // 传送带加速
        harmony.PatchAll(typeof(CargoTrafficPatch));
        // 拾取器加速
        harmony.PatchAll(typeof(InserterComponentPatch));
        // 研究加速
        harmony.PatchAll(typeof(LabComponentPatch));
        // 机甲加速
        harmony.PatchAll(typeof(MechaForgePatch));
        harmony.PatchAll(typeof(MechaLabPatch));
        harmony.PatchAll(typeof(ForgeTaskPatch));
    }

    // private const ConfigFile configFile = new ConfigFile(Paths.ConfigPath, ) 

    public void InitConfig()
    {
        _speedScale = Config.Bind("Config/配置", "Speed Scale/加速倍率", 4,
            new ConfigDescription("Default:4 ", new AcceptableValueRange<int>(2, 10), new object[2]));
    }
}