using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ScaphandreEngine.Entities
{
    public class Entity
    {
        public GameObject GameObject { get; }

        public Entity(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}
