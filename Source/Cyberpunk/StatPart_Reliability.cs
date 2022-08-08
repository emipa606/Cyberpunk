using System;
using RimWorld;

namespace Cyberpunk;

internal class StatPart_Reliability : StatPart
{
    public override void TransformValue(StatRequest req, ref float val)
    {
        if (!req.HasThing || req.Thing.GetType() != Type.GetType("Cyberpunk.ThingDef_GunCP"))
        {
            return;
        }

        var gun = (ThingDef_GunCP)req.Thing;
        GetReliability(gun, out var rel, out var jamsOn);
        parentStat.formatString = rel;
        switch (rel)
        {
            default:
                return;
            case "Unreliable":
                parentStat.description = "This gun is unreliable in combat and can jam easily.";
                break;
            case "Standard":
                parentStat.description = "This gun is reliable in combat but can occationally jam.";
                break;
            case "Very Reliable":
                parentStat.description = "This gun is very reliable in combat and tends not to jam.";
                break;
            case "Extremely Reliable":
                parentStat.description = "This gun is extremely reliable in combat and tends not to jam.";
                break;
        }

        val *= jamsOn;
    }

    public override string ExplanationPart(StatRequest req)
    {
        return string.Empty;
    }

    public static void GetReliability(ThingDef_GunCP gun, out string rel, out float jamsOn)
    {
        rel = string.Empty;
        jamsOn = JamChance(gun);
        if (jamsOn < 0.25)
        {
            rel = "Extremely Reliable";
        }
        else if (jamsOn < 0.5)
        {
            rel = "Very Reliable";
        }
        else if (jamsOn < 1f)
        {
            rel = "Standard";
        }
        else
        {
            rel = "Unreliable";
        }
    }

    public static float JamChance(ThingDef_GunCP gun)
    {
        float num;
        switch (gun.reliability)
        {
            case Reliability.UR:
                num = 80f;
                break;
            case Reliability.ST:
                num = 55f;
                break;
            case Reliability.VR:
                num = 30f;
                break;
            default:
                return 0f;
        }

        num += GetQualityFactor(gun);
        num = num * 100f / gun.HitPoints / 100f;
        return (float)(Math.Truncate(num * 100.0) / 100.0);
    }

    public static int GetQualityFactor(ThingDef_GunCP gun)
    {
        if (!gun.TryGetQuality(out var qc))
        {
            return 0;
        }

        switch (qc)
        {
            case QualityCategory.Awful:
                return 10;
            case QualityCategory.Poor:
                return 5;
            case QualityCategory.Normal:
                return 0;
            case QualityCategory.Good:
                return -5;
            case QualityCategory.Excellent:
                return -10;
            case QualityCategory.Masterwork:
                return -15;
            case QualityCategory.Legendary:
                return -20;
        }

        return 0;
    }
}