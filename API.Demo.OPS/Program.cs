using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddResponseCompression();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    //option.SerializerSettings.MaxDepth = 2;
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;// tham chiếu vòng lặp 
    options.SerializerSettings.MetadataPropertyHandling = Newtonsoft.Json.MetadataPropertyHandling.Ignore;
    //options.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;//
    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Unspecified;//Bỏ time local 
});
builder.Services.AddMvcCore().AddApiExplorer();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    //options.SuppressConsumesConstraintForFormFileParameters = true;//Multipart/form-data request inference
    options.SuppressInferBindingSourcesForParameters = true; //Disable inference rules

    //options.SuppressModelStateInvalidFilter = true;
    //options.SuppressMapClientErrors = true; //Disable ProblemDetails
    //options.ClientErrorMapping[StatusCodes.Status404NotFound].Link = "https://httpstatuses.com/404";
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
       builder =>
       {
           //builder.WithOrigins("http://example.com", "http://www.contoso.com");
           builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().SetPreflightMaxAge(TimeSpan.FromSeconds(300));
       });
});
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API.Demo.OPS", Version = "v1" });
}).AddSwaggerGenNewtonsoftSupport();

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddOptions();
builder.Services.AddMvc();// Add framework services.
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(c =>
    {
        //you can opt into exposing JSON in the 2.0 format instead
        c.SerializeAsV2 = false;// Enable middleware to serve generated Swagger as a JSON endpoint
        c.RouteTemplate = "api-docs/{documentName}/swagger.json";
    });
    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.    
    app.UseSwaggerUI(c =>
    {
        //c.RoutePrefix = "swagger"; //To serve the Swagger UI at the app's root
        //c.SwaggerEndpoint("../swagger/v1/swagger.json", "API.BDSGiaPhu.OPS v1");
        c.SwaggerEndpoint("/api-docs/v1/swagger.json", "API.Demo.OPS v1");

        c.DefaultModelExpandDepth(2);
        c.DefaultModelRendering(ModelRendering.Model);
        c.DefaultModelsExpandDepth(-1);
        c.DisplayOperationId();
        c.DisplayRequestDuration();
        c.DocExpansion(DocExpansion.None);
        c.EnableDeepLinking();
        c.EnableFilter();
        //c.MaxDisplayedTags(5);
        c.ShowExtensions();
        c.ShowCommonExtensions();
        c.EnableValidator();
        //c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Head);
        c.UseRequestInterceptor("(request) => { return request; }");
        c.UseResponseInterceptor("(response) => { return response; }");
    });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
