using System;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace Cyberpunk;

public class StatDrawEntry
{
    private readonly int displayOrderWithinCategory;

    private readonly string labelInt;

    private readonly ToStringNumberSense numberSense;

    private readonly float value;

    private readonly string valueStringInt;
    public StatCategoryDef category;

    public bool hasOptionalReq;

    public StatRequest optionalReq;

    public string overrideReportText;

    public StatDef stat;

    public StatDrawEntry(StatCategoryDef category, StatDef stat, float value, StatRequest optionalReq,
        ToStringNumberSense numberSense = ToStringNumberSense.Undefined)
    {
        this.category = category;
        this.stat = stat;
        labelInt = null;
        this.value = value;
        valueStringInt = null;
        displayOrderWithinCategory = 0;
        this.optionalReq = optionalReq;
        hasOptionalReq = true;
        this.numberSense = numberSense == ToStringNumberSense.Undefined ? stat.toStringNumberSense : numberSense;
    }

    public StatDrawEntry(StatCategoryDef category, string label, string valueString,
        int displayPriorityWithinCategory = 0)
    {
        this.category = category;
        stat = null;
        labelInt = label;
        value = 0f;
        valueStringInt = valueString;
        displayOrderWithinCategory = displayPriorityWithinCategory;
        numberSense = ToStringNumberSense.Absolute;
    }

    public bool ShouldDisplay => stat == null || !Mathf.Approximately(value, stat.hideAtValue);

    public string LabelCap
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

    public string ValueString
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

    public int DisplayPriorityWithinCategory => stat?.displayPriorityInCategory ?? displayOrderWithinCategory;

    public string GetExplanationText(StatRequest optionalReq)
    {
        if (!overrideReportText.NullOrEmpty())
        {
            return overrideReportText;
        }

        if (stat == null)
        {
            return string.Empty;
        }

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(stat.LabelCap);
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(stat.description);
        stringBuilder.AppendLine();
        if (optionalReq.Empty)
        {
            return stringBuilder.ToString();
        }

        if (stat.Worker.IsDisabledFor(optionalReq.Thing))
        {
            stringBuilder.AppendLine("StatsReport_PermanentlyDisabled".Translate());
        }
        else
        {
            stringBuilder.AppendLine(stat.Worker.GetExplanationUnfinalized(optionalReq, numberSense)
                .TrimEndNewlines());
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(stat.Worker.GetExplanationFinalizePart(optionalReq, numberSense, value));
        }

        return stringBuilder.ToString();
    }

    public float Draw(float x, float y, float width, bool selected, Action clickedCallback)
    {
        var num = width * 0.45f;
        var rect = new Rect(8f, y, width, Text.CalcHeight(ValueString, num));
        if (selected)
        {
            Widgets.DrawHighlightSelected(rect);
        }
        else if (Mouse.IsOver(rect))
        {
            Widgets.DrawHighlight(rect);
        }

        var rect2 = rect;
        rect2.width -= num;
        Widgets.Label(rect2, LabelCap);
        var rect3 = rect;
        rect3.x = rect2.xMax;
        rect3.width = num;
        Widgets.Label(rect3, ValueString);
        if (stat != null)
        {
            var localStat = stat;
            TooltipHandler.TipRegion(rect,
                new TipSignal(() => localStat.LabelCap + ": " + localStat.description, stat.GetHashCode()));
        }

        if (Widgets.ButtonInvisible(rect, false))
        {
            clickedCallback();
        }

        return rect.height;
    }

    public override string ToString()
    {
        return $"({LabelCap}: {ValueString})";
    }
}