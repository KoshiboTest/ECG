namespace Emergence.Model
{
    public class AttributeModifier
    {
        public string AttributeName { get; set; }
        public ModifierType Type { get; set; }
        public decimal ModifierValue { get; set; }
    }

    public enum ModifierType
    {
        Additive,
        Multiplicative
    }
}