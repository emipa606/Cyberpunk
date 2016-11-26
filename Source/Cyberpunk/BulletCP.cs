using RimWorld;
using System;
using Verse;
using Verse.Sound;

namespace Cyberpunk
{
    public class BulletCP : Projectile
    {
        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing);
            if (hitThing != null)
            {
                int damageAmountMax = this.def.projectile.damageAmountBase;
                int damageAmount = Rand.Range(0, damageAmountMax);

                BodyPartDamageInfo value = new BodyPartDamageInfo(null, null);
                DamageInfo dinfo = new DamageInfo(this.def.projectile.damageDef, damageAmount, this.launcher, this.ExactRotation.eulerAngles.y, new BodyPartDamageInfo?(value), this.equipmentDef);
                hitThing.TakeDamage(dinfo);
            }
            else
            {
                SoundDefOf.BulletImpactGround.PlayOneShot(Position);
                MoteMaker.MakeStaticMote(this.ExactPosition, ThingDefOf.Mote_ShotHit_Dirt, 1f);
            }
        }
    }
}
