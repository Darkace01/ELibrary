namespace ELibrary.EmailHandler;

public class EmailTemplateBuilder
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _config;
    public EmailTemplateBuilder(IConfiguration config, IWebHostEnvironment env)
    {
        _env = env;
        _config = config;
    }

    public string BuildPasswordResetTemplate(string firstName, string emailAddress, string password, string loginUrl)
    {

        var _rootPath = _env.ContentRootPath;
        var templatePath = Path.Combine(_rootPath, $"EmailTemplates\\ForgotPassword.html");
        var body = string.Empty;
        using (StreamReader reader = new StreamReader(templatePath))
        {
            body = reader.ReadToEnd();
        }
        var applicationName = _config["AppSettings:ApplicationName"];
        var applicationAddress = _config["AppSettings:ApplicationAddress"];
        var customerCareEmail = _config["AppSettings:CustomerCareEmail"];
        var websiteContactUrl = _config["AppSettings:ApplicationContactPage"];
        body = body
                .Replace("{firstName}", firstName)
                .Replace("{emailAddress}", emailAddress)
                .Replace("{password}", password)
                .Replace("{loginUrl}", loginUrl)
                .Replace("{applicationName}", applicationName)
                .Replace("{applicationAddress}", applicationAddress)
                .Replace("{customerCareEmail}", customerCareEmail)
                .Replace("{websiteContactUrl}", websiteContactUrl);

        return body;
    }

    public string BuildAccountConfirmationTemplate(string companyName, string emailAddress, string password, string profileUrl)
    {
        var _rootPath = _env.ContentRootPath;
        var templatePath = Path.Combine(_rootPath, $"EmailTemplates\\AccountConfirmation.html");
        var body = string.Empty;
        using (StreamReader reader = new StreamReader(templatePath))
        {
            body = reader.ReadToEnd();
        }
        var applicationName = _config["AppSettings:ApplicationName"];
        var applicationAddress = _config["AppSettings:ApplicationAddress"];
        var customerCareEmail = _config["AppSettings:CustomerCareEmail"];
        var websiteContactUrl = _config["AppSettings:ApplicationContactPage"];

        body = body
                .Replace("{companyName}", companyName)
                .Replace("{emailAddress}", emailAddress)
                .Replace("{password}", password)
                .Replace("{profileUrl}", profileUrl)
                .Replace("{applicationName}", applicationName)
                .Replace("{applicationAddress}", applicationAddress)
                .Replace("{customerCareEmail}", customerCareEmail)
                .Replace("{websiteContactUrl}", websiteContactUrl);

        return body;
    }
}
