using System;

namespace JT808.Messages
{
	/// <summary>
	/// 位置信息汇报 
	/// </summary>
	[MessageType(0x0200)]
    public class PostionMessage
    {
		/// <summary>
		/// 报警标志 
		/// </summary>
		[UIntBitHandler(typeof(PostionWarningMark))]
        public PostionWarningMark WarningMark { get; set; } = new PostionWarningMark();
		/// <summary>
		/// 状态 
		/// </summary>
		[UIntBitHandler(typeof(PostionStatus))]
        public PostionStatus Status { get; set; } = new PostionStatus();
		/// <summary>
		/// 纬度 以度为单位的纬度值乘以 10 的 6 次方，精确到百万 分之一度 
		/// </summary>
		[UIntHandler]
		public uint Latitude { get; set; }
		/// <summary>
		/// 经度 以度为单位的经度值乘以 10 的 6 次方，精确到百万 分之一度 
		/// </summary>
		[UIntHandler]
        public uint Longitude { get; set; }
		/// <summary>
		/// 海拔高度，单位为米（m） 
		/// </summary>
		[UInt16Handler]
        public ushort Height { get; set; }
		/// <summary>
		/// 速度 1/10km/h 
		/// </summary>
		[UInt16Handler]
        public ushort Speed { get; set; }
		/// <summary>
		/// 方向 0-359，正北为 0，顺时针 
		/// </summary>
		[UInt16Handler]
        public ushort Direction { get; set; }
		/// <summary>
		/// 时间 BCD[6] YY-MM-DD-hh-mm-ss
		/// </summary>
		[TimeBCD]
        public DateTime Time { get; set; }
    }

    public class PostionWarningMark : IBitCustomType
    {
        /// <summary>
        /// 紧急报瞥触动报警开关后触发
        /// </summary>
        public bool TouchAlarmSwitch { get; set; }
        /// <summary>
        /// 超速报警
        /// </summary>
        public bool SpeedLimit { get; set; }
        /// <summary>
        /// 疲劳驾驶
        /// </summary>
        public bool Fatigue { get; set; }
        /// <summary>
        /// 预警
        /// </summary>
        public bool Alert { get; set; }
        /// <summary>
        /// GNSS模块发生故障
        /// </summary>
        public bool GNSSModule { get; set; }
        /// <summary>
        /// GNSS天线未接或被剪断
        /// </summary>
        public bool GNSSCutAntenna { get; set; }
        /// <summary>
        /// GNSS天线短路
        /// </summary>
        public bool GNSSShortCircuit { get; set; }
        /// <summary>
        /// 终端主电源欠压
        /// </summary>
        public bool MainPowerVoltage { get; set; }
        /// <summary>
        /// 终端主电源掉电
        /// </summary>
        public bool MainPowerOff { get; set; }
        /// <summary>
        /// 终端LCD或显示器故障
        /// </summary>
        public bool DisplayTheFault { get; set; }
        /// <summary>
        /// TTS模块故障
        /// </summary>
        public bool TTSModuleFailure { get; set; }
        /// <summary>
        /// 摄像头故障
        /// </summary>
        public bool CameraMalfunction { get; set; }

        public bool Keep12 { get; set; }
        public bool Keep13 { get; set; }
        public bool Keep14 { get; set; }
        public bool Keep15 { get; set; }
        public bool Keep16 { get; set; }
        public bool Keep17 { get; set; }

        /// <summary>
        /// 驾驶超时
        /// </summary>
        public bool DrivingTimeoutOfDay { get; set; }
        /// <summary>
        /// 超时停车
        /// </summary>
        public bool StopTimeout { get; set; }
        /// <summary>
        /// 进出区域
        /// </summary>
        public bool InOutArea { get; set; }
        /// <summary>
        /// 进出路线
        /// </summary>
        public bool InOutLine { get; set; }
        /// <summary>
        /// 路段行驶时间
        /// </summary>
        public bool BritainsTime { get; set; }
        /// <summary>
        /// 路线偏离报警
        /// </summary>
        public bool LaneDeparture { get; set; }
        /// <summary>
        /// VSS故障
        /// </summary>
        public bool VSSFault { get; set; }
        /// <summary>
        /// 油量异常
        /// </summary>
        public bool OilFault { get; set; }
        /// <summary>
        /// 被盗
        /// </summary>
        public bool Stolen { get; set; }
        /// <summary>
        /// 非法点火
        /// </summary>
        public bool IllegalIgnition { get; set; }
        /// <summary>
        /// 车辆非法位移
        /// </summary>
        public bool IllegalDisplacement { get; set; }

        public bool Keep29 { get; set; }
        public bool Keep30 { get; set; }
        public bool Keep31 { get; set; }

        public void Load(object value)
        {
            uint data = (uint)value;
            TouchAlarmSwitch = BufferUtils.GetBitValue(data, 0);
            SpeedLimit = BufferUtils.GetBitValue(data, 1);
            Fatigue = BufferUtils.GetBitValue(data, 2);
            Alert = BufferUtils.GetBitValue(data, 3);
            GNSSModule = BufferUtils.GetBitValue(data, 4);
            GNSSCutAntenna = BufferUtils.GetBitValue(data, 5);
            GNSSShortCircuit = BufferUtils.GetBitValue(data, 6);
            MainPowerVoltage = BufferUtils.GetBitValue(data, 7);
            MainPowerOff = BufferUtils.GetBitValue(data, 8);
            DisplayTheFault = BufferUtils.GetBitValue(data, 9);
            TTSModuleFailure = BufferUtils.GetBitValue(data, 10);
            CameraMalfunction = BufferUtils.GetBitValue(data, 11);
            Keep12 = BufferUtils.GetBitValue(data, 12);
            Keep13 = BufferUtils.GetBitValue(data, 13);
            Keep14 = BufferUtils.GetBitValue(data, 14);
            Keep15 = BufferUtils.GetBitValue(data, 15);
            Keep16 = BufferUtils.GetBitValue(data, 16);
            Keep17 = BufferUtils.GetBitValue(data, 17);
            DrivingTimeoutOfDay = BufferUtils.GetBitValue(data, 18);
            StopTimeout = BufferUtils.GetBitValue(data, 19);
            InOutArea = BufferUtils.GetBitValue(data, 20);
            InOutLine = BufferUtils.GetBitValue(data, 21);
            BritainsTime = BufferUtils.GetBitValue(data, 22);
            LaneDeparture = BufferUtils.GetBitValue(data, 23);
            VSSFault = BufferUtils.GetBitValue(data, 24);
            OilFault = BufferUtils.GetBitValue(data, 25);
            Stolen = BufferUtils.GetBitValue(data, 26);
            IllegalIgnition = BufferUtils.GetBitValue(data, 27);
            IllegalDisplacement = BufferUtils.GetBitValue(data, 28);
            Keep29 = BufferUtils.GetBitValue(data, 29);
            Keep30 = BufferUtils.GetBitValue(data, 30);
            Keep31 = BufferUtils.GetBitValue(data, 31);
        }

        public object Save()
        {
            return BufferUtils.GetUIntBitValue(TouchAlarmSwitch, SpeedLimit, Fatigue, Alert, GNSSModule, GNSSCutAntenna, GNSSShortCircuit,
                MainPowerVoltage, MainPowerOff, DisplayTheFault, TTSModuleFailure, CameraMalfunction, Keep12,
                Keep13, Keep14, Keep15, Keep16, Keep17, DrivingTimeoutOfDay, StopTimeout, InOutArea, InOutLine,
                BritainsTime, LaneDeparture, VSSFault, OilFault, Stolen, IllegalIgnition, IllegalDisplacement,
                Keep29, Keep30, Keep31);
        }
    }

    public class PostionStatus : IBitCustomType
    {
        public bool ACC { get; set; }

        public bool Location { get; set; }

        public bool Latitude { get; set; }

        public bool Longitude { get; set; }

        public bool Operate { get; set; }

        public bool Encryption { get; set; }

        public bool Keep6 { get; set; }

        public bool Keep7 { get; set; }

        public bool Keep8 { get; set; }

        public bool Keep9 { get; set; }

        public bool OilRoad { get; set; }

        public bool ElectricityRoad { get; set; }

        public bool DoorLock { get; set; }

        public bool Keep13 { get; set; }

        public bool Keep14 { get; set; }

        public bool Keep15 { get; set; }

        public bool Keep16 { get; set; }

        public bool Keep17 { get; set; }

        public bool Keep18 { get; set; }

        public bool Keep19 { get; set; }

        public bool Keep20 { get; set; }

        public bool Keep21 { get; set; }

        public bool Keep22 { get; set; }

        public bool Keep23 { get; set; }

        public bool Keep24 { get; set; }

        public bool Keep25 { get; set; }

        public bool Keep26 { get; set; }

        public bool Keep27 { get; set; }

        public bool Keep28 { get; set; }

        public bool Keep29 { get; set; }

        public bool Keep30 { get; set; }

        public bool Keep31 { get; set; }

        public void Load(object value)
        {
            uint data = (uint)value;
            ACC = BufferUtils.GetBitValue(data, 0);
            Location = BufferUtils.GetBitValue(data, 1);
            Latitude = BufferUtils.GetBitValue(data, 2);
            Longitude = BufferUtils.GetBitValue(data, 3);
            Operate = BufferUtils.GetBitValue(data, 4);
            Encryption = BufferUtils.GetBitValue(data, 5);
            Keep6 = BufferUtils.GetBitValue(data, 6);
            Keep7 = BufferUtils.GetBitValue(data, 7);
            Keep8 = BufferUtils.GetBitValue(data, 8);
            Keep9 = BufferUtils.GetBitValue(data, 9);
            OilRoad = BufferUtils.GetBitValue(data, 10);
            ElectricityRoad = BufferUtils.GetBitValue(data, 11);
            DoorLock = BufferUtils.GetBitValue(data, 12);
            Keep13 = BufferUtils.GetBitValue(data, 13);
            Keep14 = BufferUtils.GetBitValue(data, 14);
            Keep15 = BufferUtils.GetBitValue(data, 15);
            Keep16 = BufferUtils.GetBitValue(data, 16);
            Keep17 = BufferUtils.GetBitValue(data, 17);
            Keep18 = BufferUtils.GetBitValue(data, 18);
            Keep19 = BufferUtils.GetBitValue(data, 19);
            Keep20 = BufferUtils.GetBitValue(data, 20);
            Keep21 = BufferUtils.GetBitValue(data, 21);
            Keep22 = BufferUtils.GetBitValue(data, 22);
            Keep23 = BufferUtils.GetBitValue(data, 23);
            Keep24 = BufferUtils.GetBitValue(data, 24);
            Keep25 = BufferUtils.GetBitValue(data, 25);
            Keep26 = BufferUtils.GetBitValue(data, 26);
            Keep27 = BufferUtils.GetBitValue(data, 27);
            Keep28 = BufferUtils.GetBitValue(data, 28);
            Keep29 = BufferUtils.GetBitValue(data, 29);
            Keep30 = BufferUtils.GetBitValue(data, 30);
            Keep31 = BufferUtils.GetBitValue(data, 31);
        }

        public object Save()
        {
            return BufferUtils.GetUIntBitValue(ACC, Location, Latitude, Longitude, Operate, Encryption, Keep6, Keep7, Keep8, Keep9,
                OilRoad, ElectricityRoad, DoorLock, Keep13, Keep14, Keep15, Keep16, Keep17, Keep18, Keep19, Keep20, Keep21, Keep22,
                Keep23, Keep24, Keep25, Keep26, Keep27, Keep28, Keep29, Keep30, Keep31);
        }
    }
}
