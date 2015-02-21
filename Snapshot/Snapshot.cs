using System;

namespace Snap
{
    public static class Snapshot
    {
        public static dynamic TakeSnapshot<T>(this T instance, SnapShotFlags flags = SnapShotFlags.Default)
        {
            if (flags == SnapShotFlags.None)
                throw new ArgumentException("SnapShot Flags cannot be declared as 'None'.");

            var mapProperties = (flags & SnapShotFlags.Properties) == SnapShotFlags.Properties;
            var mapFields = (flags & SnapShotFlags.Fields) == SnapShotFlags.Fields;
            var convertEnums = (flags & SnapShotFlags.ConvertEnumsToString) == SnapShotFlags.ConvertEnumsToString;
            return InternalSnapshot<T>.Take(instance, mapProperties, mapFields, convertEnums);
        }
        
        internal static dynamic Take<T>(T instance, bool mapProperties, bool mapFields, bool convertEnumsToString)
        {
            return InternalSnapshot<T>.Take(instance, mapProperties, mapFields, convertEnumsToString);
        }
    }
}
