using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace GMLParserPL.Configuration
{
    /// <summary>
    ///     This simple serializer is based on a sample created by user boformer from simtropolis forum
    /// </summary>
    /// <see cref="https://gist.githubusercontent.com/boformer/cb6840867c6febd25c8f/raw/a56159664b974be4b3e7d6625d08bc35b7a3f9a6/Configuration.cs"/>
    /// <see cref="https://community.simtropolis.com/forums/topic/73487-modding-tutorial-2-road-tree-replacer/"/>
    public abstract class JSONSerializer
    {
        private static Config config;
        private static string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Colossal Order\\Cities_Skylines\\GMLParserPL.json";

        internal static Config LoadConfig()
        {
            JsonSerializer serializer = new JsonSerializer();
            if (config == null)
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        using (StreamReader streamReader = new StreamReader(filePath))
                        using (JsonTextReader jsonReader = new JsonTextReader(streamReader))
                        {
                            config = serializer.Deserialize<Config>(jsonReader);
                        }
                    }
                    else
                    {
                        LoadResource(serializer);
                        SaveConfig();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{ObjectTypeEnum.Error};{e}");
                }
            }
            return config ?? (config = LoadResource(serializer));///new StandardConfig()); 
        }

        private static Config LoadResource(JsonSerializer serializer)
        {
            Assembly assembly = typeof(JSONSerializer).Assembly;
            Console.WriteLine($"{ObjectTypeEnum.Error};{assembly.FullName}");
            using (Stream stream = assembly.GetManifestResourceStream("GMLParserPL.GMLParserPL.json"))
            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonTextReader jsonReader = new JsonTextReader(streamReader))
            {
                return config = serializer.Deserialize<Config>(jsonReader);
            }
        }

        internal static void SaveConfig()
        {
            if (config == null) return;

            var serializer = new JsonSerializer();
            try
            {
                using (var streamWriter = new StreamWriter(filePath))
                using (JsonTextWriter jsonWriter = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(jsonWriter, config);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{ObjectTypeEnum.Error};{e}");
            }
        }
    }
}
