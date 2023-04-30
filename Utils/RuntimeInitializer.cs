using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AdditionalTiers.Utils;
internal static class RuntimeInitializer {

    public static void Initialize(MelonAssembly melonAssembly) {
        var sw = Stopwatch.StartNew();
        var countPrepared = 0;

        melonAssembly.Assembly.GetValidTypes().AsParallel().AsUnordered().ForAll(type => {
            bool VerifyM(MethodInfo a) => !a.IsAbstract && !a.ContainsGenericParameters && a.DeclaringType == type && a.HasMethodBody();
            bool VerifyC(ConstructorInfo a) => !a.IsAbstract && !a.ContainsGenericParameters && a.DeclaringType == type && a.HasMethodBody();

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