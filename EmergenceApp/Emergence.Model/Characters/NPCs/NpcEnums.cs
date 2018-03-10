using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model
{
    public enum NpcClass
    {
        NotSet =0,
        Grunt,
        Foe,
        Antagonist,
        Companion
    }

    public enum Archetype
    {
        NotSet = 0,
        Beast,
        Risen,
        Demonic,
        Flying_aka_Dragonkin,
        Elemental,
        Humanoid
    }

    public enum NpcType
    {
        NotSet = 0,
        Flesh_aka_Unliving,
        Plant,
        Fluid,
        Swarm,
        Machine,
        Energy,
        Solid,
        Natural
    }
}
