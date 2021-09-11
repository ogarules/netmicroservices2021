using System;

namespace CronNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Ejecutando Cron {DateTime.UtcNow}");
            
            System.Threading.Thread.Sleep(5000);

            string path = $"/ogatemp/log-{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}.log";
            System.IO.File.WriteAllText(path, $"Tarea ejecutada => {DateTime.UtcNow.ToString()}");

            Console.WriteLine($"Ejecución exitosa del Cron {DateTime.UtcNow}");
        }
    }
}
