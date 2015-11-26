using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Products
{
	[Activity (Label = "Products", MainLauncher = true, Icon = "@mipmap/icon")]
	public class LoginActivity : Activity
	{
		private Service.LoginService LoginService;
		private EditText UsernameInput;
		private EditText PasswordInput;
		private Button LoginButton;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Login);

			// Services
			LoginService = new Service.LoginService ();

			// UI elements
			UsernameInput = FindViewById<EditText> (Resource.Id.loginInputUsername);
			PasswordInput = FindViewById<EditText> (Resource.Id.loginInputPassword);
			LoginButton = FindViewById<Button> (Resource.Id.loginBtn);

			// Login button click event
			LoginButton.Click += OnLoginButtonClick;
		}

		private void OnLoginButtonClick (object sender, System.EventArgs e)
		{
			string username = UsernameInput.Text;
			string password = PasswordInput.Text;

			Toast message;

			if (LoginService.DoLogin (username, password))
			{
				message = Toast.MakeText (this, Resource.String.login_message_success, ToastLength.Long);

				Intent productsIntent = new Intent (this, typeof(ProductsActivity));
				StartActivity (productsIntent);
			}
			else
			{
				message = Toast.MakeText (this, Resource.String.login_message_denied, ToastLength.Long);
			}

			message.Show ();
		}
	}
}


