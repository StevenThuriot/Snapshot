namespace Snap
{
    public static class Snapshot
    {
        public static dynamic TakeSnapshot<T>(this T instance, bool snapProperties = true, bool snapFields = true)
        {
            return InternalSnapshot<T>.Take(instance, snapProperties, snapFields);
        }


        public static dynamic Take<T>(T instance, bool mapProperties = true, bool mapFields = true)
        {
            return InternalSnapshot<T>.Take(instance, mapProperties, mapFields);
        }
    }
}
