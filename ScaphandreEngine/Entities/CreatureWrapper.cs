using JetBrains.Annotations;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using ScaphandreEngine.Assertions;
using System.Linq;

namespace ScaphandreEngine.Entities
{
    public static class CreatureExtensions
    {
        public static CreatureWrapper GetWrapper(this Creature creature) => creature;
    }

    public class CreatureWrapper : LivingWrapper
    {
        /// <summary>
        /// An implicit cast operator from Creature to CreatureWrapper
        /// </summary>
        /// <param name="creature">Source creature</param>
        public static implicit operator CreatureWrapper (Creature creature)
        {
            Assert.IsNotNull(creature, "Creature should not be null!");

            return new CreatureWrapper(creature);
        }

        /// <summary>
        /// An implicit cast operator from CreatureWrapper to Creature
        /// </summary>
        /// <param name="wrapper">Source wrapper</param>
        public static implicit operator Creature (CreatureWrapper wrapper)
        {
            Assert.IsNotNull(wrapper, "Wrapper should not be null!");

            return wrapper.creature;
        }
        
        [AssertNotNull]
        protected Creature creature;

        public CreatureWrapper(Creature creature) : base(creature)
        {
            this.creature = creature;
            Assert.IsNotNull(creature, "Creature should not be null!");

            Traits = new CreatureTraits(creature);

            liveMixin = creature.GetComponent<LiveMixin>();
            Assert.IsNotNull(liveMixin, "Creature should have a LiveMixin!");

            actionField = creature.GetType().GetField("actions");
            Assert.IsNotNull(liveMixin, "Creature type should have an 'actions' field!");
        }

        private FieldInfo actionField;

        /// <summary>
        /// Actions are parts of the creature AI
        /// </summary>
        public List<CreatureAction> Actions => (List<CreatureAction>)actionField.GetValue(creature);

        public CreatureTraits Traits { get; }

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
