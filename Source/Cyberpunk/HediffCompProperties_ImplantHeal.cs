using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace Cyberpunk
{
    public class HediffCompProperties_ImplantHeal : HediffCompProperties
    {
        public int healIntervalTicks = 60;

        public HediffCompProperties_ImplantHeal()
        {
            this.compClass = typeof(HediffComp_ImplantHeal);
        }
    }
}
