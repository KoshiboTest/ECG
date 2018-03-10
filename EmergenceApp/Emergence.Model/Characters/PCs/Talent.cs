using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model
{
    public class Talent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DescriptionFluff { get; set; }
        public string ClarifyingText { get; set; }
        public TalentTree Tree { get; set; }
        public string TreeName { get; set; }
        public int Tier { get; set; }
        public int? StaminaCost { get; set; }
        public int? UpkeepCost { get; set; }
        public int? FatigueCost { get; set; }
        public bool IsSpell { get; set; }
        public WeaponSkill? LinkedSkill { get; set; }
        public string LinkedAttribute { get; set; }
        public ActionType? Action { get; set; }
        public TalentType? Type { get; set; }
        public string TierBenefitDescription { get; set; }

        public Talent()
        {
        }
    }
}
