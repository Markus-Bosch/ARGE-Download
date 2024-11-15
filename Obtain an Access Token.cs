using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class TokenHelper
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task<string> GetAccessTokenAsync(string tenantId, string clientId, string clientSecret)
    {
        var url = $"https://login.microsoftonline.com/0ae51e19-07c8-4e4b-bb6d-648ee58410f4/oauth2/v2.0/token";
        var body = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("e6793811-2fd0-4667-bafa-fb7cfb3d6bf1", clientId),
            new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default"),
            new KeyValuePair<string, string>("8f490e16-612e-4eb4-bd33-f6feaaafbcd0", clientSecret),
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        });

        var response = await client.PostAsync(url, body);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(responseBody);
        return json["access_token"].ToString();
    }
}