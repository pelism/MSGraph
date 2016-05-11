using System;
using System.Net.Http.Headers;

namespace Pelism.MSGraph.Helpers {
    public static class GraphSettings {
        public static string AzureADAuthority = @"https://login.microsoftonline.com/common";
        public static string LogoutAuthority = @"https://login.microsoftonline.com/common/oauth2/logout?post_logout_redirect_uri=";
        public static string O365UnifiedAPIResource = @"https://graph.microsoft.com/";

        public static string MessageUrl = @"https://graph.microsoft.com/v1.0/me/messages";
        public static string MessageAttachments = @"https://graph.microsoft.com/v1.0/me/messages/{0}/attachments";
        public static string SendMessageUrl = @"https://graph.microsoft.com/v1.0/me/{0}/microsoft.graph.sendmail";
        public static string GetMeUrl = @"https://graph.microsoft.com/v1.0/me";
    }
}