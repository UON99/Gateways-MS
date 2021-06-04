using System;
using System.Collections.Generic;

#nullable disable

namespace GatewaysAPI
{
    public partial class Peripheral
    {
        public Peripheral()
        {
            this.Uid = Guid.NewGuid();
        }
        public Guid Uid { get; set; }
        public string Vendor { get; set; }
        public DateTime DateCreated { get; set; }
        public int Status { get; set; }
        public string GatewaySerialNumber { get; set; }

        public virtual Gateway GatewaySerialNumberNavigation { get; set; }
    }
}
