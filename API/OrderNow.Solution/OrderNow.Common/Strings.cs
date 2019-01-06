using System;
using System.Collections.Generic;

namespace OrderNow.Common
{
    public class Strings
    {

        public const string JWT = "JWT";
        public const string userID = "UserID";
        public const string userName = "Name";
        public const string userRole = "Role";
        public const string TenantId = "TenantId";
            

        public static readonly List<string> SupportedLanguages = new List<string> { "ar-eg", "en-US" };
        public const string DefaultLanguage = "en-US";

        public const int BackgroundId = 3;
        public const int FeaturesBackgroundId = 1;


        public const long DefaultTemplateId = 1;

        public static DateTime? CurrentDateTime { get; set; }
    }
}
