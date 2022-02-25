using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    interface IDataContext
    {
        DbSet<Person> People { get; set; }
        Task<int> SaveChanges();
    }
}
