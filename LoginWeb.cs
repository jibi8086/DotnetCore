public  IActionResult Index()
        {
            UserModel login = new UserModel();
            login.EmailAddress="Test";
            login.Username = "um";
            var test = Login(login,10);
            try
            {
                 var data1 = ProviderBase.PostAsync<long>($"api/login/SignUp", login, "");
                login.Username = "um";
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View();
        }
public async Task<ActionResult> Login(UserModel login, int? expireTime) {

            try
            {
                CookieOptions option = new CookieOptions();

                if (expireTime.HasValue)
                    option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
                else
                    option.Expires = DateTime.Now.AddMilliseconds(10);

                Response.Cookies.Append("token", "123456", option);
                //string token = this.Request.Cookies["at"]?.
                string token = Request.Cookies["token"];
                var data = await ProviderBase.PostAsync<long>($"api/login/SignUp", login, string.Empty);
                return Json(data);
            }
            catch (Exception)
            {

                throw;
            }
        }
