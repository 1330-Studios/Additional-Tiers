namespace AdditionalTiers.Utils.Extensions;
internal static class Il2CppExtensions {
    public static Il2CppReferenceArray<T> Remove<T>(this Il2CppReferenceArray<T> reference, Func<T, bool> predicate) where T : Model => new(reference.Where(tmp => !predicate(tmp)).ToArray());
    public static Il2CppReferenceArray<T> Add<T>(this Il2CppReferenceArray<T> reference, params T[] newPart) where T : Model => ConcatArrayParams(reference, newPart);
    public static Il2CppReferenceArray<T> Add<T>(this Il2CppReferenceArray<T> reference, IEnumerable<T> enumerable) where T : Model => ConcatArrayEnumerable(reference, enumerable);
    public static T[] Add<T>(this T[] reference, params T[] newPart) where T : Model => ConcatArrayParams(reference, newPart);

    public static Il2CppSystem.Collections.Generic.IEnumerable<TC> SelectI<T, TR, TC>(this IEnumerable<T> enumerable, Func<T, TR> predicate) where TR : Il2CppObjectBase where TC : Il2CppObjectBase {
        var bases = new Il2CppSystem.Collections.Generic.List<TC>();
        using var enumerator = enumerable.GetEnumerator();
        while (enumerator.MoveNext())
            bases.Add(predicate(enumerator.Current).Cast<TC>());
        return bases.Cast<Il2CppSystem.Collections.Generic.IEnumerable<TC>>();
    }

    public static bool Is<T>(this Il2CppSystem.Object obj, out T ex) where T : Il2CppObjectBase {
        if (obj.GetIl2CppType() == Il2CppType.Of<T>()) {
            ex = obj.Cast<T>();
            return true;
        }

        ex = default;
        return false;
    }

    public static bool Is<T>(this Il2CppSystem.Object obj) where T : Il2CppObjectBase => obj.GetIl2CppType() == Il2CppType.Of<T>();

    public static Il2CppReferenceArray<T> ToIl2CppArray<T>(this T[] array) where T : Il2CppObjectBase => new(array);
    public static Il2CppSystem.Collections.Generic.List<T> ToIl2CppList<T>(this T[] array) where T : Il2CppObjectBase {
        Il2CppSystem.Collections.Generic.List<T> list = new();
        foreach (var t in array)
            list.Add(t);

        return list;
    }

    public static T CloneCast<T>(this Model obj) where T : Model => obj.Clone().Cast<T>();
    public static T CloneCast<T>(this T obj) where T : Model => obj.Clone().Cast<T>();

    public static T[] CloneCastArr<T>(this T[] obj) where T : Model => obj.Select(t => t.CloneCast()).ToArray();

    public static T[] CloneCastArr<T>(this Model[] obj) where T : Model => obj.Select(t => t.CloneCast<T>()).ToArray();

    private static T[] ConcatArray<T>(IReadOnlyList<T> a, IReadOnlyList<T> b) {
        var m = a.Count;
        var n = b.Count;
        var arr = new T[m + n];

        for (var i = 0; i < m + n; i++)
            if (i < m)
                arr[i] = a[i];
            else
                arr[i] = b[i - m];

        return arr;
    }

    private static T[] ConcatArrayParams<T>(T[] a, params T[] b) => ConcatArray(a, b);

    private static T[] ConcatArrayEnumerable<T>(T[] a, IEnumerable<T> e) => ConcatArray(a, e.ToArray());
}