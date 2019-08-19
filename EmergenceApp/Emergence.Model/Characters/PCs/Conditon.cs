using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model.Characters.PCs
{
    [Flags]
    enum Conditon
    {
        Dazed,
        Exhausted,
        Slowed,
        Vulnerable,
        Weakened,
        Empowered,
        Enraged,
        Focused,
        Girded,
        Hastened,
        Poised
    }
}
