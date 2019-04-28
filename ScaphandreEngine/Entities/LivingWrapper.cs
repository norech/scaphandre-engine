using ScaphandreEngine.Assertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ScaphandreEngine.Entities
{
    public static class LivingExtensions
    {
        public static LivingWrapper GetWrapper(this Living living) => living;
    }

    public class LivingWrapper : Entity
    {
        /// <summary>
        /// An implicit cast operator from Living to LivingWrapper
        /// </summary>
        /// <param name="creature">Source creature</param>
        public static implicit operator LivingWrapper(Living living)
        {
            Assert.IsNotNull(living, "Living should not be null!");

            return new LivingWrapper(living);
        }

        /// <summary>
        /// An implicit cast operator from LivingWrapper to Living
        /// </summary>
        /// <param name="wrapper">Source wrapper</param>
        public static implicit operator Living(LivingWrapper wrapper)
        {
            Assert.IsNotNull(wrapper, "Wrapper should not be null!");

            return wrapper.living;
        }

        private Living living;

        protected LiveMixin liveMixin;

        public LivingWrapper(Living living) : base(living.gameObject)
        {
            Assert.IsNotNull(living, "Living should not be null!");

            this.living = living;
        }


        /// <summary>
        /// Is the entity alive?
        /// </summary>
        public bool IsAlive => liveMixin.IsAlive();

        /// <summary>
        /// Kills the entity instantly
        /// </summary>
        /// <param name="damageType">The type of damage that killed the creature</param>
        public void Kill(DamageType damageType = DamageType.Normal)
        {
            liveMixin.Kill(damageType);
        }

        /// <summary>
        /// Try to apply damage to entity
        /// </summary>
        /// <param name="originalDamage">The damage to give to the entity, before recalculations and applying factors</param>
        /// <param name="position">Where does the damage come from?</param>
        /// <param name="type">The damage type</param>
        /// <param name="dealer">Who did the damages?</param>
        /// <returns>Was the damage applied?</returns>
        public bool TakeDamage(
            float originalDamage,
            Vector3 position = default(Vector3),
            DamageType type = DamageType.Normal,
            GameObject dealer = null)
        {
            return liveMixin.TakeDamage(originalDamage, position, type, dealer);
        }

        /// <summary>
        /// Returns
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Is<T>() where T : Living
        {
            return living is T;
        }
    }
}
