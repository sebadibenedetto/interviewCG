using System.Collections.Generic;
using TheCorcoranGroup.DLL.Enumerators;
using TheCorcoranGroup.Models;

namespace TheCorcoranGroup.DLL.Interfaces
{
    public interface IPresidentDomain
    {
        IList<President> GetPresidents(string name, Columns columnSort, SortDirection sortDirection);
    }
}
