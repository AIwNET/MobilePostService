using MobilePostService.Models;
using MobilePostService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobilePostService.Controllers
{
    public class ParcelApiController : ApiController
    {
        private ParcelRepository _parcelRepo;

        public ParcelApiController()
        {
            _parcelRepo = new ParcelRepository();
        }

        // GET api/parcelapi
        public IEnumerable<Parcel> Get()
        {
            var parcels = _parcelRepo.GetAllParcels();
            return parcels;
        }

        // POST api/parcelapi
        public HttpResponseMessage Post(Parcel parlcel)
        {
            if (ModelState.IsValid)
            {
                parlcel.RegistrationDate = DateTime.Now;

                _parcelRepo.Add(parlcel);
                _parcelRepo.SaveChanges();

                var response = Request.CreateResponse<Parcel>(HttpStatusCode.Created, parlcel);
                string uri = Url.Route(null, new { id = parlcel.Id });
                response.Headers.Location = new Uri(Request.RequestUri, uri);
                return response;
            } 
            else
            {
                var response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                return response;
            }            
        }
    }
}
