using ACSharedMemory.ACC.Reader;
using ACSharedMemory.Reader;
using GameReaderCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var rawDataParser = new RawDataParser(
                acRawData: (data as GameData<ACRawData>)?.GameNewData?.Raw,
                accRawData: (data as GameData<ACCRawData>)?.GameNewData.Raw,
                gameData: data
            );

            CurrentData = new GameData
            {
                Speed = data.NewData.SpeedKmh.ToFloat(),
                BrakeBias = data.NewData.BrakeBias.ToFloat(),
                Gear = data.NewData.Gear,

                Rpm = data.NewData.Rpms.ToFloat(),
                MaxRpm = data.NewData.MaxRpm.ToFloat(),
                Redline = data.NewData.Redline.ToFloat(),

                CurrentLapTime = data.NewData.CurrentLapTime.TotalMilliseconds.ToLong(),
                LastLapTime = data.NewData.LastLapTime.TotalMilliseconds.ToLong(),
                BestLapTime = data.NewData.BestLapTime.TotalMilliseconds.ToLong(),
                DeltaInSeconds = rawDataParser.DeltaInSeconds,
                IsLapValid = data.NewData.IsLapValid,

                CurrentLap = data.NewData.CurrentLap,
                TotalLaps = data.NewData.TotalLaps.TakeUnless(laps => laps == 0),
                SessionTimeLeft = data.NewData.SessionTimeLeft.TotalMilliseconds.ToLong(),

                IsInPitlane = data.NewData.IsInPitLane == 1,
                IsPitLimiterOn = data.NewData.PitLimiterOn == 1,

                TC1 = data.NewData.Let(newData =>
                {
                    if (!rawDataParser.HasTC1) return null;

                    return new Assist
                    {
                        Level = newData.TCLevel,
                        IsActive = newData.TCActive == 1
                    };
                }),
                TC2 = rawDataParser.TC2,
                ABS = data.NewData.Let(newData =>
                {
                    if (!rawDataParser.HasABS) return null;

                    return new Assist
                    {
                        Level = newData.ABSLevel,
                        IsActive = newData.ABSActive == 1
                    };
                }),

                EngineMap = rawDataParser.EngineMap,
                IsLightOn = rawDataParser.IsLightOn,

                TyreCompound = rawDataParser.TyreCompound,
                WheelInfos = rawDataParser.WheelInfos,

                FuelRemaining = data.NewData.Fuel.ToFloat(),
                FuelPerLap = rawDataParser.FuelPerLap,
                FuelRemainingLaps = rawDataParser.FuelRemainingLaps,

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
                ErsState = rawDataParser.ErsState
            };
        }
    }

    public class RawDataParser
    {
        private readonly GameReaderCommon.GameData gameData;
        private readonly ACRawData acRawData;
        private readonly ACCRawData accRawData;

        public RawDataParser(ACRawData acRawData, ACCRawData accRawData, GameReaderCommon.GameData gameData)
        {
            this.acRawData = acRawData;
            this.accRawData = accRawData;
            this.gameData = gameData;
        }

        public float DeltaInSeconds => acRawData?.Physics.PerformanceMeter ?? accRawData?.Physics.PerformanceMeter ?? 0f;
        public Assist TC2 => accRawData?.Let(accData =>
        {
            return new Assist
            {
                Level = accData.Graphics.TCCut,
                IsActive = false
            };
        });

        public int? EngineMap => accRawData?.Graphics.EngineMap;
        public bool? IsLightOn => accRawData?.Let(data => data.Graphics.LightsStage > 0);
        public string TyreCompound => acRawData?.Graphics.TyreCompound ?? accRawData.Graphics.TyreCompound ?? "";
        public float? FuelPerLap => accRawData?.Graphics.FuelXLap;
        public float? FuelRemainingLaps => accRawData?.Graphics.fuelEstimatedLaps;

        public bool HasTC1 => acRawData?.Extensions.TCPresetCount.Let(it => it != null && it > 0) ?? true;
        public bool HasABS => acRawData?.Extensions.ABSPresetCount.Let(it => it != null && it > 0) ?? true;

        public ErsState ErsState => acRawData?.Let(data =>
        {
            if (data.StaticInfo.HasERS == 0) return null;

            return new ErsState
            {
                RemainingLapAllowance = gameData.NewData.ERSPercent.ToFloat(),
                BatteryCharge = data.Physics.KersCharge,
                MguHMode = data.Physics.ErsHeatCharging,
                MguKMode = data.Physics.ErsPowerLevel,
                RecoveryLevel = data.Physics.ErsRecoveryLevel,
                EngineBrake = data.Physics.EngineBrake
            };
        });

        public WheelInfos WheelInfos => new WheelInfos
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
