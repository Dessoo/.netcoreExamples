using BusinessLayer.Enums;

namespace BusinessLayer.DTO
{
    public class LoggerSettingsDTO
    {
        public LoggerSettingsDTO(bool turnOn, string logLevel, int storageType)
        {
            this.TurnOn = turnOn;
            this.LogLevel = logLevel;
            this.StorageType = (LoggerStorageTypes)storageType;
        }

        public bool TurnOn { get; set; }
        public string LogLevel { get; set; }
        public LoggerStorageTypes StorageType { get; set; }
    }
}
