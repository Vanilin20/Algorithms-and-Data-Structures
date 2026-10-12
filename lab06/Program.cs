using System.Text;

class Category
{
    public string Name;
    public int Products; // товарів безпосередньо в цій категорії
    public List<Category> Children;

    public Category(string name, int products, List<Category> children)
    {
        Name = name;
        Products = products;
        Children = children;
    }
}

class Program
{
    static int callCount = 0; // лічильник викликів

    static void PrintTree(Category node, int level)
    {
        callCount++;
        Console.WriteLine(new string(' ', level * 2) + node.Name);
        foreach (var child in node.Children)
            PrintTree(child, level + 1);
    }

    static int CountProducts(Category node)
    {
        callCount++;
        // базовий випадок: у листка Children порожній, цикл не виконується
        int total = node.Products;
        foreach (var child in node.Children)
            total += CountProducts(child);
        return total;
    }

    static int MaxDepth(Category node)
    {
        callCount++;
        // базовий випадок: категорія без підкатегорій
        if (node.Children.Count == 0)
            return 1;

        int deepest = 0;
        foreach (var child in node.Children)
            deepest = Math.Max(deepest, MaxDepth(child));
        return 1 + deepest;
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var catalog = new Category("Каталог", 0, new List<Category>
        {
            new Category("Одяг", 0, new List<Category>
            {
                new Category("Верхній одяг", 0, new List<Category>
                {
                    new Category("Куртки", 12, new List<Category>()),
                    new Category("Пальта", 7, new List<Category>()),
                }),
                new Category("Светри", 15, new List<Category>()),
            }),
            new Category("Взуття", 4, new List<Category>
            {
                new Category("Кросівки", 23, new List<Category>()),
                new Category("Чоботи", 9, new List<Category>()),
            }),
            new Category("Аксесуари", 0, new List<Category>
            {
                new Category("Сумки", 18, new List<Category>()),
                new Category("Ремені", 5, new List<Category>()),
            }),
        });

        callCount = 0;
        PrintTree(catalog, 0);
        Console.WriteLine($"Викликів printTree: {callCount}");

        callCount = 0;
        Console.WriteLine($"Товарів: {CountProducts(catalog)}");
        Console.WriteLine($"Викликів countProducts: {callCount}");

        callCount = 0;
        Console.WriteLine($"Глибина: {MaxDepth(catalog)}");
        Console.WriteLine($"Викликів maxDepth: {callCount}");
    }
}
