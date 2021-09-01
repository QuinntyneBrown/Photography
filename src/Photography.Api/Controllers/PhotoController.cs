using System.Net;
using System.Threading.Tasks;
using Photography.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Photography.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController
    {
        private readonly IMediator _mediator;

        public PhotoController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<ActionResult<UploadPhoto.Response>> Save()
            => await _mediator.Send(new UploadPhoto.Request());

        [AllowAnonymous]
        [HttpGet("serve/{photoId}", Name = "ServePhotoByIdRoute")]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(FileContentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Serve([FromRoute] ServePhotoById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Photo == null)
                return new NotFoundObjectResult(null);

            return new FileContentResult(response.Photo.Bytes, response.Photo.ContentType);
        }

        [AllowAnonymous]
        [HttpGet("serve/name/{name}", Name = "ServePhotoByNameRoute")]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(FileContentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ServeByName([FromRoute] ServePhotoByName.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Photo == null)
                return new NotFoundObjectResult(null);

            return new FileContentResult(response.Photo.Bytes, response.Photo.ContentType);
        }        

        [HttpGet("{photoId}", Name = "GetPhotoByIdRoute")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPhotoById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPhotoById.Response>> GetById([FromRoute]GetPhotoById.Request request)
        {
            var response = await _mediator.Send(request);
        
            if (response.Photo == null)
            {
                return new NotFoundObjectResult(request.PhotoId);
            }
        
            return response;
        }
        
        [HttpGet(Name = "GetPhotosRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPhotos.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPhotos.Response>> Get()
            => await _mediator.Send(new GetPhotos.Request());
        
        [HttpPost(Name = "CreatePhotoRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatePhoto.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreatePhoto.Response>> Create([FromBody]CreatePhoto.Request request)
            => await _mediator.Send(request);
        
        [HttpGet("page/{pageSize}/{index}", Name = "GetPhotosPageRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPhotosPage.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetPhotosPage.Response>> Page([FromRoute]GetPhotosPage.Request request)
            => await _mediator.Send(request);
        
        [HttpPut(Name = "UpdatePhotoRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdatePhoto.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdatePhoto.Response>> Update([FromBody]UpdatePhoto.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{photoId}", Name = "RemovePhotoRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemovePhoto.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemovePhoto.Response>> Remove([FromRoute]RemovePhoto.Request request)
            => await _mediator.Send(request);
        
    }
}
