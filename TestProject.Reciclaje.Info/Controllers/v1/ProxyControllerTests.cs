using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Net.Http.Json;

namespace Reciclaje.Info.Server.Controllers.Tests
{
    [TestClass()]
    public class ProxyControllerTests
    {
        private  IConfiguration _configuration;

        
        [TestInitialize]
        public void Initialize()
        {
            _configuration = new ConfigurationBuilder()
          .AddJsonFile("appsettings.Development.json")
          .Build();
        }

        [TestMethod()]
        public async Task GetPuntosLimpiosFijosAsyncTest()
        {
            ProxyController proxy = new ProxyController(_configuration);
            var result = await proxy.GetPuntosLimpiosAsync(Shared.Types.PuntosLimpiosType.Fijos);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<GeoAtomDto>));
            Assert.IsTrue(result.Value?.entries?.Count() > 0);


        }
        [TestMethod()]
        public async Task GetPuntosLimpiosMovilesAsyncTest()
        {
            ProxyController proxy = new ProxyController(_configuration);
            var result = await proxy.GetPuntosLimpiosAsync(Shared.Types.PuntosLimpiosType.Moviles);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<GeoAtomDto>));
            Assert.IsTrue(result.Value?.entries?.Count() > 0);
        }
        [TestMethod()]
        public async  Task GetPuntosLimpiosProximidadAsyncTest()
        {
            ProxyController proxy = new ProxyController(_configuration);
            var result = await proxy.GetPuntosLimpiosAsync(Shared.Types.PuntosLimpiosType.Proximidad);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<GeoAtomDto>));
            Assert.IsTrue(result.Value?.entries?.Count() > 0);

        }
        [TestMethod()]
        public async Task GetContenedoresFijosAsyncTest()
        {
            ProxyController proxy = new ProxyController(_configuration);
            var result = await proxy.GetContendoresAsync(Shared.Types.ContenedorType.Ropa);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<GeoAtomDto>));
            Assert.IsTrue(result.Value?.entries?.Count() > 0);
        }
        [TestMethod()]
        public async Task GetContenedoresAceiteUsadoAsyncTest()
        {
            ProxyController proxy = new ProxyController(_configuration);
            var result =  await proxy.GetContendoresAsync(Shared.Types.ContenedorType.AceiteUsado);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<GeoAtomDto>));
            Assert.IsTrue(result.Value?.entries?.Count() > 0);
        }
        [TestMethod()]
        public async  Task GetContenedoresPilasAsyncTest()
        {
            ProxyController proxy = new ProxyController(_configuration);
            var result = await proxy.GetContendoresAsync(Shared.Types.ContenedorType.PilasMarquesinas);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<GeoAtomDto>));
            Assert.IsTrue(result.Value?.entries?.Count() > 0);



        }

        [TestMethod()]
        public async Task GetEquipamientoOKAsyncTest()
        {
            ProxyController proxy = new ProxyController(_configuration);
            var result = await proxy.GetEquipamientoAsync("botellas");
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value?.Equipamientos?.Count() > 0);
            
         }

        [TestMethod()]
        public async Task GetEquipamientoKOAsyncTest()
        {
            ProxyController proxy = new ProxyController(_configuration);
            var result = await proxy.GetEquipamientoAsync("ASAÑLKADL");
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value?.Equipamientos?.Count() == 0);
            


        }


    }
}