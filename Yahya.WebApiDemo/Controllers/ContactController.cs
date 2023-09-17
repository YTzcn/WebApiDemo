using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yahya.WebApiDemo.Models;

namespace Yahya.WebApiDemo.Controllers
{
    [Route("[controller]")]
    public class ContactController : Controller
    {
        [HttpGet]
        [Authorize(Roles="Admin")]
        public List<ContactModel> Get()
        {
            return new List<ContactModel>
            {
                new ContactModel{Id=1,FirstName="Aaa",LastName="bbb" }
            };
        }

    }
}
