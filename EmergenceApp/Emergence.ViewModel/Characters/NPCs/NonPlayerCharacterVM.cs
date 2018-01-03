using Emergence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.ViewModel.Characters.NPCs
{
    public class NonPlayerCharacterVM
    {
        public NonPlayerCharacter model;

        public NonPlayerCharacterVM(Archetype archetype, int level, NpcClass npcClass, int size, NpcType type)
        {
            Model.Archetype marchetype = (Model.Archetype)(int)archetype;
            Model.NpcClass mnpcClass = (Model.NpcClass)(int)npcClass;
            Model.NpcType mnpcType = (Model.NpcType)(int)type;
            model = new NonPlayerCharacter(marchetype, level, mnpcClass, size, mnpcType);
        }
    }
}
