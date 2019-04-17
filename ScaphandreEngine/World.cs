using System;

namespace ScaphandreEngine
{
    public class World
    {
        private LargeWorld _original => LargeWorld.main;

        public DateTime LastSave
        {
            get
            {
                return _original.lastSaveDT;
            }
        }

        public void Save()
        {
            IngameMenu.main.SaveGame();
        }
    }
}
