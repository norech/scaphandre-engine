using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ScaphandreEngine.Assertions;

namespace ScaphandreEngine.Entities
{
    public class Entity
    {
        public GameObject GameObject { get; }

        public Entity(GameObject gameObject)
        {
            Assert.IsNotNull(gameObject, "Entity has no GameObject!");
            GameObject = gameObject;
        }

        public Creature[] GetCreaturesInRange(float range)
        {
            return World.GetCreatures()
                .Where(c => Vector3.Distance(c.transform.position, GameObject.transform.position) <= range && c.gameObject != GameObject)
                .ToArray();
        }
    }
}
