using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdditionalTiers.Utils;
internal static class RuntimeInitializer {

    public static void Initialize(MelonAssembly masm) {
        var sw = Stopwatch.StartNew();
        int countPrepared = 0;

        masm.Assembly.GetValidTypes().AsParallel().AsUnordered().ForAll((Type type) => {
            Func<MethodInfo, bool> VerifyM = (a) => !a.IsAbstract && !a.ContainsGenericParameters && a.DeclaringType == type && a.HasMethodBody();
            Func<ConstructorInfo, bool> VerifyC = (a) => !a.IsAbstract && !a.ContainsGenericParameters && a.DeclaringType == type && a.HasMethodBody();

            type.GetMethods(all).Where(VerifyM).Do(mInfo => {
                RuntimeHelpers.PrepareMethod(mInfo.MethodHandle);
                countPrepared++;
            });

            type.GetConstructors(all).Where(VerifyC).Do(mInfo => {
                RuntimeHelpers.PrepareMethod(mInfo.MethodHandle);
                countPrepared++;
            });

            foreach (var accessors in type.GetProperties().Select(a=>a.GetAccessors())) {
                accessors.Where(VerifyM).Do(mInfo => {
                    RuntimeHelpers.PrepareMethod(mInfo.MethodHandle);
                    countPrepared++;
                });
            }
        });
        sw.Stop();
        RuntimeInfo.Logger.Msg($"Prepared {countPrepared} methods in {sw.Elapsed.TotalMilliseconds:N2}ms!");
    }
}