using Emergence.Model.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.ViewModel.Equipment
{
    public class ArmorVM
    {
        public Armor model;
        public ArmorVM()
        {
            model = new Armor();
        }
    }

    public class NaturalArmorVM : ArmorVM
    {
        public NaturalArmorVM(Model.Equipment.NaturalArmorClass c)
        {
            model = new NaturalArmor(c);
        }
    }
}
