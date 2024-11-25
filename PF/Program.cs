using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PF;
using PF.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ILojaRepositorio, LojaRepositorio>();
builder.Services.AddTransient<ICarrinhoRepositorio, CarrinhoRepositorio>();
builder.Services.AddTransient<IPedidoUsuarioRepositorio, PedidoUsuarioRepositorio>();
builder.Services.AddTransient<IEstoqueRepositorio, EstoqueRepositorio>();
builder.Services.AddTransient<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddTransient<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddTransient<IFileService, FileService>();

var app = builder.Build();

/////////////////////////// Descomente o código CreateScope() para gerar o usuário administrador.
/////////////////////////// As informações de Login estão no /Data/DbSeeder


//using (var scope = app.Services.CreateScope())
//{
//    await DbSeeder.SeedDefaultData(scope.ServiceProvider);
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
