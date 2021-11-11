using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes

{
    public class Device
    {
        public int DeviceId { get; set; }
        public string DeviceConf { get; set; }

        //public List<Sensor> Sensors { get; } = new List<Sensor>();
        public ICollection<Sensor> Sensors { get; set; } 
    }

    public class Sensor
    {
        public int SensorId { get; set; }
        public string SensorConf { get; set; }

        [ForeignKey("DeviceId")]
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public List<Measurement> Measurements { get; } = new List<Measurement>();
    }

    public class Measurement
    {
        public int MeasurementId { get; set; }
        public double Value { get; set; }
        public ulong Timestamp { get; set; }

        [ForeignKey("SensorId")]
        public int SensorId { get; set; }
        public Sensor Sensor { get; set; }

    }
}
