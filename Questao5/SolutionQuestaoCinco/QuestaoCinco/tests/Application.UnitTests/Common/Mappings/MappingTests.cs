using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using QuestaoCinco.Application.Common.Interfaces;
using QuestaoCinco.Domain.Entities;
using NUnit.Framework;
using QuestaoCinco.Application.ContasCorrentes.Queries.GetContasCorrentesWithPagination;
using QuestaoCinco.Application.Movimentos.Queries.GetMovimentos;

namespace QuestaoCinco.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(Movimento), typeof(MovimentoDto))]
    [TestCase(typeof(ContaCorrente), typeof(ContaCorrenteDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
