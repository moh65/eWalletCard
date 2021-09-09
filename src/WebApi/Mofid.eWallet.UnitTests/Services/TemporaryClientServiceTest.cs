using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using Mofid.eWallet.Infra.Exceptions;
using Mofid.eWallet.Services;
using Mofid.eWallet.Services.Implementations;
using NSubstitute;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Mofid.eWallet.UnitTests.Services
{
	[TestClass]
    public class TemporaryClientServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public async Task ImportMustExceptionInputParameter_async()
        {
            int counter = 0;
            int updatecounter = 0;
            var moqTemporary = NSubstitute.Substitute.For<ITemporaryClientRepository>();
            moqTemporary.WhenForAnyArgs(x => x.AddAsync(Arg.Any<TemporaryClient>())).Do(x => counter++);
            moqTemporary.WhenForAnyArgs(x => x.UpdateAsync(Arg.Any<TemporaryClient>())).Do(x => updatecounter++);

            ITemporaryClientService temService = new TemporaryClientService(moqTemporary);
            await temService.Import(null);

            Assert.AreEqual(counter,0);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task ImportMustErrorClientsNull_async()
        {
            int counter = 0;
            int updatecounter = 0;
            var moqTemporary = NSubstitute.Substitute.For<ITemporaryClientRepository>();
            moqTemporary.WhenForAnyArgs(x => x.AddAsync(Arg.Any<TemporaryClient>())).Do(x => counter++);
            moqTemporary.WhenForAnyArgs(x => x.UpdateAsync(Arg.Any<TemporaryClient>())).Do(x => updatecounter++);

            var file =  Substitute.For<IFormFile>();

            ITemporaryClientService temService = new TemporaryClientService(moqTemporary);
            await temService.Import(file);

            
            var fileName = "test.xlsx";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("");
            writer.Flush();
            ms.Position = 0;
            file.OpenReadStream().ReturnsForAnyArgs(ms);
            file.FileName.ReturnsForAnyArgs(fileName);
            file.Length.ReturnsForAnyArgs(ms.Length);

            Assert.AreEqual(counter, 0);
        }


        [TestMethod]
        public async Task DeleteMustSuccess()
        {
            int conter = 0;
            var moqClientRepository = Substitute.For<ITemporaryClientRepository>();
            moqClientRepository.WhenForAnyArgs(x => x.DeleteById(Arg.Any<string>())).Do(x => conter++);
            

            var service = new TemporaryClientService(moqClientRepository);
            await service.Delete("4");

            Assert.AreEqual(conter, 1);

        }


        [TestMethod]
        public async Task ListMustSuccess()
        {
            
            var moqClientRepository = Substitute.For<ITemporaryClientRepository>();
             moqClientRepository.List(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(((long)0 , new System.Collections.Generic.List<TemporaryClient>()));


            var service = new TemporaryClientService(moqClientRepository);
            var result = await service.List(0 , 20 , string.Empty , string.Empty);

            Assert.AreEqual(0, result.Item1);
            Assert.AreEqual(0, result.Item2.Count);

        }

     



    }
}
