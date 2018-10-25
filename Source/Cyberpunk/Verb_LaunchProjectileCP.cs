using RimWorld;
using Verse;


namespace Cyberpunk
{
    public class Verb_LaunchProjectileCP : Verb_LaunchProjectile
    {
        protected virtual float reliable
        {
            get
            {
                return base.EquipmentSource.GetStatValue(StatDefOf_CP.reliability);
            }
        }

        public VerbPropertiesCP verbPropsCP
        {
            get
            {
                return verbProps as VerbPropertiesCP;
            }
        }

        protected override bool TryCastShot()
        {
            string reliabilityString;
            float jamsOn;
            ThingDef_GunCP ownerEquipment = base.EquipmentSource as ThingDef_GunCP;
            StatPart_Reliability.GetReliability(ownerEquipment, out reliabilityString, out jamsOn);
            float jamRoll = (Rand.Range(0, 1000))/10f;
            //float jamRoll = Rand.Range(0, 100);
            if (jamRoll < jamsOn)
            {
                string msg = string.Format("{0}'s {1} had a weapon jam. ({2}/{3})", caster.LabelCap, ownerEquipment.LabelCap, jamRoll, jamsOn);
                Messages.Message(msg, MessageTypeDefOf.SilentInput);
                ownerEquipment.HitPoints--;
                return false;
            }
            return base.TryCastShot();
        }
    }
}
