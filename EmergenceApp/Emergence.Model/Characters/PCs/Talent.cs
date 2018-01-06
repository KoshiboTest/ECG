using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model
{
    public class Talent
    {
        public string Name;
        public string Description;
        public string DescriptionFluff;
        public string ClarifyingText;
        public TalentTree Tree;
        public string TreeName;
        public int Tier;
        public int? StaminaCost;
        public int? UpkeepCost;
        public int? FatigueCost;
        public bool IsSpell;
        public WeaponSkill? LinkedSkill;
        public string LinkedAttribute;
        public ActionType? Action;
        public TalentType? Type;
        public string TierBenefitDescription;

        public Talent()
        {
        }
    }
}
