using Verse;

namespace Cyberpunk
{
    public class VerbPropertiesCP : VerbProperties
    {
        public Reliability reliability = Reliability.ST;

        public override string ToString()
        {
            string str;
            if (!this.label.NullOrEmpty())
            {
                str = this.label;
            }
            else
            {
                str = string.Concat(new object[]
                {
                    "range=",
                    this.range,
                    ", projectile=",
                    (this.projectileDef == null) ? "null" : this.projectileDef.defName,
                    ", reliability=",
                    this.reliability.ToString()
                });
            }
            return "VerbProperties(" + str + ")";
        }
    }

    public enum Reliability : short
    {
        UR = 80,
        ST = 55,
        VR = 30,
        NA = 0
    }

}
