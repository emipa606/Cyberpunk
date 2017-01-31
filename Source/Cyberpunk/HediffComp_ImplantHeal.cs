using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace Cyberpunk
{
    public class HediffComp_ImplantHeal : HediffComp
    {
        public int ticksSinceHeal;

        public HediffCompProperties_ImplantHeal Props
        {
            get
            {
                return (HediffCompProperties_ImplantHeal)this.props;
            }
        }

        public override void CompExposeData()
        {
            Scribe_Values.LookValue<int>(ref this.ticksSinceHeal, "ticksSinceHeal", 0, false);
        }

        public override void CompPostTick()
        {
            base.CompPostTick();

            this.ticksSinceHeal++;
            if (this.ticksSinceHeal > this.Props.healIntervalTicks)
            {
                if (base.Pawn.health.hediffSet.HasNaturallyHealingInjury())
                {
                    this.ticksSinceHeal = 0;
                    float num = 8f;
                    Hediff_Injury hediff_Injury = (from x in base.Pawn.health.hediffSet.GetHediffs<Hediff_Injury>()
                                                   where x.CanHealNaturally()
                                                   select x).RandomElement<Hediff_Injury>();
                    hediff_Injury.Heal(num * base.Pawn.HealthScale * 0.01f);
                    string msg = string.Format("{0} healed.", base.Pawn.LabelCap);
                    //Messages.Message(msg, MessageSound.Silent);
                }
            }
        }
    }
}
