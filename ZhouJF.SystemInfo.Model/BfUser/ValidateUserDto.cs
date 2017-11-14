namespace YHPT.SystemInfo.Model.BfUser
{
    /// <summary>
    ///     用户登录参数
    /// </summary>
    public class ValidateUserDto
    {
        /// <summary>
        ///     用户登陆名(UserCode)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     用户密码（ 密文）
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     是否记住登录名
        /// </summary>
        public bool RemberUserName { get; set; }

        /// <summary>
        ///     登陆通过后 跳转的目标地址
        /// </summary>
        public string RedirectUrl { get; set; }
    }
}