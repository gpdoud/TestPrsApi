using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPrsApi;

public class User {

    public int Id { get; set; } = 0;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string? Phone { get; set; } = null;
    public string? Email { get; set; } = null;
    public bool IsReviewer { get; set; } = false;
    public bool IsAdmin { get; set; } = false;

}