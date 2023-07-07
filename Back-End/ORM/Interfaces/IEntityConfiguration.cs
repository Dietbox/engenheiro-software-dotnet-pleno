using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.ORM.Interfaces
{
    public interface IEntityConfiguration
    {
        void Configure(ModelBuilder builder);
    }
}
