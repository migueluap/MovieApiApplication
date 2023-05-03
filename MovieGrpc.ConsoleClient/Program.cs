// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using ProtoDefinitions;


var message = new Empty() { };
var message2 = new IdRequest() { Id = "1" };

var channel = GrpcChannel.ForAddress("http://localhost:5256");
//http://localhost:5256
//https://localhost:7256

var client = new MoviesApi.MoviesApiClient(channel);

Console.WriteLine("Enter Employee ID....");
var inputStr = Console.ReadLine();

    var serverResponse = await client.GetAllAsync(message);

if (serverResponse.Success)
{
    serverResponse.Data.TryUnpack<showListResponse>(out var data);
    Console.WriteLine(data);

}
else
    Console.WriteLine(serverResponse.Exceptions);


Console.WriteLine("/************************/");


var serverResponseGetById = await client.GetByIdAsync(message2);

if (serverResponseGetById.Success)
{
    serverResponseGetById.Data.TryUnpack<showListResponse>(out var data2);
    Console.WriteLine(data2);

}
else
    Console.WriteLine(serverResponseGetById.Exceptions);




Console.WriteLine("Press any key to exit...");
Console.ReadKey();