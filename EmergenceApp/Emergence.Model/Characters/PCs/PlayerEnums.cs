using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model
{
    public enum TalentTree
    {
        Archery,
        Assassination,
        Automatics,
        Brawling,
        Brutality,
        Bulwark,
        Grappling,
        Gunfighting,
        Hunting,
        Hurling,
        MartialArts,
        Skirmishing,
        Sniping,
        Creation,
        Earthshaping,
        Elementalism,
        Illusion,
        Incantation,
        Infernalism,
        Kinesis,
        Nature,
        Necromancy,
        Prayer,
        Seeing,
        Smiting,
        Telepathy,
        Teleportation,
        ArmoredFighting,
        Leadership,
        Metamagic,
        Quickness,
        Science,
        Toughness
    }

    public enum ActionType
    {
        Free,
        Quick,
        Combat,
        Movement,
        Reaction,
        None
    }

    public enum TalentType
    {
        AttackAugment,
        Benefit,
        Enhancement,
        Maneuver,
        Ritual,
        Stance,
        Trick,
        TriggeredAction
    }
}
