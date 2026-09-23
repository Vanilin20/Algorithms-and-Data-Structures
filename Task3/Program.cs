const int N = 5;
string[] buffer = new string[N];
int next = 0;   
int count = 0;  

for (int i = 1; i <= 8; i++)
{
    buffer[next] = $"Подія {i}";
    next = (next + 1) % N;   
    if (count < N) count++;
}

int start = count < N ? 0 : next;
for (int k = 0; k < count; k++)
    Console.WriteLine(buffer[(start + k) % N]);