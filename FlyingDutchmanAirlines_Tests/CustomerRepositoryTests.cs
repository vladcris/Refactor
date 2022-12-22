using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.RepositoryLayer.Models;
using FlyingDutchmanAirlines.RepositoryLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines_Tests;

[TestClass]
public class CustomerRepositoryTests
{
    private FlyingDutchmanAirlinesContext? _context;
    private CustomerRepository? _repository;

    [TestMethod]
    public async Task CreateCustomer_Success()
    {
        bool result = await _repository!.CreateCustomer("Andrei");
        Assert.IsTrue(result);

        var name = _context!.Customers.Single(n => n.Name == "Andrei");
        Assert.AreEqual("Andrei", name.Name);
    }

    [TestMethod]
    public async Task CreateCustomer_Failure_NameIsEmptyString()
    {
        bool result = await _repository!.CreateCustomer(string.Empty);

        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow('!')]
    [DataRow('*')]
    [DataRow('$')]
    [DataRow('@')]
    [DataRow('#')]
    public async Task CreateCustomer_Failure_NameContainsInvalidCharacters(char invalidCharacter)
    {
        bool result = await _repository!.CreateCustomer("Vlad" + invalidCharacter);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task CreateCustomer_Failure_DatabaseAccessError()
    {
        var repository = new CustomerRepository(null);

        bool result = await repository.CreateCustomer("Andrei");

        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task GetCustomerByName_Success()
    {
        var customer = await _repository!.GetCustomerByName("Linus Torvalds");

        var customerDb = await _context!.Customers.FirstAsync();

        bool AreEqual = customer == customerDb;

        Assert.IsTrue(AreEqual);

        Assert.AreEqual(customerDb, customer);
    }

    [TestMethod]
    [DataRow("!")]
    [DataRow("*")]
    [DataRow("$")]
    [DataRow("@")]
    [DataRow("#")]
    [ExpectedException(typeof(CustomerNotFoundException))]
    public async Task GetCustomerByName_Failure_InvalidName(string name)
    {
        await _repository!.GetCustomerByName(name);
    }


    [TestInitialize]
    public async Task TestInitialize()
    {
        DbContextOptions<FlyingDutchmanAirlinesContext> dboptions = new 
        DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
        .UseInMemoryDatabase("Filename=:memory:").Options;
        _context = new FlyingDutchmanAirlinesContext(dboptions);

        var customer = new Customer("Linus Torvalds");
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        _repository = new CustomerRepository(_context);
        Assert.IsNotNull(_repository);
    }

}