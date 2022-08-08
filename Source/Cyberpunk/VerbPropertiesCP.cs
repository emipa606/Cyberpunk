using Verse;

namespace Cyberpunk;

public class VerbPropertiesCP : VerbProperties
{
    public Reliability reliability = Reliability.ST;

    public override string ToString()
    {
        var text = label.NullOrEmpty()
            ? $"range={range}, projectile={(defaultProjectile == null ? "null" : defaultProjectile.defName)}, reliability={reliability}"
            : label;
        return $"VerbProperties({text})";
    }
}