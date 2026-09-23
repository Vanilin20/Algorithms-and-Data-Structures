var operations = new (int Month, decimal Amount)[]
{
    (1, 1500m), (3, 200m), (1, 300m), (12, 999m), (7, 450m), (3, 50m)
};

decimal[] totals = new decimal[12];

foreach (var op in operations)
    totals[op.Month - 1] += op.Amount; 

for (int m = 0; m < 12; m++)
    Console.WriteLine($"Місяць {m + 1}: {totals[m]}");