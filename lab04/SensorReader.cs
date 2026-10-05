using System;

namespace Lab3
{
    public class SensorReader : IDisposable
    {
        private readonly int _sensorId;
        private bool _isReading;
        private bool _disposed = false;

        public int SensorId => _sensorId;
        public bool IsReading => _isReading;

        public SensorReader(int sensorId)
        {
            _sensorId = sensorId;
            _isReading = true;
            Console.WriteLine($"[Сенсор #{_sensorId}]: Запущено читання даних.");
        }

        public void ReadValue()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(SensorReader), $"Сенсор #{_sensorId} закрито. Читання неможливе.");

            if (_isReading)
            {
                double value = new Random().NextDouble() * 100.0;
                Console.WriteLine($"[Сенсор #{_sensorId}]: Отримано значення = {value:F2}");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Console.WriteLine($"[Dispose(true)] Сенсор #{_sensorId}: Звільнення керованих ресурсів.");
                }

                if (_isReading)
                {
                    Console.WriteLine($"[Dispose] Сенсор #{_sensorId}: Зупинено читання (звільнення ресурсу).");
                    _isReading = false;
                }

                _disposed = true;
            }
        }

        ~SensorReader()
        {
            Console.WriteLine($"[Деструктор]: Виклик для Сенсора #{_sensorId}");
            Dispose(false);
        }
    }
}