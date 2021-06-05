using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewaysAPI
{

    //Helper class used for initializing serial number 
    public class Counter
    {
        public AppDbContext context = new AppDbContext();
        public static int Count;
        public Counter()
        {
            Task();
        }

        public async void Task()
        {
            Count = await context.Gateways.CountAsync();
        }
        public static void Increment()
        {
            Count++;
        }
    }
}
