using System;
using System.Collections.Generic;
using System.Text;
using System;
using Xunit;
using GatewaysAPI;
using GatewaysAPI.Controllers;
using System.ComponentModel.DataAnnotations;


namespace GatewaysTEST
{
    public class GatewayTest
    {
        GatewayController gController = new GatewayController(new AppDbContext());
        [Fact]
        public async void PostInvalidIP()
        {
            Gateway gateway = new Gateway();
            gateway.Hrname = "Something";
            gateway.Ipv4 = "0.0.0.0";

            try { 
            await gController.PostGateway(gateway);
            }catch(Exception e)
            {
                Assert.IsType<ValidationException>(e);
            }

        }
    }
}
