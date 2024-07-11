using UnityEngine;

namespace Naka.UI {
    public class CompositeMenu : Menu {

        [Header("Parameters")]

        [SerializeField] private Menu[] _menus;

        public override void Open() {
            foreach(Menu menu in _menus) menu.Open();
        }

        public override void Close() {
            foreach(Menu menu in _menus) menu.Close();
        }

    }
}