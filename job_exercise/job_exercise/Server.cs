using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Classes;
using JsonClass;

namespace Server
{
    public class Server
    {



        public string Request(string json_request)
        {
            using (var db = new DataContext())
            {
                
                try
                {
                    Device restored = JsonSerializer.Deserialize<Device>(json_request);
                    if (restored.DeviceId != 0) {
                        
                        var d = db.Devices
                        // Загрузить то устройство
                        .Where(c => c.DeviceId == restored.DeviceId)
                        .FirstOrDefault();

                        // Внести изменения
                        d.DeviceConf = restored.DeviceConf;
              
                        // Сохранить изменения
                        db.SaveChanges();
                                               
                        return JsonSerializer.Serialize<Response>(new Response { Success = true });
                    }
                    else
                    {
                        db.Add(restored);
                        db.SaveChanges();
                        return JsonSerializer.Serialize<Response>(new Response { Success = true });
                    }
                }
                catch { }
                

               
               
            }
            


            return JsonSerializer.Serialize<Response>(new Response { Success = false});

        }


        public string FirstDevice()
        {
            using (var db = new DataContext())
            {
                var device = db.Devices
                    .OrderBy(b => b.DeviceId)
                    .First();
                string json = JsonSerializer.Serialize<Device>(device);
                Console.WriteLine(json);

                return json;
            }
           
        }
        public void doSomething()
        {
            using (var db = new DataContext())
            {
                // Note: This sample requires the database to be created before running.
                Console.WriteLine($"Database path: {db.DbPath}.");

                // Create
                /*
                Console.WriteLine("Inserting a new device");
                Device d = new Device { DeviceConf = "Something conf" };
                db.Add(d);
                db.SaveChanges();
                */
                // Read
                Console.WriteLine("Querying for a device");
                var device = db.Devices
                    .OrderBy(b => b.DeviceId)
                    .First();

                
                
                //Console.WriteLine(Convert.ToString(device.DeviceId) + ' ' + device.DeviceConf);

                // Update
                /*
                Console.WriteLine("Updating the device and adding a sensor");
                device.DeviceConf = "Some thing 12345678da da ";
                device.Sensors.Add(
                    new Sensor { SensorConf = "123456789Hello World"});
                db.SaveChanges();

                // Adding M
                var sen = db.Sensors
                    .OrderBy(s => s.SensorId)
                    .First();

                Console.WriteLine("Adding a Measurement");

                string a = DateTime.Now.ToString("hh:mm:ss");
                Console.WriteLine(a);
                ulong h = Convert.ToUInt64(a[0].ToString() + a[1].ToString()) * 3600 + 
                    Convert.ToUInt64(a[3].ToString()+a[4].ToString())*60 + Convert.ToUInt64(a[6].ToString()+a[7].ToString());
                sen.Measurements.Add(
                    new Measurement { Value = 100000.8765, Timestamp = h });//Convert.ToUInt64(a)
                db.SaveChanges();

                // Delete
                /* Console.WriteLine("Delete the blog");
                 db.Remove(blog);
                 db.SaveChanges();*/

                
            }
        }
    }
}
