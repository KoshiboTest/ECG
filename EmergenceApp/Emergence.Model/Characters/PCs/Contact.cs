using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model
{
    public class Contact
    {
        public Contact(EquipmentType et)
        {
            Name = Character.GenerateRandomName();
            Description = string.Format("A guy who gets your {0}.", et.ToString());
            DiscountedProduct = et;
            CostMultiplier = .8M;
        }

        public string Name
        { get; set; }
        public string Description
        { get; set; }
        public EquipmentType DiscountedProduct
        { get; set; }
        public decimal CostMultiplier
        { get; set; }
    }
}
