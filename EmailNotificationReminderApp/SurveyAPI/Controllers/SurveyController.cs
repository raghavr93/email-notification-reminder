using SurveyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SurveyAPI.Controllers
{
    
    public class SurveyController : ApiController
    {
        private readonly IMailSender _mailSender;
        private readonly IResponseUpdate _respUpdate;

        
        public SurveyController(IMailSender mailSender, IResponseUpdate respUpdate)
        {
            _mailSender = mailSender;
            _respUpdate = respUpdate;
        }

        public IHttpActionResult Get()
        {
            return Ok("COOL");
        }
        // GET: Survey
        public IHttpActionResult Get(int Id)
        {
            var respEmail = _respUpdate.UpdateResponse(Id);

            _mailSender.SendMail(respEmail.EmailId);

            return Ok();
        }
    }
}
