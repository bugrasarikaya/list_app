using AutoMapper;
using Microsoft.EntityFrameworkCore;
using list_api.Data;
using list_api.Repository.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using list_api.Services;
using list_api.Security;
using xUnitTests.Data;
namespace xUnitTests.TestSetups {
	public class RepositoryTestFixture {
		public ListApiDbContext Context { get; set; } = null!;
		public IDistributedCache Cache { get; set; } = null!;
		public IEncryptor Encryptor { get; set; } = null!;
		public IMapper Mapper { get; set; } = null!;
		public IMessageService Messager { get; set; } = null!;
		public RepositoryTestFixture() {
			DbContextOptions<ListApiDbContext> options = new DbContextOptionsBuilder<ListApiDbContext>().UseInMemoryDatabase(databaseName: "ListApiTestDb").Options;
			Context = new ListApiDbContext(options);
			Context.Database.EnsureCreated();
			Cache = new MemoryDistributedCache(Options.Create(new MemoryDistributedCacheOptions()));
			Encryptor = new SHA256Encryptor();
			Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
			Messager = new RabbitMQService(Options.Create(new RabbitMQConfiguration()));
			Feed.Database(Context);
		}
	}
}