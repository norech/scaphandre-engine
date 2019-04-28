using ScaphandreEngine.Entities;
using System;
using System.Linq;
using UnityEngine;

namespace ScaphandreEngine
{
    public static class World
    {
        private static LargeWorld _original => LargeWorld.main;

        public static DateTime LastSave => _original.lastSaveDT;
        public static string LastSavedAgo => _original.lastSavedAgoString;

        public static Player Player => Player.main;

        public static void Save()
        {
            IngameMenu.main.SaveGame();
        }

        public static Creature[] GetCreatures()
        {
            return UnityEngine.Object.FindObjectsOfType<Creature>();
        }

        public static T[] GetCreatures<T>() where T : Creature
        {
            return UnityEngine.Object.FindObjectsOfType<T>();
        }

        /// <summary>
        /// Selects every creatures in a range, ignoring their depth
        /// </summary>
        /// <param name="minPos">Minimum position</param>
        /// <param name="maxPos">Maximum position</param>
        /// <returns>Selected creatures</returns>
        public static Creature[] GetCreaturesWithin(Vector2 minPos, Vector2 maxPos)
        {
            return GetCreatures()
                .Where(c =>
                       c.transform.position.x >= minPos.x
                    && c.transform.position.x <= maxPos.x
                    && c.transform.position.y >= minPos.y
                    && c.transform.position.y <= maxPos.y
                ).ToArray();
        }

        /// <summary>
        /// Selects every creatures in a range
        /// </summary>
        /// <param name="minPos">Minimum position</param>
        /// <param name="maxPos">Maximum position</param>
        /// <returns>Selected creatures</returns>
        public static Creature[] GetCreaturesWithin(Vector3 minPos, Vector3 maxPos)
        {
            return GetCreatures()
                .Where(c =>
                       c.transform.position.x >= minPos.x
                    && c.transform.position.x <= maxPos.x
                    && c.transform.position.y >= minPos.y
                    && c.transform.position.y <= maxPos.y
                    && c.transform.position.z >= minPos.z
                    && c.transform.position.z <= maxPos.z
                ).ToArray();
        }

        /// <summary>
        /// Selects every creatures around a position
        /// </summary>
        /// <param name="position">Position to search around</param>
        /// <param name="range">Radius</param>
        /// <returns>Selected creatures</returns>
        public static Creature[] GetCreaturesAround(Vector3 position, float range)
        {
            return GetCreatures()
                .Where(c => Vector3.Distance(c.transform.position, position) <= range)
                .ToArray();
        }
    }
}
