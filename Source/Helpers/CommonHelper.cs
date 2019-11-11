using System;
using System.Collections.Generic;
using System.Linq;
namespace GeodataLoader.Source.Helpers
{
    //====================================================
    //=== Klasa odpowiedzialna za tworzenie wiadomoœci ===
    //----------------------------------------------------
    //========== Class responsible for messages ==========
    //====================================================

    // wykonane przez: / made by:
    // jaggi
    public static class CommonHelpers
    {
        // stwórz prost¹ wiadomoœæ / create simple message
        public static void Log(string message)
        {
            // ró¿ne opcje wysy³ania wiadomoœci / different sending options
            UnityEngine.Debug.Log(message); 
            //Console.WriteLine(message);
            //DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, message);
        }
    }

    //============================================
    //=== Klasa odpowiedzialna za rozdzielanie ===
    //--------------------------------------------
    //====== Class responsible for spliting ======
    //============================================

    // wykonane przez: / made by:
    // jaggi
    public static class EnumerableExtension
    {
        // rozdzielanie listy na mniejsze listy / spliting list to smaller lists
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