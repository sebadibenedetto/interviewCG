using System.Linq;
using System.Web.Http;
using TheCorcoranGroup.DLL.Enumerators;
using TheCorcoranGroup.DLL.Interfaces;
using TheCorcoranGroup.ViewModels;

namespace TheCorcoranGroup.WebAPI.Controllers
{
    public class PresidentsController : ApiController
    {
        private readonly IPresidentDomain _presidentDomain;
        public PresidentsController(IPresidentDomain presidentDomain)
        {
            _presidentDomain = presidentDomain;
        }

        /// <summary>
        /// Get the list of presidents, sorted and filtered
        /// </summary>
        /// <param name="columnSort">Column by which it is ordered</param>
        /// <param name="sortDirection">Sort direction (ascending or descending)</param>
        /// <param name="name">Property by which it is filtered (if it is empty, not filtered)</param>
        /// <returns>Return a collection of objects president</returns>
        public IHttpActionResult Get(Columns columnSort, SortDirection sortDirection, string name = null)
        {
            return Ok(_presidentDomain.GetPresidents(name, columnSort, sortDirection).Select(x => new PresidentModel
            {
                Name = x.Name,
                Birthday = x.Birthday,
                DeathDay = x.DeathDay
            }));
        }
    }
}
