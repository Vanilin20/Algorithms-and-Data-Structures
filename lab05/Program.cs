// ЛАБОРАТОРНА 5. Сортування об'єктів
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Student
{
    public string Surname { get; }
    public string Group { get; }
    public int Grade { get; }   // середній бал
    public int Year { get; }    // рік вступу

    public Student(string surname, string group, int grade, int year)
    {
        Surname = surname; Group = group; Grade = grade; Year = year;
    }
}

class Program
{
    // Українська абетка. Порівняння за кодами Unicode тут не підходить:
    // літера «І» має менший код, ніж «Д», і «Іваненко» опинився б першим.
    const string Alphabet = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";

    static string AlphaKey(string text)
    {
        var sb = new StringBuilder();
        foreach (char ch in text.ToUpperInvariant())
        {
            int idx = Alphabet.IndexOf(ch);
            sb.Append((char)('A' + (idx >= 0 ? idx : Alphabet.Length)));
        }
        return sb.ToString();
    }

    static void PrintAll(string title, IEnumerable<Student> items)
    {
        Console.WriteLine(title);
        foreach (var s in items)
            Console.WriteLine($"{s.Surname} {s.Group} {s.Grade} {s.Year}");
        Console.WriteLine();
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var students = new List<Student>
        {
            new("Ткаченко",  "ІПЗ-3/1", 85, 2024),
            new("Бондар",    "ІПЗ-3/2", 92, 2023),
            new("Іваненко",  "ІПЗ-3/1", 85, 2024),
            new("Коваль",    "ІПЗ-3/2", 78, 2024),
            new("Сидоренко", "ІПЗ-3/1", 85, 2023),
            new("Мельник",   "ІПЗ-3/2", 92, 2024),
            new("Гриценко",  "ІПЗ-3/1", 78, 2023),
            new("Дяченко",   "ІПЗ-3/2", 85, 2023),
        };

        PrintAll("Вихідний список", students);

        // TODO 1: за прізвищем, за абеткою
        // OrderBy не змінює вихідний список і є стабільним
        var bySurname = students.OrderBy(s => AlphaKey(s.Surname), StringComparer.Ordinal);
        PrintAll("1. За прізвищем", bySurname);

        // TODO 2: за балом, від вищого до нижчого
        var byGrade = students.OrderByDescending(s => s.Grade);
        PrintAll("2. За балом (спадання)", byGrade);

        // TODO 3: за групою, а всередині групи — за балом від вищого
        // головний ключ — OrderBy (група), другий — ThenByDescending (бал)
        var byGroupGrade = students
            .OrderBy(s => s.Group, StringComparer.Ordinal)
            .ThenByDescending(s => s.Grade);
        PrintAll("3. За групою, потім за балом", byGroupGrade);

        // Частина 2: серед однакових балів — прізвища за абеткою
        var byGradeSurname = students
            .OrderByDescending(s => s.Grade)
            .ThenBy(s => AlphaKey(s.Surname), StringComparer.Ordinal);
        PrintAll("Бал спадання + прізвище за абеткою", byGradeSurname);
    }
}