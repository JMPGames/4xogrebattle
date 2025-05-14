public static class Common {
    public const int MAX_UNITS_PER_TROOP = 6;

    public static bool IndexAndValueExistsInArray<T>(int i, T[] a) {
        return IndexExistsInArray(i, a) && a[i] != null;
    }

    public static bool IndexExistsInArray<T>(int i, T[] a) {
        return i >= 0 && i < a.Length;
    }
}