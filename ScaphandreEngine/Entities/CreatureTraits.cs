using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScaphandreEngine.Assertions;

namespace ScaphandreEngine.Entities
{
    /// <summary>
    /// Container of traits required by a creature
    /// </summary>
    public class CreatureTraits
    {
        private Creature creature;
        public CreatureTraits(Creature creature)
        {
            this.creature = creature;

            Assert.IsNotNull(creature, "Creature should not be null!");
        }
        
        /// <summary>
        /// How much the creature feels aggressed
        /// </summary>
        public CreatureTrait Aggression
        {
            get
            {
                Assert.IsNotNull(creature.Aggression, "Creature Aggression Trait should not be null!");

                return creature.Aggression;
            }
        }

        /// <summary>
        /// How much the creature is curious
        /// </summary>
        public CreatureTrait Curiosity
        {
            get
            {
                Assert.IsNotNull(creature.Curiosity, "Creature Curiosity Trait should not be null!");

                return creature.Curiosity;
            }
        }

        /// <summary>
        /// How much the creature is friendly
        /// </summary>
        public CreatureTrait Friendliness
        {
            get
            {
                Assert.IsNotNull(creature.Friendliness, "Creature Friendliness Trait should not be null!");

                return creature.Friendliness;
            }
        }

        /// <summary>
        /// How much the creature is happy
        /// </summary>
        public CreatureTrait Happy
        {
            get
            {
                Assert.IsNotNull(creature.Happy, "Creature Happy Trait should not be null!");

                return creature.Happy;
            }
        }

        /// <summary>
        /// How much the creature is hungry
        /// </summary>
        public CreatureTrait Hunger
        {
            get
            {
                Assert.IsNotNull(creature.Hunger, "Creature Hunger Trait should not be null!");

                return creature.Hunger;
            }
        }

        /// <summary>
        /// How much the creature is scared
        /// </summary>
        public CreatureTrait Scared
        {
            get
            {
                Assert.IsNotNull(creature.Scared, "Creature Scared Trait should not be null!");

                return creature.Scared;
            }
        }

        /// <summary>
        /// How much the creature is tired
        /// </summary>
        public CreatureTrait Tired
        {
            get
            {
                Assert.IsNotNull(creature.Tired, "Creature Tired Trait should not be null!");

                return creature.Tired;
            }
        }

        public override string ToString()
        {
            return "Aggression: " + Aggression.Value
                + "\nCuriosity: " + Curiosity.Value
                + "\nFriendliness: " + Friendliness.Value
                + "\nHappy: " + Happy.Value
                + "\nHunger: " + Hunger.Value
                + "\nScared: " + Scared.Value
                + "\nTired: " + Tired.Value;
        }
    }
}
