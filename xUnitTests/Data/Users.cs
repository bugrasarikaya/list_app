using list_api.Data;
using list_api.Models;
namespace xUnitTests.Data {
	public static class Users {
		public static void AddUsers(this ListApiDbContext context) {
			context.Users.AddRange(
				new User { ID = 1, IDRole = 2, Name = "ashrivers", Password = "997ab44b5e9090c57ec59cef493a480899b38e3c4e1ce2000be1c5be61cdfc95" },
				new User { ID = 2, IDRole = 2, Name = "heatherpoe", Password = "8daf9e3bcb152a7ff440fc08f866486761144c1339a1e0327e4a621ddc7f8b09" },
				new User { ID = 3, IDRole = 2, Name = "isaacabrams", Password = "e2e0faf312f0f2ce748562f546a87a1be0487ab7996701065ab615af85195d5f" },
				new User { ID = 4, IDRole = 1, Name = "sebastianlacroix", Password = "e2e0faf312f0f2ce748562f546a87a1be0487ab7996701065ab615af85195d5f" },
				new User { ID = 5, IDRole = 2, Name = "smilingjack", Password = "6ff4b8592f45a6c7db8241beb96604af427b189fedcac83551785ef43cb7df51" },
				new User { ID = 6, IDRole = 2, Name = "theresevoerman", Password = "6572d7f8c105a31e9275183ed662eded37416131ee0e73f9119141c8dd36924c" },
				new User { ID = 7, IDRole = 1, Name = "maximillianstrauss", Password = "615641a668ac162a061cd0fb6bf3eb23001341a2b830a049d30e06f21b36616c" }
			);
		}
	}
}