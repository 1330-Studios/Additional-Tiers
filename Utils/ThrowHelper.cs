using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdditionalTiers.Utils;
internal static class ThrowHelper {
    [DoesNotReturn]
    internal static void ThrowMoreThanOneElementException() {
        throw new InvalidOperationException("Sequence contains more than one element");
    }

    [DoesNotReturn]
    internal static void ThrowMoreThanOneMatchException() {
        throw new InvalidOperationException("Sequence contains more than one matching element");
    }

    [DoesNotReturn]
    internal static void ThrowNoElementsException() {
        throw new InvalidOperationException("Sequence contains no elements");
    }

    [DoesNotReturn]
    internal static void ThrowNoMatchException() {
        throw new InvalidOperationException("Sequence contains no matching element");
    }

    [DoesNotReturn]
    internal static void ThrowNotSupportedException() {
        throw new NotSupportedException();
    }
}