namespace AdditionalTiers.Utils.Attack {

    public struct DamageChange {
        public bool set;
        public bool multiply;
        public float damage;
        public float maxDamage;
        public float cappedDamage;
        public BloonProperties immuneBloonProperties;
    }

    public enum DamageModelCreation {
        Standard = 0,
        None = 1,
        Full = 2
    }
}