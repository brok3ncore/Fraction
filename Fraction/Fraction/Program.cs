using System;

public class Fraction
{
    public int Numerator { get; set; }
    public int Denominator { get; set; }

    // Конструктор с двумя параметрами
    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new ArgumentException("Знаменатель не может быть равен нулю.");

        Numerator = numerator;
        Denominator = denominator;
        Simplify();
    }

    // Конструктор копирования
    public Fraction(Fraction f)
    {
        Numerator = f.Numerator;
        Denominator = f.Denominator;
    }

    // Преобразование в double
    public double ToDouble()
    {
        return (double)Numerator / Denominator;
    }

    // Упрощение дроби
    private void Simplify()
    {
        int gcd = GCD(Math.Abs(Numerator), Math.Abs(Denominator));
        Numerator /= gcd;
        Denominator /= gcd;

        if (Denominator < 0)
        {
            Numerator = -Numerator;
            Denominator = -Denominator;
        }
    }

    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = a % b;
            a = b;
            b = temp;
        }
        return a;
    }

    // Арифметические операции
    public static Fraction Add(Fraction a, Fraction b)
    {
        return new Fraction(
            a.Numerator * b.Denominator + b.Numerator * a.Denominator,
            a.Denominator * b.Denominator
        );
    }

    public static Fraction Subtract(Fraction a, Fraction b)
    {
        return new Fraction(
            a.Numerator * b.Denominator - b.Numerator * a.Denominator,
            a.Denominator * b.Denominator
        );
    }

    public static Fraction Multiply(Fraction a, Fraction b)
    {
        return new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
    }

    public static Fraction Divide(Fraction a, Fraction b)
    {
        if (b.Numerator == 0)
            throw new DivideByZeroException("Деление на ноль невозможно.");

        return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
    }

    // Сравнение
    public bool Equals(Fraction f)
    {
        return this.Numerator * f.Denominator == f.Numerator * this.Denominator;
    }

    public static int Compare(Fraction a, Fraction b)
    {
        int left = a.Numerator * b.Denominator;
        int right = b.Numerator * a.Denominator;
        if (left == right) return 0;
        return left > right ? 1 : -1;
    }
    public override string ToString()
    {
        return $"{Numerator}/{Denominator}";
    }
}

// =================== Тестирование ===================

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Тестирование класса Fraction ===");
        Fraction f1 = new Fraction(2, 3);
        Fraction f2 = new Fraction(3, 4);

        Console.WriteLine($"f1 = {f1}");
        Console.WriteLine($"f2 = {f2}");

        Console.WriteLine($"f1 + f2 = {Fraction.Add(f1, f2)}");
        Console.WriteLine($"f1 - f2 = {Fraction.Subtract(f1, f2)}");
        Console.WriteLine($"f1 * f2 = {Fraction.Multiply(f1, f2)}");
        Console.WriteLine($"f1 / f2 = {Fraction.Divide(f1, f2)}");

        Console.WriteLine($"f1 == f2 ? {f1.Equals(f2)}");
        Console.WriteLine($"Compare(f1, f2) = {Fraction.Compare(f1, f2)}");
        Console.WriteLine($"f1 как double = {f1.ToDouble():F2}");

        Console.WriteLine("\n=== Простые числа ===");
        PrintPrimes(50);
    }

    // =================== Часть 2 — Простые числа ===================

    static void PrintPrimes(int N)
    {
        for (int i = 1; i <= N; i++)
        {
            if (IsPrime(i))
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ResetColor();

            Console.Write(i + " ");
        }

        Console.ResetColor();
        Console.WriteLine();
    }

    static bool IsPrime(int n)
    {
        if (n < 2) return false;
        for (int i = 2; i <= Math.Sqrt(n); i++)
            if (n % i == 0)
                return false;
        return true;
    }
}
