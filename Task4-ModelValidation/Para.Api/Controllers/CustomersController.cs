using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema.Validators;
using Para.Schema;
using Microsoft.EntityFrameworkCore;
using Para.Data.Context;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using System.Linq.Expressions;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ParaDbContext _dbContext;
        private readonly IUnitOfWork unitOfWork;

        public CustomersController(IMediator mediator, ParaDbContext dbContext, IUnitOfWork unitOfWork)
        {
            this.mediator = mediator;
            _dbContext = dbContext;
            this.unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CustomerResponse>>> Get()
        {
            var operation = new GetAllCustomerQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerResponse>> Get([FromRoute] long customerId)
        {
            var operation = new GetCustomerByIdQuery(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerResponse>> Post([FromBody] CustomerRequest value)
        {

            var validator = new CustomerValidator(_dbContext);
            var validationResult = validator.Validate(value);

            if (!validationResult.IsValid)
            {
                var errorResponse = new ApiResponse<CustomerResponse>(false)
                {
                    Message = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                };
                return errorResponse;
            }

            var operation = new CreateCustomerCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerId}")]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerRequest value)
        {
            var operation = new UpdateCustomerCommand(customerId, value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerId}")]
        public async Task<ApiResponse> Delete(long customerId)
        {
            var operation = new DeleteCustomerCommand(customerId);
            var result = await mediator.Send(operation);
            return result;
        }
        //Include kullanarak modelleri birbirine ekledik.
        [HttpGet("with-addresses-and-phones")]
        public async Task<ApiResponse<List<Customer>>> Include()
        {
            var customers = await unitOfWork.CustomerRepository.Include(
                x => x.CustomerAddresses,
                x => x.CustomerPhones
            );

            return new ApiResponse<List<Customer>>(customers);
        }
        //Where kullanım örneği
        [HttpGet("where-name")]
        public async Task<ApiResponse<List<Customer>>> Where(string name)
        {
            Expression<Func<Customer, bool>> a = x => x.FirstName == name;
            var customers = await unitOfWork.CustomerRepository.Where(a);

            return new ApiResponse<List<Customer>>(customers);
        }
    }
}