using System.Text;
using RimWorld;
using Verse;

namespace Cyberpunk;

public class StatWorker_Reliability : StatWorker
{
    public override string GetExplanationUnfinalized(StatRequest req, ToStringNumberSense numberSense)
    {
        return string.Empty;
    }

    public override string GetExplanationFinalizePart(StatRequest req, ToStringNumberSense numberSense, float finalVal)
    {
        var stringBuilder = new StringBuilder();
        if (stat.parts == null)
        {
            return stringBuilder.ToString();
        }

        foreach (var statPart in stat.parts)
        {
            var text = statPart.ExplanationPart(req);
            if (text.NullOrEmpty())
            {
                continue;
            }

            stringBuilder.AppendLine(text);
            stringBuilder.AppendLine();
        }

        var empty = finalVal < 0.25 ? "Extremely Reliable" :
            finalVal < 0.5 ? "Very Reliable" :
            !(finalVal < 1f) ? "Unreliable" : "Standard";
        stringBuilder.AppendLine($"Reliability: {empty}\r\n\r\nChance of jam: {finalVal}%");

        return stringBuilder.ToString();
    }
}