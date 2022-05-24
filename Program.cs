using System.Data.SqlClient;
using ToDOList2.Interfaces;
using ToDOList2.MSSQLRepositories;
using ToDOList2.XMLRepositories;
using ToDOList2.Service;
using ToDOList2.GraphQL.GraphQLSchema;
using GraphQL.SystemTextJson;
using GraphQL.Server;
using ToDOList2.GraphQL.GraphQLQueries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IRepositoryResolver, RepositoryResolver>();

builder.Services.AddTransient<System.Data.IDbConnection>(options => new SqlConnection("Server=DESKTOP-56K1OB0;Database=ToDoList;Trusted_Connection=True;MultipleActiveResultSets=true"));
builder.Services.AddTransient<MSSQLTaskRepository>();
builder.Services.AddTransient<MSSQLCategoryRepository>();


builder.Services.AddTransient<XMLTaskRepository>();
builder.Services.AddTransient<XMLCategoryRepository>();

builder.Services.AddScoped<AppQueries>();
builder.Services.AddScoped<AppSchema>();

builder.Services.AddGraphQL()
        .AddSystemTextJson()
        .AddGraphTypes(typeof(AppSchema), ServiceLifetime.Scoped);


var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseGraphQL<AppSchema>();
app.UseGraphQLAltair(options: new GraphQL.Server.Ui.Altair.AltairOptions());

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
