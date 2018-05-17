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
