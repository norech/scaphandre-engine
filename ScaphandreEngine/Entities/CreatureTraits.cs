using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }
        
        /// <summary>
        /// How much the creature feels aggressed
        /// </summary>
        public CreatureTrait Aggression => creature.Aggression;

        /// <summary>
        /// How much the creature is curious
        /// </summary>
        public CreatureTrait Curiosity => creature.Curiosity;

        /// <summary>
        /// How much the creature is friendly
        /// </summary>
        public CreatureTrait Friendliness => creature.Friendliness;

        /// <summary>
        /// How much the creature is happy
        /// </summary>
        public CreatureTrait Happy => creature.Happy;

        /// <summary>
        /// How much the creature is hungry
        /// </summary>
        public CreatureTrait Hunger => creature.Hunger;

        /// <summary>
        /// How much the creature is scared
        /// </summary>
        public CreatureTrait Scared => creature.Scared;

        /// <summary>
        /// How much the creature is tired
        /// </summary>
        public CreatureTrait Tired => creature.Tired;

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
