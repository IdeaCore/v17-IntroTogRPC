using System;
using Grpc.Net.Client;
using Grpc.Core;
using System.Threading.Tasks;

namespace GrpcClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello gRPC from Mohamad!");

            // channel to communicate with the service
            var channel = GrpcChannel.ForAddress("http://localhost:5000");

            var customerClient = new Customer.CustomerClient(channel);

            var result = await customerClient.GetCustomerInfoAsync(new CustomerFindModel() {
                UserId = 8
            });

            Console.WriteLine($"First Name: {result.FirstName} \nLastName: {result.LastName}");

            var allCustomers = customerClient.GetAllCustomers(new AllCustomerModel());

            await foreach(var customer in allCustomers.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"{customer.FirstName} {customer.LastName}");
            }
        }
    }
}
