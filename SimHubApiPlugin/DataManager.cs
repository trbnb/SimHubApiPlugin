using ACSharedMemory.ACC.Reader;
using ACSharedMemory.Reader;
using GameReaderCommon;
using SimHubApiPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcTools.Utils.Helpers;
using PCarsSharedMemory.AMS2.Models;

namespace SimHubApiPlugin
{
    public class DataManager
    {
        public static DataManager Instance { get; } = new DataManager();

        public GameData CurrentData { get; private set; }

        public void OnNewData(ref GameReaderCommon.GameData data)
        {
            if (data.NewData == null)
            {
                CurrentData = null;
                return;
            }

            IRawDataParser rawDataParser = null;

            switch (data)
            {
                case GameData<ACRawData> acRawData:
                    rawDataParser = new AcRawDataParser(acRawData.GameNewData.Raw, data);
                    break;
                case GameData<ACCRawData> accRawData:
                    rawDataParser = new AccRawDataParser(accRawData.GameNewData.Raw);
                    break;
                case GameData<AMS2APIStruct> ams2RawData:
                    rawDataParser = new Ams2RawDataParser(ams2RawData.GameNewData.Raw);
                    break;
            }

            CurrentData = new GameData
            {
                Speed = data.NewData.SpeedKmh.ToFloat(),
                BrakeBias = data.NewData.BrakeBias.ToFloat(),
                Gear = data.NewData.Gear,

                Rpm = data.NewData.Rpms.ToFloat(),
                MaxRpm = data.NewData.MaxRpm.ToFloat(),
                Redline = data.NewData.CarSettings_RPMShiftLight2.ToFloat(),
                ShouldShift = data.NewData.Rpms >= data.NewData.CarSettings_CurrentGearRedLineRPM,
                Turbo = data.NewData.TurboPercent.ToFloat(),

                CurrentLapTime = data.NewData.CurrentLapTime.TotalMilliseconds.ToLong(),
                LastLapTime = data.NewData.LastLapTime.TotalMilliseconds.ToLong(),
                BestLapTime = data.NewData.BestLapTime.TotalMilliseconds.ToLong(),
                FastestLapTime = rawDataParser?.FastestLapTime,
                DeltaInSeconds = rawDataParser?.DeltaInSeconds,
                IsLapValid = data.NewData.IsLapValid,

                CurrentLap = data.NewData.CurrentLap,
                TotalLaps = data.NewData.TotalLaps.TakeUnless(laps => laps == 0),
                SessionTimeLeft = data.NewData.SessionTimeLeft.TotalMilliseconds.ToLong(),

                IsInPitlane = data.NewData.IsInPitLane == 1,
                IsPitLimiterOn = data.NewData.PitLimiterOn == 1,

                TC1 = data.NewData.Let(newData =>
                {
                    if (rawDataParser?.HasTC1 != true) return null;

                    return new Assist
                    {
                        Level = newData.TCLevel,
                        IsActive = newData.TCActive == 1
                    };
                }),
                TC2 = rawDataParser?.TC2,
                ABS = data.NewData.Let(newData =>
                {
                    if (rawDataParser?.HasABS != true) return null;

                    return new Assist
                    {
                        Level = newData.ABSLevel,
                        IsActive = newData.ABSActive == 1
                    };
                }),

                EngineMap = rawDataParser?.EngineMap?.TakeUnless(it => it < 0),
                IsLightOn = rawDataParser?.IsLightOn,

                TyreCompound = rawDataParser?.TyreCompound ?? string.Empty,
                WheelInfos = rawDataParser?.WheelInfos ?? data.WheelInfos(),

                FuelRemaining = data.NewData.Fuel.ToFloat(),
                FuelPerLap = rawDataParser?.FuelPerLap,
                FuelRemainingLaps = rawDataParser?.FuelRemainingLaps,
                IsFuelAlertActive = data.NewData.CarSettings_FuelAlertActive == 1,

                FlagState = new FlagState
                {
                    Black = data.NewData.Flag_Black == 1,
                    Blue = data.NewData.Flag_Blue == 1,
                    Checkered = data.NewData.Flag_Checkered == 1,
                    Green = data.NewData.Flag_Green == 1,
                    Name = data.NewData.Flag_Name,
                    Orange = data.NewData.Flag_Orange == 1,
                    White = data.NewData.Flag_White == 1,
                    Yellow = data.NewData.Flag_Yellow == 1,
                },

                Position = data.NewData.Position,
                TotalPositions = data.NewData.OpponentsCount,

                DrsState = data.ToDrsState(),
                ErsState = rawDataParser?.ErsState
            };
        }
    }

    public static class Defaults
    {
        public static WheelInfos WheelInfos(this GameReaderCommon.GameData gameData)
        {
            return new WheelInfos
            {
                TemperatureUnit = gameData.NewData.TemperatureUnit,
                PressureUnit = gameData.NewData.TyrePressureUnit,
                FrontLeft = new WheelInfo
                {
                    Health = gameData.NewData.TyreWearFrontLeft.ToFloat(),
                    Pressure = gameData.NewData.TyrePressureFrontLeft.ToFloat(),
                    Temperature = gameData.NewData.TyreTemperatureFrontLeft.ToFloat()
                },
                FrontRight = new WheelInfo
                {
                    Health = gameData.NewData.TyreWearFrontRight.ToFloat(),
                    Pressure = gameData.NewData.TyrePressureFrontRight.ToFloat(),
                    Temperature = gameData.NewData.TyreTemperatureFrontRight.ToFloat()
                },
                RearLeft = new WheelInfo
                {
                    Health = gameData.NewData.TyreWearRearLeft.ToFloat(),
                    Pressure = gameData.NewData.TyrePressureRearLeft.ToFloat(),
                    Temperature = gameData.NewData.TyreTemperatureRearLeft.ToFloat()
                },
                RearRight = new WheelInfo
                {
                    Health = gameData.NewData.TyreWearRearRight.ToFloat(),
                    Pressure = gameData.NewData.TyrePressureRearRight.ToFloat(),
                    Temperature = gameData.NewData.TyreTemperatureRearRight.ToFloat()
                }
            };
        }
    }

    public interface IRawDataParser
    {
        long? FastestLapTime { get; }
        float DeltaInSeconds { get; }
        Assist TC2 { get; }

        int? EngineMap { get; }
        bool? IsLightOn { get; }
        string TyreCompound { get; }
        float? FuelPerLap  { get; }
        float? FuelRemainingLaps  { get; }

        bool HasTC1 { get; }
        bool HasABS { get; }
        ErsState ErsState { get; }
        WheelInfos WheelInfos { get; }
    }

    public class AcRawDataParser : IRawDataParser
    {
        private readonly ACRawData acRawData;
        private readonly GameReaderCommon.GameData gameData;

        public AcRawDataParser(ACRawData acRawData, GameReaderCommon.GameData gameData)
        {
            this.acRawData = acRawData;
            this.gameData = gameData;
        }
        
        public long? FastestLapTime => null;
        public float DeltaInSeconds => acRawData.Physics.PerformanceMeter;
        public Assist TC2 => null;
        public int? EngineMap => null;
        public bool? IsLightOn => null;
        public string TyreCompound => acRawData.Graphics.TyreCompound;
        public float? FuelPerLap => null;
        public float? FuelRemainingLaps => null;

        public bool HasTC1 => acRawData.Extensions.TCPresetCount.Let(it => it > 1);
        public bool HasABS => acRawData.Extensions.ABSPresetCount.Let(it => it > 1);

        public ErsState ErsState => acRawData.Let(data =>
        {
            if (data.StaticInfo.HasERS == 0) return null;

            return new ErsState
            {
                RemainingLapAllowance = (1 - (gameData.NewData.ERSPercent / 100)).ToFloat(),
                BatteryCharge = data.Physics.KersCharge,
                MguHMode = (MguHMode) data.Physics.ErsHeatCharging,
                MguKMode = (MguKMode) data.Physics.ErsPowerLevel,
                RecoveryLevel = data.Physics.ErsRecoveryLevel,
                EngineBrake = data.Physics.EngineBrake + 1
            };
        });

        public WheelInfos WheelInfos => null;
    }
    
    public class AccRawDataParser : IRawDataParser
    {
        private readonly ACCRawData accRawData;

        public AccRawDataParser(ACCRawData accRawData)
        {
            this.accRawData = accRawData;
        }

        public long? FastestLapTime => accRawData.Realtime.BestSessionLap.LaptimeMS;

        public float DeltaInSeconds => accRawData.Graphics.iDeltaLapTime / 1000f;
        public Assist TC2 => accRawData.Let(accData =>
        {
            if (accData.StaticInfo.CarModel.ToLower().Contains("mclaren"))
            {
                return null;
            }

            return new Assist
            {
                Level = accData.Graphics.TCCut,
                IsActive = false
            };
        });

        public int? EngineMap => accRawData.Graphics.EngineMap;
        public bool? IsLightOn => accRawData.Let(data => data.Graphics.LightsStage > 0);
        public string TyreCompound => accRawData.Graphics.TyreCompound.Let(it => it == "dry_compound" ? "DRY" : "WET");
        public float? FuelPerLap => accRawData.Graphics.FuelXLap;
        public float? FuelRemainingLaps => accRawData.Graphics.fuelEstimatedLaps;
        public bool HasTC1 => true;
        public bool HasABS => true;
        public ErsState ErsState => null;
        public WheelInfos WheelInfos => null;
    }

    public class Ams2RawDataParser : IRawDataParser
    {
        private readonly AMS2APIStruct rawData;

        public Ams2RawDataParser(AMS2APIStruct rawData)
        {
            this.rawData = rawData;
        }

        public long? FastestLapTime => rawData.mSessionFastestLapTime.TakeUnless(it => it < 0)?.Let(it => (long) it);
        public float DeltaInSeconds => rawData.mSplitTime;
        public Assist TC2 { get; }
        public int? EngineMap => null;
        public bool? IsLightOn => null;
        public string TyreCompound => rawData.mTyreCompound.First().value ?? string.Empty;
        public float? FuelPerLap => null;
        public float? FuelRemainingLaps => null;
        public bool HasTC1 => false;
        public bool HasABS => true;
        public ErsState ErsState => null;
        public WheelInfos WheelInfos => null;
    }
}
