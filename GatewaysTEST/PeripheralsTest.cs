using System;
using Xunit;
using GatewaysAPI;
using GatewaysAPI.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace GatewaysTEST
{
    public class PeripheralsTest
    {

        GatewayController gController = new GatewayController(new AppDbContext());
        PeripheralController pController = new PeripheralController(new AppDbContext());

        [Fact]
        public async void MoreThan10PeripheralsperGateway()
        {
                Gateway gateway = new Gateway();
                gateway.SerialNumber = "Something";
                gateway.Hrname = "Test123";
                gateway.Ipv4 = "1.1.1.1";

                await gController.PostGateway(gateway);

                for (int i = 0; i < 10; i++)
                {
                    Peripheral p = new Peripheral();
                    p.Vendor = "Someone";
                    p.GatewaySerialNumber = "Something";
                    p.Status = 1;
                    await pController.PostPeripheral(p);
                }

                Peripheral p2 = new Peripheral();
                p2.Vendor = "Someone";
                p2.GatewaySerialNumber = "Something";
                p2.Status = 1;

                try
                {
                    await pController.PostPeripheral(p2);

                }

                catch (Exception e)
                {
                    ValidationException expected = new ValidationException();

                    Assert.IsType<ValidationException>(e);
                }

            Teardown();

        }
        private async void Teardown()
        {
            var gateway = gController.GetGateway("Something").Value;

            foreach(Peripheral p in gateway.Peripherals)
            {
                await pController.DeletePeripheral(p.Uid);
            }

            await gController.DeleteGateway("Something");
            Thread.Sleep(1000);
        }
    }
}
