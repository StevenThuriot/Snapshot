using System;

namespace Snap
{
    [Flags]
    public enum SnapShotFlags
    {
        None = 0,
        Fields = 1,
        Properties = 2,
        ConvertEnumsToString = 4,
        Default = Fields | Properties
    }
}