namespace Hishop.TransferManager
{
    using System;

    public class JDTarget : Target
    {
        public const string TargetName = "京东商家助手";

        public JDTarget(string versionString): base("京东商家助手", versionString)
        {
        }

        public JDTarget(Version version) : base("京东商家助手", version)
        {
        }

        public JDTarget(int major, int minor, int build): base("京东商家助手", major, minor, build)
        {
        }
    }
}
