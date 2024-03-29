﻿using System;
using GameReaderCommon;
using SimHubApiPlugin.Formatting;
using SimHubApiPlugin.Models;
using SimHubApiPlugin.Utils;
using static SimHubApiPlugin.Models.DrsState;

namespace SimHubApiPlugin.Parsing;

public static class CommonParsing
{
    public static string Speed(this GameData data) => data.NewData.SpeedKmh.ToString("0");
    public static string BrakeBias(this GameData data) => data.NewData.BrakeBias.ToString("F1");
    public static string Gear(this GameData data) => data.NewData.Gear;
        
    public static float Rpm(this GameData data) => data.NewData.Rpms.ToFloat();
    public static float MaxRpm(this GameData data) => data.NewData.MaxRpm.ToFloat();
    public static float Redline(this GameData data) => data.NewData.CarSettings_RPMShiftLight2.ToFloat();
    public static bool ShouldShift(this GameData data) => data.NewData.Rpms >= data.NewData.CarSettings_CurrentGearRedLineRPM;
    public static float Turbo(this GameData data) => data.NewData.TurboPercent.ToFloat();
        
    public static string CurrentLapTime(this GameData data) => data.NewData.CurrentLapTime.FormatLaptime();
    public static string LastLapTime(this GameData data) => data.NewData.LastLapTime.FormatLaptime();
    public static string BestLapTime(this GameData data) => data.NewData.BestLapTime.FormatLaptime();
    public static bool IsLapValid(this GameData data) => data.NewData.IsLapValid;
        
    public static int CurrentLap(this GameData data) => data.NewData.CurrentLap;
    public static int? TotalLaps(this GameData data) => data.NewData.TotalLaps.TakeUnlessStruct(laps => laps <= 0);
    public static string SessionTimeLeft(this GameData data) => data.NewData.SessionTimeLeft.FormatSessionTime();
        
    public static bool IsInPitlane(this GameData data) => data.NewData.IsInPitLane == 1;
    public static bool IsPitLimiterOn(this GameData data) => data.NewData.PitLimiterOn == 1;

    public static Assist Tc1(this GameData data) => data.NewData.Let(newData => new Assist(
        Level: newData.TCLevel,
        IsActive: newData.TCActive == 1
    ));
    public static Assist Abs(this GameData data) => data.NewData.Let(newData => new Assist(
        Level: newData.ABSLevel,
        IsActive: newData.ABSActive == 1
    ));
        
    public static string FuelRemaining(this GameData data) => data.NewData.Fuel.ToString("F2");
    public static bool IsFuelAlertActive(this GameData data) => data.NewData.CarSettings_FuelAlertActive == 1;

    public static FlagState FlagState(this GameData data) => new FlagState(
        Black: data.NewData.Flag_Black == 1,
        Blue: data.NewData.Flag_Blue == 1,
        Checkered: data.NewData.Flag_Checkered == 1,
        Green: data.NewData.Flag_Green == 1,
        Name: data.NewData.Flag_Name,
        Orange: data.NewData.Flag_Orange == 1,
        White: data.NewData.Flag_White == 1,
        Yellow: data.NewData.Flag_Yellow == 1
    );
        
    public static int Position(this GameData data) => data.NewData.Position;
    public static int TotalPositions(this GameData data) => data.NewData.OpponentsCount;
        
    public static DrsState DrsState(this GameData data) =>
        data.NewData.IsInPitLane == 1 ? None :
        data.NewData.DRSEnabled == 1 ? Enabled :
        data.NewData.DRSAvailable == 1 ? Available : None;

    public static WheelInfos WheelInfos(this GameData gameData, Func<double, TyreHealth> toTyreHealth) => new(
        FrontLeft: new WheelInfo(
            Health: toTyreHealth(gameData.NewData.TyreWearFrontLeft),
            Pressure: gameData.NewData.TyrePressureFrontLeft.ToString("F1") + gameData.NewData.TyrePressureUnit,
            Temperature: gameData.NewData.TyreTemperatureFrontLeft.ToFloat()
        ),
        FrontRight: new WheelInfo(
            Health: toTyreHealth(gameData.NewData.TyreWearFrontRight),
            Pressure: gameData.NewData.TyrePressureFrontRight.ToString("F1") + gameData.NewData.TyrePressureUnit,
            Temperature: gameData.NewData.TyreTemperatureFrontRight.ToFloat()
        ),
        RearLeft: new WheelInfo(
            Health: toTyreHealth(gameData.NewData.TyreWearRearLeft),
            Pressure: gameData.NewData.TyrePressureRearLeft.ToString("F1") + gameData.NewData.TyrePressureUnit,
            Temperature: gameData.NewData.TyreTemperatureRearLeft.ToFloat()
        ),
        RearRight: new WheelInfo(
            Health: toTyreHealth(gameData.NewData.TyreWearRearRight),
            Pressure: gameData.NewData.TyrePressureRearRight.ToString("F1") + gameData.NewData.TyrePressureUnit,
            Temperature: gameData.NewData.TyreTemperatureRearRight.ToFloat()
        )
    );

    public static TyreHealth ToTyreHealth(this double wear) => new(
        Value: wear.ToFloat(),
        Formatted: wear.ToString("P1")
    );
}