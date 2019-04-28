using ScaphandreEngine.Assertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScaphandreEngine.Entities
{
    public static class PlayerExtensions
    {
        public static PlayerWrapper GetWrapper(this Player player) => player;
    }

    public class PlayerWrapper : LivingWrapper
    {
        /// <summary>
        /// An implicit cast operator from Player to PlayerWrapper
        /// </summary>
        /// <param name="creature">Source creature</param>
        public static implicit operator PlayerWrapper(Player creature)
        {
            Assert.IsNotNull(creature, "Player should not be null!");

            return new PlayerWrapper(creature);
        }

        /// <summary>
        /// An implicit cast operator from PlayerWrapper to Player
        /// </summary>
        /// <param name="wrapper">Source wrapper</param>
        public static implicit operator Player(PlayerWrapper wrapper)
        {
            Assert.IsNotNull(wrapper, "Wrapper should not be null!");

            return wrapper.player;
        }

        private Player player;

        public PlayerWrapper(Player player) : base(player)
        {
            Assert.IsNotNull(player, "Player should not be null!");

            this.player = player;
        }

        public PDA PDA => player.GetPDA();
        public PilotingChair PilotingChair => player.GetPilotingChair();

        public bool IsInCinematicMode => player.cinematicModeActive;
        public bool IsInMechMode => player.GetInMechMode();

        public bool DisplaySurfaceWater => player.displaySurfaceWater;

        public float InfectionAmount => player.GetInfectionAmount();
        public float BreathPeriod => player.GetBreathPeriod();
        public Ocean.DepthClass DepthClass => player.GetDepthClass();
        public float Depth => player.GetDepth();

        public float OxygenAvailable => player.GetOxygenAvailable();
        public float OxygenCapacity => player.GetOxygenCapacity();

        private readonly MethodInfo Player_GetOxygenPerBreath = typeof(Player).GetMethod("GetOxygenPerBreath");
        public float OxygenPerBreath => (float)Player_GetOxygenPerBreath.FastInvoke(player, BreathPeriod, DepthClass);

        public bool IsReloadButtonHeld => player.GetReloadHeld();
        public bool IsReloadButtonUp => player.GetReloadUp();

        public bool IsRightHandButtonDown => player.GetRightHandDown();
        public bool IsRightHandButtonUp => player.GetRightHandUp();
        public bool IsRightHandButtonHeld => player.GetRightHandHeld();

        public bool IsQuickSlotKeyUp(int slotId) => player.GetQuickSlotKeyUp(slotId);
        public bool IsQuickSlotKeyDown(int slotId) => player.GetQuickSlotKeyDown(slotId);
        public bool IsQuickSlotKeyHeld(int slotId) => player.GetQuickSlotKeyHeld(slotId);
    }
}
