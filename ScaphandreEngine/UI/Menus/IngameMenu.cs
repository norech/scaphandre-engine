using BaseIngameMenu = IngameMenu;

namespace ScaphandreEngine.UI.Menus
{
    public class IngameMenu : Menu
    {
        public static IngameMenu Main => new IngameMenu(BaseIngameMenu.main);

        private IngameMenu(BaseIngameMenu @base)
        {
            _original = @base;
        }

        private BaseIngameMenu _original;

        public void Open()
        {
            _original.Open();
        }

        public void Close()
        {
            _original.Close();
        }
    }
}
