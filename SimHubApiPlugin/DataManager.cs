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
                accRawData: (data as GameData<ACCRawData>)?.GameNewData.Raw
            );

            CurrentData = new GameData
            {
                Speed = data.NewData.SpeedKmh,
                DeltaInSeconds = rawDataParser.DeltaInSeconds,
                Gear = data.NewData.Gear,
                TC1 = data.NewData.TCLevel,
                ABS = data.NewData.ABSLevel,
                FuelRemaining = data.NewData.Fuel,
                ErsAvailable = data.NewData.ERSMax > 0,
                ErsAllowance = data.NewData.ERSPercent,
                ErsBattery = rawDataParser.ErsBattery,
                BrakeBias = data.NewData.BrakeBias
            };
        }
    }

    public class GameData
    {
        public double Speed { get; set; }
        public float DeltaInSeconds { get; set; }
        public string Gear { get; set; }
        public int TC1 { get; set; }
        public int TC2 { get; set; }
        public int ABS { get; set; }
        public double FuelRemaining { get; set; }
        public bool ErsAvailable { get; set; }
        public float ErsBattery { get; set; }
        public double ErsAllowance { get; set; }
        public double BrakeBias { get; set; }
    }

    public class RawDataParser
    {
        private readonly ACRawData acRawData;
        private readonly ACCRawData accRawData;

        public RawDataParser(ACRawData acRawData, ACCRawData accRawData)
        {
            this.acRawData = acRawData;
            this.accRawData = accRawData;
        }

        public float DeltaInSeconds => acRawData?.Physics.PerformanceMeter ?? accRawData?.Physics.PerformanceMeter ?? 0f;
        public float ErsBattery => acRawData?.Physics.KersCharge ?? accRawData?.Physics.KersCharge ?? 0f;
    }
}
