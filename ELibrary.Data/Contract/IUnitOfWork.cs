using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Data.Contract
{
    public interface IUnitOfWork
    {
        Task Save();
    }
}
