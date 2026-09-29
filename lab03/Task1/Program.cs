var lines = new List<string>
{
    "Запуск системи", "Вхід користувача", "Помилка з'єднання",
    "Повторна спроба", "Вихід"
};


for (int i = lines.Count - 1; i >= 0; i--)
    Console.WriteLine(lines[i]);