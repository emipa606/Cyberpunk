using System;
using Verse;

namespace Cyberpunk;

public class ThingDef_GunCP : ThingWithComps
{
    public Reliability reliability
    {
        get
        {
            foreach (var verb in def.Verbs)
            {
                if (verb.GetType() == Type.GetType("Cyberpunk.VerbPropertiesCP"))
                {
                    return ((VerbPropertiesCP)verb).reliability;
                }
            }

            return Reliability.NA;
        }
    }

    public override string GetInspectString()
    {
        var inspectString = base.GetInspectString();
        StatPart_Reliability.GetReliability(this, out var rel, out var jamsOn);
        return $"{inspectString}\r\nReliability: {rel}\r\nChance of jam: {jamsOn}%";
    }

    public override void ExposeData()
    {
        base.ExposeData();
        StatPart_Reliability.GetReliability(this, out var rel, out var _);
        Scribe_Values.Look(ref rel, "reliability", "NA");
    }
}