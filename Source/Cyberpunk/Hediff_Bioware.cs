using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace Cyberpunk
{
    public class Hediff_Bioware : HediffWithComps
    {
        public override bool ShouldRemove
        {
            get
            {
                return false;
            }
        }

        public override void PostAdd(DamageInfo? dinfo)
        {
        }
    }
}
