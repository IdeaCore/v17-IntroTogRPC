using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcService.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerDataModel> GetCustomerInfo(CustomerFindModel request, ServerCallContext context)
        {
            var model = new CustomerDataModel();

            if(request.UserId == 1)
            {
                model.FirstName = "Mohamad";
                model.LastName = "Lawand";
            } 
            else if(request.UserId == 2)
            {
                model.FirstName = "Homer";
                model.LastName = "Simpsons";
            }
            else if(request.UserId == 3)
            {
                model.FirstName = "Bruce";
                model.LastName = "Wayne";
            }
            else 
            {
                model.FirstName = "Mickey";
                model.LastName = "Mouse";
            }

            return Task.FromResult(model);
        }

        public override async Task GetAllCustomers(AllCustomerModel request, 
                        IServerStreamWriter<CustomerDataModel> responseStream, 
                        ServerCallContext context)
        {
            var allCustomers = new List<CustomerDataModel>();

            var c1 = new CustomerDataModel();
            c1.FirstName = "Bruce";
            c1.LastName = "Wayne";
            allCustomers.Add(c1);

            var c2 = new CustomerDataModel();
            c2.FirstName = "Homer";
            c2.LastName = "Simpsons";
            allCustomers.Add(c2);

            var c3 = new CustomerDataModel();
            c3.FirstName = "Tony";
            c3.LastName = "Stark";
            allCustomers.Add(c3);

            var c4 = new CustomerDataModel();
            c4.FirstName = "Mickey";
            c4.LastName = "Mouse";
            allCustomers.Add(c4);

            foreach(var item in allCustomers)
            {
                await responseStream.WriteAsync(item);

                await Task.Delay(1000);
            }
        }
    }
}