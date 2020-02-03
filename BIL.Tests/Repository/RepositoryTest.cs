using BIL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIL.Tests.Repository
{
    public abstract class RepositoryTest
    {
        protected DbContextOptions<BILContext> BuildInMemoryDatabase(string databaseName)
        {
            var options = new DbContextOptionsBuilder<BILContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            return options;

        }
    }
}
