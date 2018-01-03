using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model
{
    public class Background
    {
        public string Name
        { get; set; }
        public string Description
        { get; set; }
        public AttributeArray StartingArray
        { get; set; }
        public string Skills
        { get; set; }
        public string TalentTraining
        { get; set; }
        public string Notoriety
        { get; set; }
        public List<Contact> Contacts
        { get; set; }
        public int StartingCash
        { get; set; }
        public Lifestyle StartingLifestyle
        { get; set; }
        public List<BackgroundSpecial> SpecialProperties
        { get; set; }
    }
}
