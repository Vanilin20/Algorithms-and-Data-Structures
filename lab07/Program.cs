using System;
using System.Collections.Generic;

class Program
{
    // --- ВХІДНІ ДАНІ ДЛЯ ЗАДАЧІ 1 ---
    static string[] lines = {
        "",
        "a+b",
        "{[a]+(b)}",
        "([)]",
        "a)",
        "(a[b]",
        "f(x[1])*{y+(z)}"
    };

    // --- КЛАС ТА ВХІДНІ ДАНІ ДЛЯ ЗАДАЧІ 2 ---
    public class Request
    {
        public string Name { get; set; }
        public int Minutes { get; set; }

        public Request(string name, int minutes)
        {
            Name = name;
            Minutes = minutes;
        }
    }

    static List<Request> requests = new List<Request>
    {
        new Request("Довідка про навчання", 4),
        new Request("Академічна відпустка", 7),
        new Request("Копія наказу", 2),
        new Request("Виписка з оцінками", 6),
        new Request("Перескладання", 9),
        new Request("Студентський квиток", 3)
    };

    // --- ЗАДАЧА 1: Перевірка дужок (Стек) ---
    static bool IsBalanced(string line)
    {
        Stack<char> stack = new Stack<char>();
        Dictionary<char, char> brackets = new Dictionary<char, char>()
        {
            { ')', '(' },
            { ']', '[' },
            { '}', '{' }
        };

        foreach (char c in line)
        {
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            else if (c == ')' || c == ']' || c == '}')
            {
                // Перевірка стека на порожність перед зняттям елемента
                if (stack.Count == 0)
                    return false;
                
                char top = stack.Pop();
                if (top != brackets[c])
                    return false;
            }
        }

        // Якщо стек порожній — усі дужки збалансовані
        return stack.Count == 0;
    }

    // --- ЗАДАЧА 2: Обробка заявок (Черга) ---
    static (int totalTime, double avgWaitTime) ProcessQueue(List<Request> reqList)
    {
        Queue<Request> queue = new Queue<Request>(reqList);

        // Обробка випадку порожньої черги без падіння
        if (queue.Count == 0)
        {
            Console.WriteLine("Обробляти нічого.");
            return (0, 0);
        }

        int currentTime = 0;
        int totalWaitingTime = 0;
        int count = 0;

        while (queue.Count > 0)
        {
            Request req = queue.Dequeue();
            int startTime = currentTime;
            int endTime = startTime + req.Minutes;

            int waitingTime = startTime;
            totalWaitingTime += waitingTime;

            count++;
            Console.WriteLine($"{count}. {req.Name} з {startTime} до {endTime} хв");

            currentTime = endTime;
        }

        Console.WriteLine($"Оброблено заявок: {count}, загальний час: {currentTime} хв");
        
        double avgWaitTime = (double)totalWaitingTime / count;
        return (currentTime, avgWaitTime);
    }

    static void Main(string[] args)
    {
        // Налаштування консолі для коректного відображення українських символів
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== ЗАДАЧА 1: Перевірка дужок ===");
        foreach (string line in lines)
        {
            string result = IsBalanced(line) ? "так" : "ні";
            Console.WriteLine($"'{line}' -> {result}");
        }

        Console.WriteLine("\n=== ЗАДАЧА 2: Обробка заявок ===");
        var (totalTime, avgWait) = ProcessQueue(requests);
        Console.WriteLine($"Середній час очікування: {avgWait:F2} хв");
    }
}