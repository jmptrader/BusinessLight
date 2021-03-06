namespace BusinessLight.PhoneBook.Api.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.Http.Description;

    using BusinessLight.PhoneBook.Dto;
    using BusinessLight.PhoneBook.Service;

    using FluentValidation;

    using Swashbuckle.Swagger.Annotations;

    [EnableCors("*", "*", "*")]
    public class ContactInfoController : ApiController
    {
        private readonly ContactApplicationService contactApplicationService;

        public ContactInfoController(ContactApplicationService contactApplicationService)
        {
            if (contactApplicationService == null)
            {
                throw new ArgumentNullException(nameof(contactApplicationService));
            }

            this.contactApplicationService = contactApplicationService;
        }

        [HttpGet]
        [ResponseType(typeof(ContactInfoDetailDto))]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(ContactInfoDetailDto))]
        public HttpResponseMessage Get(Guid id)
        {
            var contactDetail = this.contactApplicationService.GetContactInfo(id);
            return contactDetail == null ? this.Request.CreateResponse(HttpStatusCode.NoContent) : this.Request.CreateResponse(HttpStatusCode.OK, contactDetail);
        }


        [HttpPost]
        public HttpResponseMessage Create(ContactInfoDetailDto contact)
        {
            try
            {
                this.contactApplicationService.CreateContactInfo(contact);
                return Request.CreateResponse(HttpStatusCode.Created, contact);
            }
            catch (ValidationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        [ResponseType(typeof(ContactInfoDetailDto))]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(ContactInfoDetailDto))]
        public HttpResponseMessage Update(ContactInfoDetailDto contact)
        {
            try
            {
                this.contactApplicationService.UpdateContactInfo(contact);
                return Request.CreateResponse(HttpStatusCode.OK, contact);
            }
            catch (ValidationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                this.contactApplicationService.DeleteContactInfo(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ValidationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}