using RimWorld;
using Verse;

namespace Cyberpunk;

public class StatDrawEntry
{
    private readonly string labelInt;

    private readonly ToStringNumberSense numberSense;

    private readonly StatRequest optionalReq;

    private readonly StatDef stat;

    private readonly float value;

    private readonly string valueStringInt;

    public StatDrawEntry(StatCategoryDef category, StatDef stat, float value, StatRequest optionalReq,
        ToStringNumberSense numberSense = ToStringNumberSense.Undefined)
    {
        this.stat = stat;
        labelInt = null;
        this.value = value;
        valueStringInt = null;
        this.optionalReq = optionalReq;
        this.numberSense = numberSense == ToStringNumberSense.Undefined ? stat.toStringNumberSense : numberSense;
    }

    public StatDrawEntry(StatCategoryDef category, string label, string valueString,
        int displayPriorityWithinCategory = 0)
    {
        stat = null;
        labelInt = label;
        value = 0f;
        valueStringInt = valueString;
        numberSense = ToStringNumberSense.Absolute;
    }

    private string LabelCap
    {
        get
        {
            if (labelInt != null)
            {
                return labelInt.CapitalizeFirst();
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
                : valueStringInt;
        }
    }

    public override string ToString()
    {
        return $"({LabelCap}: {ValueString})";
    }
}