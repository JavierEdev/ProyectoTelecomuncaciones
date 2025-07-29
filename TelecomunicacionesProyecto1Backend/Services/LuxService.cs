using System.IO.Ports;
using System.Collections.Concurrent;

public class LuxService : BackgroundService
{
    private readonly ConcurrentQueue<float> _valores = new();
    private SerialPort _serial;

    public IEnumerable<float> Valores => _valores;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _serial = new SerialPort("COM6", 9600);
        _serial.Open();

        return Task.Run(() =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var line = _serial.ReadLine().Trim();
                    if (float.TryParse(line, out float lux))
                    {
                        Console.WriteLine($"Lux leído: {lux}");
                        _valores.Enqueue(lux);
                        if (_valores.Count > 50)
                            _valores.TryDequeue(out _);
                    }
                }
                catch { }
            }
        }, stoppingToken);
    }
}
