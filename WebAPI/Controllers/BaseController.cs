using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]
    public abstract class BaseController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        protected IRepositoryWrapper Repository
        {
            get
            {
                return _repository ??= HttpContext.RequestServices.GetService<IRepositoryWrapper>();
            }
        }

        protected IMapper Mapper
        {
            get
            {
                return _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
            }
        }
    }
}
