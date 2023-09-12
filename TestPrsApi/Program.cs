
using System.Text.Json;

using TestPrsApi;

const string baseurl = "http://localhost:5555";

JsonSerializerOptions joptions = new JsonSerializerOptions() {
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true
};

HttpClient http = new HttpClient();

#region Getall
var jsonresponse = await GetUsersAsync(http, joptions);
var users = jsonresponse.DataReturned as IEnumerable<User>;

foreach(var u in users) {
    Console.WriteLine($"{u.Firstname} {u.Lastname}");
}

async Task<JsonResponse> GetUsersAsync(HttpClient http, JsonSerializerOptions jsonOptions) {
    HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"{baseurl}/api/users");
    HttpResponseMessage res = await http.SendAsync(req);
    Console.WriteLine($"Http ErrorCode is {res.StatusCode}");
    if(res.StatusCode != System.Net.HttpStatusCode.OK) {
    }
    var json = await res.Content.ReadAsStringAsync();
    var users = (IEnumerable<User>?)JsonSerializer.Deserialize(json, typeof(IEnumerable<User>), jsonOptions);
    if(users is null) {
        throw new Exception();
    }
    return new JsonResponse() {
        HttpStatusCode = (int) res.StatusCode,
        DataReturned = users
    };
}
#endregion

var user = new User() {
    Id = 1,
    Username = "sa", Password = "sa",
    Firstname = "System", Lastname = "Administrator",
    Phone = "911", Email = "administrator@system.com",
    IsReviewer = true, IsAdmin = true
};

await UpdateUser(user, joptions);

async Task<JsonResponse> UpdateUser(User user, JsonSerializerOptions options) {
    HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Put, $"{baseurl}/api/users/{user.Id}");
    var json = JsonSerializer.Serialize<User>(user, options);
    req.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
    HttpResponseMessage res = await http.SendAsync(req);
    Console.WriteLine($"HTTP StatusCode is {res.StatusCode}");
    return new JsonResponse() {
        HttpStatusCode = (int) res.StatusCode
    };
}