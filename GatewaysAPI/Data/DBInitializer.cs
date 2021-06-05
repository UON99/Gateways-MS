using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewaysAPI.Data
{
    public class DBInitializer : IDBInitializer
    {
        private readonly AppDbContext _db;
        public DBInitializer(AppDbContext db)
        {
            _db = db;
        }
        public void Initialize()
        {
            if(_db.Database.GetPendingMigrations().Count()>0)
                _db.Database.Migrate();
        }

    }
}
