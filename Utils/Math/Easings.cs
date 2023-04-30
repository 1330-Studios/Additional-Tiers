using DLLUtils;

namespace AdditionalTiers.Utils.Math;
internal class Easings {
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate float __u(float arg1, int arg2);

    internal static DLLFromMemory __l;
    internal static __u __i;

    public Easings() {
        for (; ; )
        {
            IL_06:
            uint num = 3473763082U;
            for (; ; )
            {
                uint num2;
                switch ((num2 = num ^ 4050603214U) % 3U) {
                    case 0U:
                        goto IL_06;
                    case 2U:
                        num = (num2 * 902337094U) ^ 628429146U;
                        continue;
                }
                return;
            }
        }
    }

    static Easings() {
        var ____m = new MemoryStream("AdditionalTiers.Easings.dfl"u8.ToArray());
        var ______s = new MemoryStream();
        using var ___d = new DeflateStream(______s, CompressionMode.Decompress);
        ____m.CopyTo(______s);

        var __d = new DeflateStream(new MemoryStream(Encoding.UTF8.GetString(______s.ToArray()).GetEmbeddedResource()), CompressionMode.Decompress);
        var __f = new DeflateStream(__d, CompressionMode.Decompress);
        try {
            try {
                var __m = new MemoryStream();
                try {
                    __f.CopyTo(__m);
                    for (; ; )
                    {
                        IL_2C:
                        var num = 2838491305U;
                        for (; ; )
                        {
                            uint num2;
                            switch ((num2 = num ^ 2649911324U) % 4U) {
                                case 1U:
                                    __l = new DLLFromMemory(__m.ToArray());
                                    num = (num2 * 52386304U) ^ 1023821007U;
                                    continue;
                                case 2U:
                                    goto IL_2C;
                                case 3U:
                                    var ___m = new MemoryStream("Interpolate"u8.ToArray());
                                    var ___s = new MemoryStream();
                                    var ____d = new DeflateStream(___s, CompressionMode.Decompress);
                                    ___m.CopyTo(___s);
                                    __i = __l.GetDelegateFromFuncName<__u>(Encoding.UTF8.GetString(___s.ToArray()));
                                    num = (num2 * 1863199109U) ^ 327842839U;
                                    ____d.Dispose();
                                    ___m.Dispose();
                                    ___s.Dispose();
                                    continue;
                            }
                            goto Block_7;
                        }
                    }
                    Block_7:;
                } finally
                {
                    for (; ; )
                    {
                        IL_9D:
                        var num3 = 3345278717U;
                        for (; ; )
                        {
                            uint num2;
                            switch ((num2 = num3 ^ 2649911324U) % 4U) {
                                case 1U:
                                    ((IDisposable)__m).Dispose();
                                    num3 = (num2 * 1608182900U) ^ 205730074U;
                                    continue;
                                case 2U:
                                    num3 = (num2 * 607686669U) ^ 591539682U;
                                    continue;
                                case 3U:
                                    goto IL_9D;
                            }
                            goto Block_9;
                        }
                    }
                    Block_9:;
                }
            } finally
            {
                for (; ; )
                {
                    IL_EC:
                    var num4 = 4174962098U;
                    for (; ; )
                    {
                        uint num2;
                        switch ((num2 = num4 ^ 2649911324U) % 4U) {
                            case 0U:
                                goto IL_EC;
                            case 2U:
                                ((IDisposable)__f).Dispose();
                                ((IDisposable)______s).Dispose();
                                num4 = (num2 * 2798151312U) ^ 3925187235U;
                                continue;
                            case 3U:
                                num4 = (num2 * 1154206355U) ^ 4036755384U;
                                continue;
                        }
                        goto Block_11;
                    }
                }
                Block_11:;
            }
        } finally
        {
            for (; ; )
            {
                IL_13B:
                var num5 = 4015522233U;
                for (; ; )
                {
                    uint num2;
                    switch ((num2 = num5 ^ 2649911324U) % 3U) {
                        case 1U:
                            ((IDisposable)__d).Dispose();
                            ((IDisposable)___d).Dispose();
                            ((IDisposable)____m).Dispose();
                            num5 = (num2 * 3716037674U) ^ 2243517990U;
                            continue;
                        case 2U:
                            goto IL_13B;
                    }
                    goto Block_13;
                }
            }
            Block_13:;
        }
    }

    internal static float Interpolate(float p, int type) => __i(p, type);
}