using ScaphandreEngine.Entities;
using UnityEngine;

namespace ScaphandreEngine.Events
{
    public class EntitySpawnEvent : Event
    {
        public EntitySpawnEvent(GameObject entity)
        {
            Entity = entity;
        }

        public GameObject Entity { get; }
    }
}
