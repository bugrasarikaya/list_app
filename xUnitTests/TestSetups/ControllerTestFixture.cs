using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using list_api.Data;
using list_api.Repository.Common;
using list_api.Services;
using list_api.Security;
using xUnitTests.Data;
using list_api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
namespace xUnitTests.TestSetups {
	public class ControllerTestFixture {
		public HttpClient Client { get; set; } = null!;
		public ListApiDbContext Context { get; set; } = null!;
		public IDistributedCache Cache { get; set; } = null!;
		public IEncryptor Encryptor { get; set; } = null!;
		public IMapper Mapper { get; set; } = null!;
		public IMessageService Messager { get; set; } = null!;
		public ControllerTestFixture() {
			var appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(host => {
				host.ConfigureServices(services => {
					var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ListApiDbContext>));
					services.Remove(descriptor!);
					services.AddDbContext<ListApiDbContext>(options => { options.UseInMemoryDatabase(databaseName: "ListApiTestDb"); });
				});
			});
			Client = appFactory.CreateClient();
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