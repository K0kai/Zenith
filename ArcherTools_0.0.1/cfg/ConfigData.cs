﻿
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace ArcherTools_0._0._1.cfg
{
    [Serializable]
    public class ConfigData : ConfigDataBase
    {
        private const string CurrentVersion = "2.0";


        //Non serializable variables
        public static string ConfigVersion { get; set; } = CurrentVersion;
        public static UserConfig? _userConfig { get; private set; }

        public static ReceivingConfig? _receivingConfig { get; private set; }


        //Serializable variables
        [XmlElement("XmlCfgVersion")]
        public string _serializableCfgVersion;
        [XmlElement("userConfiguration")]
        public UserConfig? _serializableUserConfig;
        [XmlElement("receivingConfiguration")]
        public ReceivingConfig? _serializableRcvConfig;


        public ConfigData() { }
        public ConfigData(UserConfig userConfig, ReceivingConfig receivingConfig)
        {
            ConfigVersion = CurrentVersion;
            _userConfig = userConfig;
            _receivingConfig = receivingConfig;
            _serializableCfgVersion = ConfigVersion;
            _serializableUserConfig = userConfig;
            _serializableRcvConfig = receivingConfig;

        }

        public void PrepareForSerialization()
        {
            _serializableCfgVersion = ConfigVersion;
            _serializableUserConfig = _userConfig;
            _serializableRcvConfig = _receivingConfig;
        }

        public void PostDeserialization()
        {
            ConfigVersion = _serializableCfgVersion;
            _userConfig = _serializableUserConfig;
            _receivingConfig = _serializableRcvConfig;

        }

        public static void SerializeConfigData()
        {
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "Config.xml";
            string filePath = Path.Combine(directoryPath, fileName);

            try
            {
                if (Directory.Exists(directoryPath))
                {

                }
                else
                {
                    throw new DirectoryNotFoundException($"{directoryPath} does not exist.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            var serializer = new XmlSerializer(typeof(ConfigData));
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, new ConfigData(_userConfig, _receivingConfig));
                Debug.WriteLine($"Serialized config file at path \"{filePath}\".");
#if DEBUG
                if (Directory.Exists(directoryPath))
                {
                    Process.Start("explorer.exe", filePath);
                }
#endif
            }


        }

        public static ConfigData? DeserializeConfigData()
        {
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "Config.xml";
            string filePath = Path.Combine(directoryPath, fileName);

            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Config data file does not exist.");
                }
                var serializer = new XmlSerializer(typeof(ConfigData));
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    return (ConfigData?)serializer.Deserialize(fileStream);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

        }

        public static void setReceivingConfig(ReceivingConfig receivingConfig)
        {
            _receivingConfig = receivingConfig;
        }

        public string GetConfigDetails()
        {
            return ConfigVersion;
        }
    }

}