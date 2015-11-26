using System;

namespace Products.Service
{
	public class LoginService
	{
		private const string TestUsername = "admin";
		private const string TestPassword = "admin";

		public LoginService ()
		{
		}

		public bool DoLogin(string username, string password)
		{
			return (username == TestUsername && password == TestPassword);
		}
	}
}

