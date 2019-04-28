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
            Mod = mod;
        }

        public Mod Mod { get; private set; }

        public abstract string GetName();
        public abstract void Execute(string[] args);
    }
}
