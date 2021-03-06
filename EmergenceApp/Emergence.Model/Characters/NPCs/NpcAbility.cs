﻿namespace Emergence.Model
{
    public class NpcAbility
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Tier { get; set; }
        public NpcAttack GrantedAttack { get; set; }
        public int StaminaCost { get; set; }
        public int? UpkeepCost { get; set; }

        public NpcAbility()
        {
        }
    }
}