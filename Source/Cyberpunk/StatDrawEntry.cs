using RimWorld;
using Verse;

namespace Cyberpunk;

public class StatDrawEntry
{
    private readonly ToStringNumberSense numberSense;

    private readonly StatRequest optionalReq;

    private readonly StatDef stat;

    private readonly float value;

    public StatDrawEntry(StatCategoryDef category, StatDef stat, float value, StatRequest optionalReq,
        ToStringNumberSense numberSense = ToStringNumberSense.Undefined)
    {
        this.stat = stat;
        LabelCap = null;
        this.value = value;
        ValueString = null;
        this.optionalReq = optionalReq;
        this.numberSense = numberSense == ToStringNumberSense.Undefined ? stat.toStringNumberSense : numberSense;
    }

    public StatDrawEntry(StatCategoryDef category, string label, string valueString,
        int displayPriorityWithinCategory = 0)
    {
        stat = null;
        LabelCap = label;
        value = 0f;
        ValueString = valueString;
        numberSense = ToStringNumberSense.Absolute;
    }

    private string LabelCap
    {
        get
        {
            if (field != null)
            {
                return field.CapitalizeFirst();
            }

            return stat.LabelCap;
        }
    }

    private string ValueString
    {
        get
        {
            if (numberSense == ToStringNumberSense.Factor)
            {
                return value.ToStringByStyle(ToStringStyle.PercentZero);
            }

            return stat != null
                ? stat.Worker.GetStatDrawEntryLabel(stat, value, numberSense, optionalReq)
                : field;
        }
    }

    public override string ToString()
    {
        return $"({LabelCap}: {ValueString})";
    }
}