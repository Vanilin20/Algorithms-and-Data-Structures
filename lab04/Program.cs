using System;

namespace Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== СЦЕНАРІЙ 1: Використання оператора using ===");
            Scenario1_Using();

            Console.WriteLine("\n=== СЦЕНАРІЙ 2: Явний виклик Dispose() ===");
            Scenario2_ExplicitDispose();

            Console.WriteLine("\n=== СЦЕНАРІЙ 3: Об'єкт без Dispose (робота GC) ===");
            Scenario3_GarbageCollector();

            Console.WriteLine("\nПрограма завершила роботу.");
        }

        static void Scenario1_Using()
        {
            using (var sensor = new SensorReader(101))
            {
                sensor.ReadValue();
            }
        }

        static void Scenario2_ExplicitDispose()
        {
            var sensor = new SensorReader(202);
            sensor.ReadValue();
            sensor.Dispose();
        }

        static void Scenario3_GarbageCollector()
        {
            void CreateUnreferencedSensor()
            {
                var sensor = new SensorReader(303);
                sensor.ReadValue();
            }

            CreateUnreferencedSensor();

            Console.WriteLine("Запуск GC.Collect()...");
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}