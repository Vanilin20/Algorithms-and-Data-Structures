// ЛАБОРАТОРНА 8. Швидкий пошук ключів
// Дані не змінювати. Міняти тільки код фрагментів.
// Старі варіанти залишено поруч закоментованими.

using System;
using System.Collections.Generic;

record Client(int Id, string Name);
record Order(int Number, int ClientId, int Sum);

class Program
{
    static readonly List<Client> clients = new()
    {                               // id, прізвище
        new Client(101, "Коваленко"),
        new Client(102, "Мельник"),
        new Client(103, "Шевчук"),
        new Client(104, "Бондар"),
        new Client(105, "Ткачук"),
        new Client(106, "Гриценко"),
    };

    static readonly List<Order> orders = new()
    {                               // номер, id клієнта, сума
        new Order(1, 103, 450),  new Order(2, 101, 120),
        new Order(3, 105, 980),  new Order(4, 103, 300),
        new Order(5, 102, 75),   new Order(6, 106, 1500),
        new Order(7, 101, 220),  new Order(8, 103, 60),
    };

    static readonly List<int> blocked = new() { 102, 104 };   // заблоковані клієнти

    // Фрагмент 1. Ім'я клієнта для кожного замовлення
    // ПОТРІБНА структура: Dictionary<int, Client>, ключ — id клієнта.
    static void Fragment1()
    {
        // СТАРИЙ ВАРІАНТ: для кожного замовлення — повний прохід по clients.
        // foreach (var order in orders)
        //     foreach (var client in clients)
        //         if (client.Id == order.ClientId)
        //             Console.WriteLine("Замовлення " + order.Number + " — " + client.Name);

        // НОВИЙ ВАРІАНТ: словник будується ОДИН раз, далі — пошук за ключем.
        var byId = new Dictionary<int, Client>();
        foreach (var client in clients)
            byId[client.Id] = client;

        foreach (var order in orders)   // обходимо список orders — він задає порядок виводу
            if (byId.TryGetValue(order.ClientId, out var client))
                Console.WriteLine("Замовлення " + order.Number + " — " + client.Name);
    }

    // Фрагмент 2. Сума замовлень, крім заблокованих клієнтів
    // ПОТРІБНА структура: HashSet<int>, ключ — id заблокованого клієнта.
    static void Fragment2()
    {
        // СТАРИЙ ВАРІАНТ: blocked.Contains на List — лінійний прохід по списку для кожного замовлення.
        // int total = 0;
        // foreach (var order in orders)
        //     if (!blocked.Contains(order.ClientId))
        //         total = total + order.Sum;
        // Console.WriteLine("Сума прийнятих замовлень: " + total);

        // НОВИЙ ВАРІАНТ: множина будується один раз, перевірка належності — за ключем.
        var blockedSet = new HashSet<int>(blocked);

        int total = 0;
        foreach (var order in orders)
            if (!blockedSet.Contains(order.ClientId))
                total = total + order.Sum;
        Console.WriteLine("Сума прийнятих замовлень: " + total);
    }

    // Фрагмент 3. Скільки замовлень у кожного клієнта
    // ПОТРІБНА структура: Dictionary<int, int> (ключ — id клієнта, значення — кількість)
    // + HashSet<int> уже надрукованих id.
    static void Fragment3()
    {
        // СТАРИЙ ВАРІАНТ: для кожного нового клієнта — повторний прохід по всіх orders,
        // плюс counted.Contains — ще один лінійний пошук по списку.
        // var counted = new List<int>();
        // foreach (var order in orders)
        // {
        //     if (counted.Contains(order.ClientId))
        //         continue;
        //     int count = 0;
        //     foreach (var other in orders)
        //         if (other.ClientId == order.ClientId)
        //             count = count + 1;
        //     counted.Add(order.ClientId);
        //     Console.WriteLine(order.ClientId + ": " + count);
        // }

        // НОВИЙ ВАРІАНТ: один прохід рахує, другий друкує.
        var counts = new Dictionary<int, int>();
        foreach (var order in orders)
            counts[order.ClientId] = counts.GetValueOrDefault(order.ClientId) + 1;

        // Друкуємо, обходячи orders (а не словник), щоб зберегти порядок першої появи.
        var printed = new HashSet<int>();
        foreach (var order in orders)
            if (printed.Add(order.ClientId))   // Add повертає false, якщо id уже був
                Console.WriteLine(order.ClientId + ": " + counts[order.ClientId]);
    }

    // Фрагмент 4. Найбільше замовлення
    // НЕ ПОТРІБНА структура: це один прохід по списку без жодного пошуку за ключем.
    // Залишено як є.
    static void Fragment4()
    {
        var best = orders[0];
        foreach (var order in orders)
            if (order.Sum > best.Sum)
                best = order;
        Console.WriteLine("Найбільше замовлення: " + best.Sum);
    }

    // Фрагмент 5. Чийсь «оптимізований» варіант фрагмента 1
    // НЕ ПОТРІБНА нова структура: словник тут уже є, але він створюється
    // всередині циклу — заново для кожного замовлення. Тож це той самий
    // подвійний цикл, що й у старому фрагменті 1. Правильна версія — новий фрагмент 1.
    // Залишено як є.
    static void Fragment5()
    {
        foreach (var order in orders)
        {
            var byId = new Dictionary<int, Client>();
            foreach (var client in clients)
                byId[client.Id] = client;
            Console.WriteLine("Замовлення " + order.Number + " — " + byId[order.ClientId].Name);
        }
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Фрагмент 1 ===");
        Fragment1();
        Console.WriteLine("\n=== Фрагмент 2 ===");
        Fragment2();
        Console.WriteLine("\n=== Фрагмент 3 ===");
        Fragment3();
        Console.WriteLine("\n=== Фрагмент 4 ===");
        Fragment4();
        Console.WriteLine("\n=== Фрагмент 5 ===");
        Fragment5();
    }
}
