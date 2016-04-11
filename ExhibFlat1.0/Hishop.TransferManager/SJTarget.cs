namespace Hishop.TransferManager
{
    using System;

    public class SJTarget : Target
    {
        public const string TargetName = "商机助理";

        public SJTarget(string versionString) : base("商机助理", versionString)
        {
        }

        public SJTarget(Version version): base("商机助理", version)
        {
        }

        public SJTarget(int major, int minor, int build): base("商机助理", major, minor, build)
        {
        }
    }
}
