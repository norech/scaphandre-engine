using System.Collections;
using System.ComponentModel;
using UnityEngine;

namespace ScaphandreEngine.CommandHandling
{
    public abstract class CommandBase : Command
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Setup(Mod mod)
        {
            // Kept for a later usage
        }

        public abstract string GetName();
        public abstract void Execute(string[] args);
    }
}
