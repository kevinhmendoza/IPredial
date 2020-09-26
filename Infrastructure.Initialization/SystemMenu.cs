using Infrastructure.Initialization.Modules;
using System.Collections.Generic;

namespace Infrastructure.Initialization
{
    class SystemMenu
    {
        public List<Menu> Menus { get; set; }
        public SystemMenu()
        {
            Menus = new List<Menu>();
            InicializarModulos();
            int _contadorModulos = 1;
            foreach (Menu _menu in Menus)
            {
                _menu.MenuId = _contadorModulos.ToString().PadLeft(2, '0');
                int contadorMenu = 0;
                _menu.SubMenu.ForEach(t =>
                {
                    t.Url = $"/{_menu.Modulo}/{t.Modulo}";
                    t.MenuId = $"{_menu.MenuId}{contadorMenu}";
                    contadorMenu++;
                });
                _contadorModulos++;
            }
        }
        private void InicializarModulos()
        {
            Seguridad();
        }
        private void Seguridad()
        {
            Menus.Add(new ModuloSeguridad().Menu());
        }
    }
    public class Menu
    {
        public Menu()
        {
            Prioridad = 100;
        }
        public string MenuId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Prioridad { get; set; }
        public string Icono { get; set; }
        public bool Habilitado { get; set; }
        public string Url { get; set; }
        public string Modulo { get; set; }
        public string Rol { get; set; }
        public List<Menu> SubMenu { get; set; }
    }
}
