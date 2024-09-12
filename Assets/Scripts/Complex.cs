using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complex
{
    public float Real { get; set; }
    public float Imaginary { get; set; }

    public Complex(float real, float imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public static Complex operator +(Complex a, Complex b)
        => new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);

    public static Complex operator -(Complex a, Complex b)
        => new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);

    public static Complex operator *(Complex a, Complex b)
        => new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);

    public static Complex operator /(Complex a, float b)
        => new Complex(a.Real / b, a.Imaginary / b);

    public static Complex operator /(Complex a, Complex b)
    {
        float divisor = b.Real * b.Real + b.Imaginary * b.Imaginary;
        float real = (a.Real * b.Real + a.Imaginary * b.Imaginary) / divisor;
        float imaginary = (a.Imaginary * b.Real - a.Real * b.Imaginary) / divisor;
        return new Complex(real, imaginary);
    }

    public static Complex operator -(Complex a)
       => new Complex(-a.Real, -a.Imaginary);

    public static Complex Zero => new Complex(0, 0);
    public static Complex One => new Complex(1, 0);

    // Method to calculate the magnitude (absolute value) of a complex number
    public float Abs()
    {
        return Mathf.Sqrt(Real * Real + Imaginary * Imaginary);
    }



    // Additional methods like magnitude, conjugate, etc., can be added as needed.
}
