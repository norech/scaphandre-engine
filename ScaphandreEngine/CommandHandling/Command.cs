using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ScaphandreEngine.CommandHandling
{
    public interface Command
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Setup(Mod mod);

        string GetName();
        void Execute(string[] args);
    }
}
