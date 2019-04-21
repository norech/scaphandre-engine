using JetBrains.Annotations;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ScaphandreEngine.Entities
{
    public class CreatureWrapper<T> : CreatureWrapper where T : Creature
    {
        /// <summary>
        /// An implicit cast operator from T to CreatureWrapper<T>
        /// </summary>
        /// <param name="creature">Source creature</param>
        public static implicit operator CreatureWrapper<T> ([NotNull] T creature)
        {
            if (creature == null) return null;

            return new CreatureWrapper<T>(creature);
        }

        /// <summary>
        /// An implicit cast operator from CreatureWrapper<T> to T
        /// </summary>
        /// <param name="wrapper">Source wrapper</param>
        public static implicit operator T (CreatureWrapper<T> wrapper)
        {
            if (wrapper == null) return null;

            return (T)wrapper.creature;
        }

        public CreatureWrapper(T creature) : base(creature)
        {
            //
        }
    }

    public class CreatureWrapper : Entity
    {
        /// <summary>
        /// An implicit cast operator from Creature to CreatureWrapper
        /// </summary>
        /// <param name="creature">Source creature</param>
        public static implicit operator CreatureWrapper (Creature creature)
        {
            if (creature == null) return null;

            return new CreatureWrapper(creature);
        }

        /// <summary>
        /// An implicit cast operator from Creature to CreatureWrapper
        /// </summary>
        /// <param name="wrapper">Source wrapper</param>
        public static implicit operator Creature (CreatureWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.creature;
        }

        [AssertNotNull]
        protected LiveMixin liveMixin;
        
        [AssertNotNull]
        protected Creature creature;

        public CreatureWrapper(Creature creature)
            : base(creature.gameObject ??
                  throw new System.Exception("Invalid creature provided in CreatureWrapper: missing GameObject"))
        {
            this.creature = creature;
            Traits = new CreatureTraits(creature);
            liveMixin = creature.GetComponent<LiveMixin>();

            if(liveMixin == null)
            {
                throw new System.Exception("Invalid creature provided in CreatureWrapper: missing LiveMixin");
            }

            actionField = creature.GetType().GetField("actions");
        }

        private FieldInfo actionField;

        /// <summary>
        /// Actions are parts of the creature AI
        /// </summary>
        public List<CreatureAction> Actions => (List<CreatureAction>)actionField.GetValue(creature);

        public CreatureTraits Traits { get; }

        /// <summary>
        /// Is the creature alive?
        /// </summary>
        public bool IsAlive => liveMixin.IsAlive();

        /// <summary>
        /// Kills the creature instantly
        /// </summary>
        /// <param name="damageType">The type of damage that killed the creature</param>
        public void Kill(DamageType damageType = DamageType.Normal)
        {
            liveMixin.Kill(damageType);
        }

        /// <summary>
        /// Try to apply damage for entity
        /// </summary>
        /// <param name="originalDamage">The damage to give to the creature, before recalculations and applying factors</param>
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
        /// Do the creature have eyes?
        /// </summary>
        public bool HasEyes
        {
            get => creature.hasEyes;
            set => creature.hasEyes = value;
        }

        /// <summary>
        /// Is the creature detectable by the Seamoth sonar?
        /// </summary>
        public bool DetectableBySeamothSonar
        {
            get => creature.cyclopsSonarDetectable;
            set => creature.cyclopsSonarDetectable = value;
        }

        /// <summary>
        /// How much does the creature can hear sounds?
        /// </summary>
        public float HearingSensivity
        {
            get => creature.hearingSensitivity;
            set => creature.hearingSensitivity = value;
        }
    }
}
