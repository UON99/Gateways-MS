using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace GatewaysAPI
{
    public partial class Gateway
    {
        public Gateway()
        {
            Counter count2 = new Counter();
            this.SerialNumber = "Serial" + Counter.Count;
            Peripherals = new HashSet<Peripheral>();
        }

        public string SerialNumber { get; set; }
        public string Hrname { get; set; }
        public string Ipv4 { get; set; }

        public virtual ICollection<Peripheral> Peripherals { get; set; }
    }
}
