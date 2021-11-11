using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Classes;
using JsonClass;
namespace Client
{

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hi");
            Console.Read();

            Server.Server server = new Server.Server();

            Device device_request = new Device {DeviceId = 2, DeviceConf = "jkjkjkjkjkjk"};
            string json_request = JsonSerializer.Serialize<Device>(device_request);
            Console.WriteLine(server.Request(json_request));

           
           /* 
            string json = server.FirstDevice();
            Device restored = JsonSerializer.Deserialize<Device>(json);
            Console.WriteLine(Convert.ToString(restored.DeviceId) + ' ' + restored.DeviceConf + ' ' + Convert.ToString(restored.Sensors.Count));
        */

        }
    }
}