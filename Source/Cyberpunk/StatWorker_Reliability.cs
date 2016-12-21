using RimWorld;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Verse;

namespace Cyberpunk
{
    public class StatWorker_Reliability : StatWorker
    {
        public override string GetExplanation(StatRequest req, ToStringNumberSense numberSense)
        {
            
            return string.Empty;
        }

        public new void FinalizeExplanation(StringBuilder sb, StatRequest req, ToStringNumberSense numberSense, float val)
        {
            sb.AppendLine("FinalizeExplanation");
            //if (this.stat.parts != null)
            //{
            //    for (int i = 0; i < this.stat.parts.Count; i++)
            //    {
            //        string text = this.stat.parts[i].ExplanationPart(req);
            //        if (!text.NullOrEmpty())
            //        {
            //            sb.AppendLine(text);
            //            sb.AppendLine();
            //        }
            //    }
            //}
        }
    }
}
