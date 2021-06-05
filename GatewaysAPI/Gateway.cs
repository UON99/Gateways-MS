using System;
using System.Collections.Generic;

#nullable disable

namespace GatewaysAPI
{
    public partial class Gateway
    {
        public Gateway()
        {
            Peripherals = new HashSet<Peripheral>();
        }

        public string SerialNumber { get; set; }
        public string Hrname { get; set; }
        public string Ipv4 { get; set; }

        public virtual ICollection<Peripheral> Peripherals { get; set; }
    }
}
