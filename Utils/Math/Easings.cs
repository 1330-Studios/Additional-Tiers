using SysMath = System.Math;

namespace AdditionalTiers.Utils.Math;

public static class Easings {
    /// <summary>
    /// Modeled after the line y = x
    /// </summary>
    public static float Linear(float p) {
        return p;
    }

    /// <summary>
    /// Modeled after the parabola y = x^2
    /// </summary>
    public static float QuadraticEaseIn(float p) {
        return p * p;
    }

    /// <summary>
    /// Modeled after the parabola y = -x^2 + 2x
    /// </summary>
    public static float QuadraticEaseOut(float p) {
        return -(p * (p - 2));
    }

    /// <summary>
    /// Modeled after the piecewise quadratic
    /// y = (1/2)((2x)^2)             ; [0, 0.5)
    /// y = -(1/2)((2x-1)*(2x-3) - 1) ; [0.5, 1]
    /// </summary>
    public static float QuadraticEaseInOut(float p) {
        if (p < 0.5f) {
            return 2 * p * p;
        }

        return -2 * p * p + 4 * p - 1;
    }

    /// <summary>
    /// Modeled after the cubic y = x^3
    /// </summary>
    public static float CubicEaseIn(float p) {
        return p * p * p;
    }

    /// <summary>
    /// Modeled after the cubic y = (x - 1)^3 + 1
    /// </summary>
    public static float CubicEaseOut(float p) {
        var f = p - 1;
        return f * f * f + 1;
    }

    /// <summary>	
    /// Modeled after the piecewise cubic
    /// y = (1/2)((2x)^3)       ; [0, 0.5)
    /// y = (1/2)((2x-2)^3 + 2) ; [0.5, 1]
    /// </summary>
    public static float CubicEaseInOut(float p) {
        if (p < 0.5f) {
            return 4 * p * p * p;
        }

        var f = 2 * p - 2;
        return 0.5f * f * f * f + 1;
    }

    /// <summary>
    /// Modeled after the quartic x^4
    /// </summary>
    public static float QuarticEaseIn(float p) {
        return p * p * p * p;
    }

    /// <summary>
    /// Modeled after the quartic y = 1 - (x - 1)^4
    /// </summary>
    public static float QuarticEaseOut(float p) {
        var f = p - 1;
        return f * f * f * (1 - p) + 1;
    }
    /// <summary>
    /// Modeled after the piecewise quartic
    /// y = (1/2)((2x)^4)        ; [0, 0.5)
    /// y = -(1/2)((2x-2)^4 - 2) ; [0.5, 1]
    /// </summary>
    public static float QuarticEaseInOut(float p) {
        if (p < 0.5f) {
            return 8 * p * p * p * p;
        }

        var f = p - 1;
        return -8 * f * f * f * f + 1;
    }

    /// <summary>
    /// Modeled after the quintic y = x^5
    /// </summary>
    public static float QuinticEaseIn(float p) {
        return p * p * p * p * p;
    }

    /// <summary>
    /// Modeled after the quintic y = (x - 1)^5 + 1
    /// </summary>
    public static float QuinticEaseOut(float p) {
        var f = p - 1;
        return f * f * f * f * f + 1;
    }

    /// <summary>
    /// Modeled after the piecewise quintic
    /// y = (1/2)((2x)^5)       ; [0, 0.5)
    /// y = (1/2)((2x-2)^5 + 2) ; [0.5, 1]
    /// </summary>
    public static float QuinticEaseInOut(float p) {
        if (p < 0.5f) {
            return 16 * p * p * p * p * p;
        }

        var f = 2 * p - 2;
        return 0.5f * f * f * f * f * f + 1;
    }

    /// <summary>
    /// Modeled after quarter-cycle of sine wave
    /// </summary>
    public static float SineEaseIn(float p) {
        return (float)SysMath.Sin((p - 1) * SysMath.PI / 2) + 1;
    }

    /// <summary>
    /// Modeled after quarter-cycle of sine wave (different phase)
    /// </summary>
    public static float SineEaseOut(float p) {
        return (float)SysMath.Sin(p * SysMath.PI / 2);
    }

    /// <summary>
    /// Modeled after half sine wave
    /// </summary>
    public static float SineEaseInOut(float p) {
        return 0.5f * (1 - (float)SysMath.Cos(p * SysMath.PI));
    }
}