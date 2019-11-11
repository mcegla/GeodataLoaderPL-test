using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColossalFramework.UI;
using ColossalFramework.Plugins;

//===========================================================================================================
//=== This part of code is helping in tedious tasks for example: in-game console messages, spliting, etc. ===
//===========================================================================================================

namespace GeodataLoader.Source.Helpers
{
    public static class CommonHelpers
    {
        public static void Log(string message)
        {
            // Create simple in-game console message
            UnityEngine.Debug.Log(message);
            //DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, message);
        }
    }

    public static class EnumerableExtension
    {
        // Spliting to list
        public static IEnumerable<T[]> Split<T>(this IEnumerable<T> enumerable, int size)
        {
            var count = 0;
            var length = enumerable.Count();
            var res = new List<T[]>((int)Math.Ceiling((double)length / size));
            while (count * size < length)
            {
                res.Add(enumerable.Skip(count * size).Take(size).ToArray());
                count++;
            }
            return res;
        }
    }
}