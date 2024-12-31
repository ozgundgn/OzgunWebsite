using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }
    }
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while initialising the database");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            //var administratorRole = new IdentityRole(Roles.Administrator);

            //if (_roleManager.Roles.All(r => r.Name == administratorRole.Name))
            //{
            //    await _roleManager.CreateAsync(administratorRole);
            //}

            //var administrator = new ApplicationUser { UserName = "ozgun.dogan", Email = "ozgundgn0@gmail.com",  };

            //if (_userManager.Users.All(u => u.UserName != administrator.UserName ))
            //{
            //    await _userManager.CreateAsync(administrator,);
            //    if (!string.IsNullOrEmpty(administratorRole.Name))
            //    {
            //        await _userManager.AddToRoleAsync(administrator, administratorRole.Name);
            //    }
            //}

            // Default data
            // Seed, if necessary
            if (!_context.TodoLists.Any())
            {
                _context.TodoLists.Add(new TodoList
                {
                    Title = "Todo List",
                    Description = "333333",
                    Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
                });

                await _context.SaveChangesAsync();
            }

            if (!_context.Blogs.Any())
            {
                var cookiePath = Path.Combine("ClientApp/public/images", "netcore-cookie.png");
                var cookieImage = File.ReadAllBytes(cookiePath);

                var slavePath = Path.Combine("ClientApp/public/images", "slave.png");
                var slaveImage = File.ReadAllBytes(slavePath);

                var pubsubPath = Path.Combine("ClientApp/public/images", "pubsub.png");
                var pubsubImage = File.ReadAllBytes(pubsubPath);

                _context.Blogs.AddRange(new List<Blog>() {
         new Blog {
           Title = ".NET CORE’DA COOKIE SCHEME ile ASPNET.CORE.IDENTITY KULLANMADAN AUTHENTICATION VE ROLE-BASED, CLAIM-BASED AUTHORIZATION",
             Content = "Bu yazıda Asp.Net Core’da her uygulamanın gereksinimi olan kimlik doğrulama ve yetkilendirmenin mantığı ve işleyişinden biraz bahsetmek istiyorum. Asp.Net Core projelerinde default olarak gelen dosyalar, classlar, klasörler, Nuget Manager’den indirdiğimiz ve işimizi kolayca hallettiğimiz paketlerle bazı olayları anlamadan geçip gitmiş olabiliyoruz (en azından kendi adıma). Yetkilendirme ve Kimlik doğrulama yönetimi yine paket haline gelmiş olarak kolayca yapılmaya devam edilmekte. Örnek olarak Asp.Net Core Identity kullanarak proje içerisine kullanıcı kaydı, kullanıcı doğrulaması, rol tanımlamaları, token işlemleri yapılması için hazır controllerlar, sayfalar mevcuttur.",
             CreatedDate = new DateTime(2021, 12, 6),
             LastUpdatedDate = new DateTime(2021, 12, 6),
             Image = cookieImage,
             Link = "https://medium.com/@ozgundgn0/net-coreda-cookie-scheme-ile-aspnet-core-identity-e9e7f22f4f20",
             BlogParts = new List < BlogPart > () {
               new BlogPart() {
                   PartNo = 1,
                     Text = "<p>Bu yazıda Asp.Net Core’da her uygulamanın gereksinimi olan kimlik doğrulama ve yetkilendirmenin mantığı ve işleyişinden biraz bahsetmek istiyorum. Asp.Net Core projelerinde default olarak gelen dosyalar, classlar, klasörler, Nuget Manager’den indirdiğimiz ve işimizi kolayca hallettiğimiz paketlerle bazı olayları anlamadan geçip gitmiş olabiliyoruz (en azından kendi adıma). Yetkilendirme ve Kimlik doğrulama yönetimi yine paket haline gelmiş olarak kolayca yapılmaya devam edilmekte. Örnek olarak Asp.Net Core Identity kullanarak proje içerisine kullanıcı kaydı, kullanıcı doğrulaması, rol tanımlamaları, token işlemleri yapılması için hazır controllerlar, sayfalar mevcuttur.<br/><br/>Peki bu bir çok işlevi gören paketleri projeye yüklemeden kendimiz authentication ve authorization yapmak istersek nasıl yapabiliriz?<br/><br/>Öncelikle authentication ve authorization tanımlarını yaparak başlayabiliriz.<br/><br/><b>Authentication:</b> Kullanıcı kimliğini doğrulama işlemidir.<br/><br/> <b>Authorization</b>: Kullanıcın hangi eylemlere izninin olup olmadığıyla ilgilidir. Örneğin bir kullanıcının yeni bir rol tanımlama yetkisi varken bir diğerinin olmayabilir.</p> <br/><br/><br/><p>\r\nAuhtentication işlemini şemalar tanımlayarak iki türlü yapabiliriz;\r\n\r\nCookie şeması kullanılarak; kullanıcı bilgileri cookie’ den alır.\r\nJwt (Json Web Token) Bearer şeması kullanılarak; kullanıcı bilgisini tokenden alır.\r\nKullanıcı bilgileriyle bir token oluştururuz ve bu tokeni Http isteğiyle Header bilgisine “Bearer alinantoken“ şeklinde göndererek token gerektiren işlemlere erişim sağlayabiliriz.\r\nAuthentication örneklerine geçmeden önce Claims, ClaimsIdentity ve ClaimsPrincipal kavramlarından bahsedelim.\r\n\r\n\r\nClaims : Kimlik tanımlamaları için kullanılırlar. Kimlik bilgilerini tutan sınıftır. Kullanıcının adı, soyadı, email, rolleri gibi kişiye ait bilgileri tutmak için claimler oluşturulur. Yetkilendirme için ise Claim Tipi “Role” olan Claim’ler kullanılmaktadır.\r\n\r\nClaimsIdentity: Oluşturulan claim’leri tuttuğumuz sınıftır.\r\n\r\nClaimsPrincipal: Oluşturan ClaimsIdentity’yi kaydettiğimiz sınıftır. Bu sınıf HttpContext.User ın oluştuğu sınıftır. Yani kayıtlı olan Kullanıcı bilgileri aslında ClaimsPrincipal’dır. Kimlik bilgileri ve rol tanımlamalarını bu sınıf aracılığıyla kaydedip, okuruz.\r\n\r\nÖrneğimize bir Visual Studio’da Asp.Net Core projesi oluşturarak başlayalım.\r\n\r\nŞimdi appsettings.json dosyasında SqlServer connection stringimizi tanımlayalım. Burada tablolar oluşturacağız.</p>",
                     CodeBlocks = new List < CodeBlock > () {
                       new CodeBlock() {
                         CodeNo = 1,
                           Code = "{\r\n  \"Logging\": {\r\n    \"LogLevel\": {\r\n      \"Default\": \"Information\",\r\n      \"Microsoft\": \"Warning\",\r\n      \"Microsoft.Hosting.Lifetime\": \"Information\"\r\n    }\r\n  },\r\n  \"AllowedHosts\": \"*\",\r\n  \"ConnectionStrings\": {\r\n    \"LocalDb\": \"Server=OZGUN; Database=ForPractice; Trusted_Connection=true\"\r\n  }\r\n}"
                       },
                     }
                 },
                 new BlogPart() {
                   PartNo = 2,
                     Text = "Startup.cs doyasında bazı işlemler yapmamız gerekiyor. ConfigureServices metodu içerisine",
                     CodeBlocks = new List < CodeBlock > () {
                       new CodeBlock() {
                         CodeNo = 2,
                           Code = "   public void ConfigureServices(IServiceCollection services)\r\n        {\r\n            services.AddControllersWithViews();\r\n            services.AddDbContext<ApplicationContext>(options=>options.UseSqlServer(Configuration.GetConnectionString(\"LocalDb\")));\r\n            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)\r\n                .AddCookie();\r\n        }"
                       },
                     }
                 },
                 new BlogPart() {
                   PartNo = 3,
                     Text = "AddAuthentication içerisine tanımladığımız şema default olarak geçerli olacak olan şemadır. (Birden fazla şema tanımlaması yapılabiliyor. Bu yüzden burada default olarak tanımlanmış olan şema herhangi biri kullanılmadığında çalıştırması gereken olarak kabul ediliyor.)\r\n\r\nBirazdan oluşturacağımız ApplicationContext sınıfını db yolunu belirterek ekliyoruz.\r\n\r\nStartup.cs de yine Configure metodu içerisin tanımlamamız gereken yerler var;",
                     CodeBlocks = new List < CodeBlock > () {
                       new CodeBlock() {
                         CodeNo = 3,
                           Code = " public void Configure(IApplicationBuilder app, IWebHostEnvironment env)\r\n        {\r\n            if (env.IsDevelopment())\r\n            {\r\n                app.UseDeveloperExceptionPage();\r\n            }\r\n            else\r\n            {\r\n                app.UseExceptionHandler(\"/Home/Error\");\r\n                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.\r\n                app.UseHsts();\r\n            }\r\n            app.UseHttpsRedirection();\r\n            app.UseStaticFiles();\r\n\r\n            app.UseRouting();\r\n\r\n            app.UseCookiePolicy();\r\n            app.UseAuthentication();\r\n            app.UseAuthorization();\r\n\r\n            app.UseEndpoints(endpoints =>\r\n            {\r\n                endpoints.MapControllerRoute(\r\n                    name: \"default\",\r\n                    pattern: \"{controller=Home}/{action=Login}/{id?}\");\r\n            });\r\n        }"
                       },
                     }
                 },
                 new BlogPart() {
                   PartNo = 4,
                     Text = "Burada UseAuthentication ve UseAuthorization middlewarelerini ekledik. Yalnız dikkat edilmesi gereken nokta UseAuhtentication ‘ın UseAuthorization’dan önce gelmesi . Çünkü önce kimlik doğrulaması yapıp ardından o kimliğe olan yetkileri kontrol etmemiz gerekir (mantıklı:)). Yine UseAuthentication’ın UseRouting in altında olması (önce yönlendirmeler yapılabilmeli), UseEndpoints’in üstünde olması gerekir (endpointlere ulaşmadan önce kimlik doğrulama işleminden geçmiş olması gerekir).\r\n\r\nBunlar haricinde herhangi bir doğrulanmış kullanıcı gerekli olan middleware bulunuyorsa o middlewareden önce UseAuhtentication middleware i kullanılması gerekir.\r\n\r\nUseEndpoints middleware’inde normalde Index olarak tanımlı olan method adını Login olarak değiştirdik. Oluşturacağımız Login.cshtml dosyasına direkt yönledirme yapacağız. Kullanıcı kimliği doğrulanırsa Index sayfasına yönlendirme yapacağız.\r\n\r\nEntityFramework codefirst ile tablolarımızı oluşturmak için Nuget Manager’ den eklememiz gereken paketler;\r\n\r\nMicrosoft.EntityFrameworkCore\r\n\r\nMicrosoft.EntityFrameworkCore.SqlServer\r\n\r\nMicrosoft.EntityFrameworkCore.Tools\r\n\r\nClasslarımız yani tablolarımız;",
                     CodeBlocks = new List < CodeBlock > () {
                       new CodeBlock() {
                         CodeNo = 4,
                           Code = " public class Role\r\n    {\r\n        public int Id { get; set; }\r\n        public string Name { get; set; }\r\n        public ICollection<UserRole> UserRoles { get; set; }\r\n    }\r\n    \r\n    public class User\r\n    {\r\n        public int Id { get; set; }\r\n        public string UserName { get; set; }\r\n        public string FullName { get; set; }\r\n        public string Password { get; set; }\r\n        public string TaxNumber { get; set; }\r\n        public ICollection<UserRole> UserRoles { get; set; }\r\n    }\r\n        public class UserRole\r\n    {\r\n        public int UserId { get; set; }\r\n        public int RoleId { get; set; }\r\n        public virtual User User { get; set; }\r\n        public virtual Role Role { get; set; }\r\n    }"
                       },
                     }
                 },
                 new BlogPart() {
                   PartNo = 5,
                     Text = "Tabloları oluşturacağımız sınıf (yukarıda Startup.cs’ ye eklediğimiz) ;",
                     CodeBlocks = new List < CodeBlock > () {
                       new CodeBlock() {
                         CodeNo = 5,
                           Code = " public ApplicationContext(DbContextOptions options) : base(options)\r\n        {\r\n        }\r\n        \r\n        public DbSet<User> Users { get; set; }\r\n        public DbSet<Role> Roles { get; set; }\r\n        public DbSet<UserRole> UserRoles { get; set; }\r\n\r\n        protected override void OnModelCreating(ModelBuilder modelBuilder)\r\n        {\r\n            modelBuilder.Entity<UserRole>()\r\n                .HasKey(ur => new { ur.UserId, ur.RoleId });\r\n            modelBuilder.Entity<UserRole>()\r\n                .HasOne(ur => ur.User)\r\n                .WithMany(u => u.UserRoles)\r\n                .HasForeignKey(ur => ur.UserId);\r\n            modelBuilder.Entity<UserRole>()\r\n                .HasOne(ur => ur.Role)\r\n                .WithMany(u => u.UserRoles)\r\n                .HasForeignKey(ur => ur.RoleId);\r\n        }"
                       }
                     }
                 },
                 new BlogPart() {
                   PartNo = 6,
                     Text = "Package Manager Console’ da migration komutlarıyla sırasıyla çalıştıralım ve tablolarımızı oluşturalım. <br/><br/> add-migration <br/>update-database<br/><br/> UserMigration bizim rastgele migrationa verdiğimiz isimdir.<br/><br/>HomeController’ da kullanıcı tanımını ve kontrol işlemini yapalım.",
                     CodeBlocks = new List < CodeBlock > () {
                       new CodeBlock() {
                         CodeNo = 6,
                           Code = " public class HomeController : Controller\r\n    {\r\n        private readonly ILogger<HomeController> _logger;\r\n        private readonly ApplicationContext _appContext;\r\n\r\n        public HomeController(ILogger<HomeController> logger,ApplicationContext appContext)\r\n        {\r\n            _logger = logger;\r\n            _appContext = appContext;\r\n        }\r\n\r\n        [Authorize]\r\n        public IActionResult Index()\r\n        {\r\n            var userContext = HttpContext.User.Claims.FirstOrDefault(x => x.Type == \"TaxNumber\");\r\n            if (userContext != null)\r\n            {\r\n                ViewBag.TaxNumber = userContext.Value;\r\n            }\r\n\r\n            return View();\r\n        }\r\n\r\n        public IActionResult Login()\r\n        {\r\n            return View();\r\n        }\r\n\r\n        [HttpPost]\r\n        public IActionResult Login(string name,string password)\r\n        {\r\n            var user = _appContext.Users.FirstOrDefault(x => x.UserName == name && x.Password == password);\r\n            if (user == null)\r\n                return View();\r\n\r\n            ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);\r\n            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));\r\n            identity.AddClaim(new Claim(\"TaxNumber\", user.TaxNumber.ToString()));\r\n            var principal = new ClaimsPrincipal(identity);\r\n            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);\r\n            return RedirectToAction(\"Index\",\"Home\");\r\n        }\r\n}"
                       }
                     }
                 },
                 new BlogPart() {
                   PartNo = 7,
                     Text = "Index sayfasına girebilmek için doğrulanmış kimlik isteyeceğiz. Bunun için [Authorize] attribute unu ekledik Index metodunun başına. Login sayfamızın post işleminde ise tablodan aldığımız kullanıcı bilgisini Claim’ler oluşturarak yukarıda bahsettiğimiz gibi ClaimIdentity’ye, onu ClaimsPrincipal’a ve son olarak principal nesnemizi de HttpContext.SignInAsync metoduyla kaydediyoruz.\r\n\r\n“TaxNumber” adında custom bir Claim oluşturduk onu Index girişinde <br/><br/><br/>var userContext = HttpContext.User.Claims.FirstOrDefault(x => x.Type == “TaxNumber”);\r\nif (userContext != null)\r\n{\r\nViewBag.TaxNumber = userContext.Value;\r\n}\r\nşeklinde aldık ViewBag.TaxNumber’a yerleştirdik. Bu bilgiyi Index.cshtml sayfasında göstereceğiz.\r\n\r\nŞimdi Login.cshtml sayfamızı oluşturalım. ",
                     CodeBlocks = new List < CodeBlock > () {
                       new CodeBlock() {
                         CodeNo = 7,
                           Code = "@{\r\n    Layout = null;\r\n    <!DOCTYPE html>\r\n    <html lang=\"en\">\r\n    <head>\r\n        <meta charset=\"utf-8\" />\r\n        <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n        <title>@ViewData[\"Title\"] - Authentication</title>\r\n        <link rel=\"stylesheet\" href=\"~/lib/bootstrap/dist/css/bootstrap.min.css\" />\r\n        <link rel=\"stylesheet\" href=\"~/css/site.css\" />\r\n    </head>\r\n    <body>\r\n        <header>\r\n            <div class=\"container\">\r\n                <div class=\"row\">\r\n                    <div class=\"col-md-3\"></div>\r\n\r\n                    <form asp-action=\"Login\" method=\"post\">\r\n                        <div class=\"form-group\">\r\n                            <label>Username:</label>\r\n                            <input type=\"text\" class=\"form-control\" id=\"name\" name=\"name\" />\r\n                        </div>\r\n                        <div class=\"form-group\">\r\n                            <label>Password:</label>\r\n                            <input type=\"password\" class=\"form-control\" id=\"password\" name=\"password\" />\r\n                        </div>\r\n                        <div class=\"form-group\">\r\n                            <input type=\"submit\" class=\"btn btn-primary\" value=\"Login\" />\r\n                        </div>\r\n                    </form>\r\n                </div>\r\n            </div>\r\n        </header>\r\n    </body>\r\n    <footer class=\"border-top footer text-muted\">\r\n        <div class=\"container\">\r\n            &copy; 2021 - Authentication - <a asp-area=\"\" asp-controller=\"Home\" asp-action=\"Privacy\">Privacy</a>\r\n        </div>\r\n    </footer>\r\n    <script src=\"~/lib/jquery/dist/jquery.min.js\"></script>\r\n    <script src=\"~/lib/bootstrap/dist/js/bootstrap.bundle.min.js\"></script>\r\n    <script src=\"~/js/site.js\" asp-append-version=\"true\"></script>\r\n</html>"
                       }
                     }
                 },
                 new BlogPart() {
                   PartNo = 8,
                     Text = "Index.cshtml sayfamıza da TaxNumber ı okuyup ekranda gösterelim.",
                     CodeBlocks = new List < CodeBlock > () {
                       new CodeBlock() {
                         CodeNo = 8,
                           Code = "@{\r\n    ViewData[\"Title\"] = \"Home Page\";\r\n}\r\n\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">Welcome</h1>\r\n    @{\r\n        if (ViewBag.TaxNumber != null)\r\n        {\r\n            <p>Your Tax Number is \" @(ViewBag.TaxNumber) \"</p>\r\n        }\r\n        else\r\n        {\r\n            <p>You have no tax number  </p>\r\n        }\r\n    }\r\n    <p>Learn about <a href=\"https://docs.microsoft.com/aspnet/core\">building Web apps with ASP.NET Core</a>.</p>\r\n</div>"
                       },
                     }
                 },
                 new BlogPart() {
                   PartNo = 9,
                     Text = "Tabii ki tablolarımıza kayıt yapmalıyız.",
                     Images = new List < Image > () {
                       new Image() {
                           ImageNo = 1,
                             ImageInfo = "https://miro.medium.com/v2/resize:fit:720/format:webp/1*psDmXs6G8Q7GvLZSziTTFg.png"
                         },
                         new Image() {
                           ImageNo = 2,
                             ImageInfo = "https://miro.medium.com/v2/resize:fit:425/1*NgZPdT4YEKpTpvM-ZXclZQ.png"
                         },
                         new Image() {
                           ImageNo = 3,
                             ImageInfo = "https://miro.medium.com/v2/resize:fit:624/1*fS8mW6Sa0eiY8fgTymyQQA.png"
                         }
                     }
                 },
                 new BlogPart() {
                   PartNo = 10,
                     Text = "Birazdan authorization işleminde bu yetkileri kullanacağız. Şimdi authentication kontrolünü yapalım.",
                     Images = new List < Image > () {
                       new Image() {
                         ImageNo = 4,
                           ImageInfo = "https://miro.medium.com/v2/resize:fit:700/0*trRsU-Txn6bSeAxR"
                       },
                     }
                 },
                 new BlogPart() {
                   PartNo = 11,
                     Text = "Özgün olarak giriş yapıyorum, Index sayfasına yönlendirip TaxNumber ımı yazması gerekiyor.",
                     Images = new List < Image > () {
                       new Image() {
                         ImageNo = 5,
                           ImageInfo = "https://miro.medium.com/v2/resize:fit:700/0*sft1vv3Tp64GefH6"
                       },
                     }
                 },
                 new BlogPart() {
                   PartNo = 12,
                     Text = "Doğru:)\r\n\r\nŞimdi Müşteri2 ile giriş yapmayı deniyorum.",
                     Images = new List < Image > () {
                       new Image() {
                         ImageNo = 6,
                           ImageInfo = "https://miro.medium.com/v2/resize:fit:700/1*oZpOV0rOsuQ6JkD0dFsdWQ.png"
                       },
                     }
                 },
                 new BlogPart() {
                   PartNo = 13,
                     Text = "Müşteri2' nin UserName ve TaxNumber bilgisi Claimler ile HttpContext’ e kaydedildiği için kimliği doğrulanmış oldu.\r\n\r\nHttpContext.SignInAsync metodunu açıklama satırı yapıp Özgün kullanıcısıyla tekrar giriş yapmayı denediğimde",
                     Images = new List < Image > () {
                       new Image() {
                           ImageNo = 7,
                             ImageInfo = "https://miro.medium.com/v2/resize:fit:700/1*cocBi5Gspw_fNnEwBZDWRw.png"
                         },
                         new Image() {
                           ImageNo = 8,
                             ImageInfo = "https://miro.medium.com/v2/resize:fit:700/1*NfKUsNkVrJnwgM0nK7Z0OQ.png"
                         }
                     }
                 },
                 new BlogPart() {
                   PartNo = 14,
                     Text = "hatasını alıyorum.\r\n\r\nŞimdi yetkileri kullanalım, sadece yapmamız gereken Authorize attriubute sine Roles tanımı yapmak. Index sayfasına sadece Admin yetkisi olanın giriş yapmasını istiyorum.\r\n\r\nYetkiler claim tipi role olarak tanımlanır.\r\n\r\nRoller birer claimdirler ama her claim role değildir. Bir rolü ClaimType sınıfının içindeki Role tipiyle tanımlayabiliriz. Role tanımlamalarını bir claim içerisinde kaydederiz ve oradan Role tipindeki değerleri alarak yetki kontrolü yaparız. Claim tipleri ClaimTypes sınıfında tanımlanır.",
                     Images = new List < Image > () {
                       new Image() {
                         ImageNo = 9,
                           ImageInfo = "https://miro.medium.com/v2/resize:fit:700/1*tARXXfxR-_itndqM75yeMA.png"
                       }
                     }
                 },
                 new BlogPart() {
                   PartNo = 15,
                     Text = "Şimdi Müşteri kullanıcısıyla giriş yaptığımda",
                     Images = new List < Image > () {
                       new Image() {
                         ImageNo = 10,
                           ImageInfo = "https://miro.medium.com/v2/resize:fit:700/1*FD46kKYBvN6Q9IYxPeQxRg.png"
                       },
                     }
                 },
                 new BlogPart() {
                   PartNo = 16,
                     Text = "AccessDenied yani erişim yetkisi olmadığını söyledi.\r\n\r\nROLE-BASED / CLAIM-BASED POLICY\r\n\r\nRole-Based;\r\n\r\nTanımladığımız claimler ve roller ile ilgili işlem yaparken birden fazla kontrol yapmak isteyebiliriz. Bu gereksinimler bütünü policy olarak tanımlanıyor ve bu tanımın içerisinde yer alan gereksinimleri kontrol edebiliyoruz.\r\n\r\nBirden fazla role kontrolü yapılması istendiğinde bunu policy olarak tanımlayalım.\r\n\r\nStartup.cs’de ConfigureServices metoduna aşağıdaki şekilde kod bloğunu ekleyelim.",
                     Images = new List < Image > () {
                       new Image() {
                         ImageNo = 11,
                           ImageInfo = "https://miro.medium.com/v2/resize:fit:700/1*Q-NkUpmj5XDQK2g3Lf_ggA.png"
                       },
                     }
                 },
                 new BlogPart() {
                   PartNo = 17,
                     Text = "Privacy sayfasına kontrolümüzü ekleyelim;",
                     Images = new List < Image > () {
                       new Image() {
                         ImageNo = 12,
                           ImageInfo = "https://miro.medium.com/v2/resize:fit:581/1*vGs4lTIzbxzTDvmPD8bZiw.png"
                       }
                     }
                 },
                 new BlogPart() {
                   PartNo = 18,
                     Text = "Özgün kullanıcısıyla Index sayfasına giriş yaptıktan sonra Privacy menüsüne tıkladığımda yapılacak role kontrolleri yukarıda tanımladığımız şekilde admin ve editor olacaktır ve Özgün kullanıcısına sayfayı açacaktır.\r\n\r\nClaim-Based;\r\n\r\nAynı şekilde Role yerine tanımlanan belli bir claim in olması şartını policy’ye ekleyebiliriz.",
                     Images = new List < Image > () {
                       new Image() {
                         ImageNo = 13,
                           ImageInfo = "https://miro.medium.com/v2/resize:fit:700/1*dNgLbhzjzHOz0cpmNpwasA.png"
                       },
                     }
                 },
                 new BlogPart() {
                   PartNo = 19,
                     Text = "Burada AdministratorName adıyla tanımlanan policyde ClaimTipi Name e sahip olan ve Özgün değerine eşit olan claim varsa erişime izin vermesi istenir.\r\n\r\nYine Privacy metodunda kontrolü aynı şekilde yapıyoruz.",
                     Images = new List < Image > () {
                       new Image() {
                           ImageNo = 14,
                             ImageInfo = "https://miro.medium.com/v2/resize:fit:700/1*eoIe8MSQyX-bMlT5Fnu9KQ.png"
                         },
                         new Image() {
                           ImageNo = 15,
                             ImageInfo = "https://miro.medium.com/v2/resize:fit:700/1*UbeTy1nvX_yIdG-zrTVMOg.png"
                         }
                     }
                 },
                 new BlogPart() {
                   PartNo = 20,
                     Text = "Sayfaya erişebildik.\r\n\r\nKaydettiğimiz bilgileri silmek için yapmamız gereken işlem ise HttpContext.SignOutAsync() metodunu kullanmaktır. Aşağıdaki şekilde Logout metodunu HomeController’da oluşturalım. <br/><br/>public IActionResult Logout()\r\n{\r\nHttpContext.SignOutAsync();\r\nreturn RedirectToAction(“Login”, “Home”);\r\n}"
                 },
                 new BlogPart() {
                   PartNo = 21,
                     Text = "Layout.cshtml de tanımladığımız Logout menusunu tıkladığımızda bu metoda gelerek kayıtlı olan kullanıcı bilgilerini silecektir.\r\n\r\nHerhangi bir paket yüklemeden tanımladığımız tablolar ile Cookie şemasını kullanarak Authentication ve Authorization işlemini yaptık. JWTBearer için Startup.cs de tanımladığımız Cookie şeması yerine tanımlama yapıp token authentication de yapılabilir.\r\n\r\nYükleyerek kullandığımız paketler içerisinde kullanılan Claims tanımlamalarını ve kimlik bilgisi kaydetme işlemini, yetkilendirme mantığını elimden geldiğince temel şekilde örneklendirdim. Faydalı olabilmesi dileğiyle.\r\n\r\nKaynaklar;https://learn.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-6.0&source=post_page-----e9e7f22f4f20--------------------------------"
                 },
             }
         },
         new Blog {
           Title = "REDIS’ DE REPLICATION, MASTER-SLAVE İLİŞKİSİ",
             Content = "Replication yani ‘çoğaltma’ özelliği, bir Redis sunucusunun başka bir Redis sunucusuna birebir kopyalanması işlemini temel alır. Master’da yapılan güncellemeler, slave’de de gerçekleşir. Redis’in 2.6 sürümünden itibaren sadece veri okuma amaçlı default olarak readonly’dir. Kurulum dosyasında bulunan redis.windows.conf dosyasında “slave-read-only yes” ifadesini no olarak değiştirebiliriz ancak bu master ve slave arasında tutarsızlık oluşturabileceği nedeniyle tavsiye edilmez. Slave’in yazma özelliğini aktif ettiğimiz takdirde oluşan değişikler master a aktırılmaz.",
             CreatedDate = new DateTime(2021, 12, 6),
             LastUpdatedDate = new DateTime(2021, 11, 25),
             Image = slaveImage,
             Link = "https://medium.com/@ozgundgn0/redis-de-replication-master-slave-i%CC%87li%CC%87%C5%9Fki%CC%87si%CC%87-3e756102d945",
             BlogParts = new List < BlogPart > () {
               new BlogPart() {
                 PartNo = 1,
                   Text = "Replication yani ‘çoğaltma’ özelliği, bir Redis sunucusunun başka bir Redis sunucusuna birebir kopyalanması işlemini temel alır. Master’da yapılan güncellemeler, slave’de de gerçekleşir. Redis’in 2.6 sürümünden itibaren sadece veri okuma amaçlı default olarak readonly’dir. Kurulum dosyasında bulunan redis.windows.conf dosyasında “slave-read-only yes” ifadesini no olarak değiştirebiliriz ancak bu master ve slave arasında tutarsızlık oluşturabileceği nedeniyle tavsiye edilmez. Slave’in yazma özelliğini aktif ettiğimiz takdirde oluşan değişikler master a aktırılmaz.\r\n\r\nÖzellikleri;\r\n\r\n* Replication işlemi kullanıcı müdahelesi gerektirmeden otomatik olarak gerçekleşir.\r\n\r\n* Herhangi bir ağ kesintisi durumunda slaveler otomatik olarak mastera bağlanmayı dener ve senkronize olurlar.\r\n\r\n* Slaveler de birbirlerinden oluşturulabilir.\r\n\r\nÖrneğin; A master sunucu,B A’dan oluşan slave, C B’den oluşan slave olsun\r\n\r\nA →B →C\r\n\r\nB yazılabilir bir slave olduğunda, onun üzerindeki değişiklikleri C görmez. C bunun yerine A master sunucusunun kopyasını taşımaya devam eder.\r\n\r\nNeden ihtiyaç duyulur?\r\n\r\nSunucumuzda çok fazla operasyon olduğunda okuma işlemlerimizi slavelere vererek veri kontrolü, erişim güvenliği sağlamış oluruz.\r\n\r\nNasıl kullanılır?\r\n\r\nBilgisayarımıza ikinici bir redis-cli yüklüyoruz, bunu slave olarak tanımlayacağız.\r\n\r\n",
                   Images = new List < Image > () {
                     new Image() {
                       ImageNo = 1,
                         ImageInfo = "https://miro.medium.com/v2/resize:fit:700/0*Xih-v2k9lo_EaAhv"
                     }
                   }
               }, new BlogPart() {
                 PartNo = 2,
                   Text = "Slave olarak değiştireceğimiz redis klasöründeki redis.windows.conf dosyasında açıklama satırı olan slaveof kısmını (aşağıdaki ekran görüntüsünde bulunan)\r\n\r\n“ slaveof 127.0.0.1 6379” ve portu 6380 olarak değiştirelim. Master portumuz 6379 olarak kaldı (bu değer default olarak tanımlıdır) ve slave’imizi de 6379'dan türetip portunu 6380 yaptık.",
                   Images = new List < Image > () {
                     new Image() {
                       ImageNo = 2,
                         ImageInfo = "https://miro.medium.com/v2/resize:fit:700/0*7ILC0nZ8lJz6WA_G"
                     },
                   }
               }, new BlogPart() {
                 PartNo = 3,
                   Text = "Her iki kurulum dosyasında bulunan redis-cli.exe lerini çalıştırarak deneme yapalım.",
                   Images = new List < Image > () {
                     new Image() {
                       ImageNo = 3,
                         ImageInfo = "https://miro.medium.com/v2/resize:fit:700/0*64Lyz7luISn7jCm0"
                     },
                   }
               }, new BlogPart() {
                 PartNo = 4,
                   Text = "Master sunucumuzda set ile birdegerkoy degerini 5 tanımladık ve slave olan 6380 portumuzda bu değeri get metodu ile okuyabildik.\r\n\r\nSlave sunucumuzda set etme işlemi yapmak istiyoruz.",
                   Images = new List < Image > () {
                     new Image() {
                       ImageNo = 4,
                         ImageInfo = "https://miro.medium.com/v2/resize:fit:700/0*WCU5CvG97y7RShDm"
                     },
                   }
               }, new BlogPart() {
                 PartNo = 5,
                   Text = "Default olarak read-only olan slave’imiz set işlemine izin vermedi.\r\n\r\nINFO komutunu redis-cli.exe de çalıştırarak bulunduğumuz redis server ile ilgili bilgileri görebiliyoruz .\r\n\r\n",
                   Images = new List < Image > () {
                     new Image() {
                       ImageNo = 5,
                         ImageInfo = "https://miro.medium.com/v2/resize:fit:700/1*t8dxnF2L7VEYnGnZZbcJCw.png"
                     },
                   }
               }, new BlogPart() {
                 PartNo = 6,
                   Text = "Slave olan redis-cli.exe de INFO komutu sonucunda REPLICATION kısmında “role:slave” olarak hangi görevde olduğu belirtiliyor.\r\n\r\nC# da Kullanımı için;\r\n\r\nSon olarak C# için bağlantı şekline bir örnek verelim. Nuget Package Manager den yüklediğimiz ServiceStack.Redis ile bağlantısı",
                   CodeBlocks = new List < CodeBlock > () {
                     new CodeBlock() {
                       CodeNo = 1,
                         Code = "using ServiceStack.Redis;\r\nusing System;\r\n\r\nnamespace RedisMasterSlave\r\n{\r\n    class Program\r\n    {\r\n        static void Main(string[] args)\r\n        {\r\n            IRedisClientsManager _clientsManager = new PooledRedisClientManager(\"localhost:6379\",\"localhost:6380\");\r\n            var _redisClientMaster = _clientsManager.GetClient();\r\n            var _redisClientSlave = _clientsManager.GetReadOnlyClient();\r\n            _redisClientMaster.Set(\"birdegerkoy\", 45);\r\n            var deger = _redisClientSlave.Get<string>(\"birdegerkoy\");\r\n            Console.WriteLine(deger);\r\n            \r\n        }\r\n    }"
                     }
                   }
               }, new BlogPart() {
                 PartNo = 7,
                   Text = "_redisClientMaster ile mastera “birdegerkoy” duk. Bu değeri _redisClientSlave ile okuduk.\r\n\r\nGörevi genel olarak okumak slave bağlantımızı GetReadOnlyClient() ile alabiliyoruz.\r\n\r\nStackExchange.Redis ile yapılan bağlantı;",
                   CodeBlocks = new List < CodeBlock > () {
                     new CodeBlock() {
                       CodeNo = 2,
                         Code = "using StackExchange.Redis;\r\nusing System;\r\n\r\nnamespace RedisMasterSlave\r\n{\r\n    class Program\r\n    {\r\n        static void Main(string[] args)\r\n        {\r\n\r\n            Console.WriteLine(\"Value from Slave -- StackExchange.Redis \");\r\n\r\n            var conn = ConnectionMultiplexer.Connect(\"localhost:6379,localhost:6380\");\r\n            var db = conn.GetDatabase(0);\r\n            db.StringSet(\"baskadeger\", 23);\r\n            var fromReplica = db.StringGet(\"baskadeger\", CommandFlags.PreferSlave);\r\n            Console.WriteLine(fromReplica);\r\n            Console.ReadLine();\r\n        }\r\n    }\r\n}"
                     },
                   }
               }, new BlogPart() {
                 PartNo = 8,
                   Text = "Burada da\r\n\r\nvar fromReplica = db.StringGet(“baskadeger”, CommandFlags.PreferSlave);\r\n\r\nsatırında özellikle CommandFlags.PreferSlave ifadesiyle slave(köle) olan bağlantıdan veriyi almak istediğimizi belirtmiş olduk.\r\n\r\nKaynaklar;\r\n\r\nhttps://redis.io/topics/replication\r\n\r\nhttps://www.programmerall.com/article/4414584805/\r\n\r\nhttp://cagataykiziltan.net/redis-replica/\r\n\r\nhttp://devnot.com/2018/rediste-master-slave-ve-sentinel-yapilari/"
               }
             }
         },
         new Blog {
           Title = "Redis Caching ve Pub/Sub/Messaging",
             Content = "Çalıştığım iş yerinde etkin olarak kullandığımız ve üzerinde araştırma da yaparak edindiğim bilgileri paylaşmak istememden kaynaklı, NoSql veri yapısı yönetimi ve sunucusu olan Redis’in caching ve pub/sub/messaging özelliklerinden bahsedeceğim.",
             CreatedDate = new DateTime(2021, 12, 6),
             Image = pubsubImage,
             LastUpdatedDate = new DateTime(2021, 11, 17),
             Link = "https://medium.com/@ozgundgn0/redis-caching-ve-pub-sub-messaging-5cfa0bb1c199",
             BlogParts = new List < BlogPart > () {
               new BlogPart() {
                   PartNo = 1,
                     Text = "Çalıştığım iş yerinde etkin olarak kullandığımız ve üzerinde araştırma da yaparak edindiğim bilgileri paylaşmak istememden kaynaklı, NoSql veri yapısı yönetimi ve sunucusu olan Redis’in caching ve pub/sub/messaging özelliklerinden bahsedeceğim.\r\n\r\nREmote DIctionary Server (Uzak Sözlük Sunucusu), yani REDIS ilk olarak Salvatore Sanfilippo tarafından geliştirilen hızlı, açık kaynak (open source), bellek içi (in-memory, bilgi sadece ram de tutulur, bilgisayar kapatılınca kaybolur ), anahtar-değer (key-value pair) veri deposudur.\r\nİstenildiği takdirde diske yazma özelliği de vardır.(bkz. https://redis.io/topics/persistence).\r\n\r\nRedis verileri 3 şekilde tutabilmektedir;\r\n1) RedisNativeClient: Düşük seviye byte[] dizisi cinsinden veri tutar.\r\n2) RedisClient: string olarak veri tutar.\r\n3) RedisTypedClient: redis.As<T>() şeklinde verilen T modelinde, .Net POCO tipi veri tutma işlemini yapar.\r\n\r\nBu yöntemlerden bir veya birkaçını aşağıdaki örneklerde kullanmış olacağız.\r\n\r\nGereklilikler;\r\n* redis-cli https://github.com/microsoftarchive/redis/releases son sürümünü indirebilirsiniz,\r\n* Redis Desktop Manager veya Another Redis Desktop Manager buradan kaydettiğimiz bilgileri arayüzde görmek için.\r\n\r\nRedis ile ilgili indirmeleri yaptıktan sonra Hizmetlerden kontrol edelim, çalışıyor mu diye.",
                     Images = new List < Image > () {
                       new Image() {
                         ImageNo = 1,
                           ImageInfo = "https://miro.medium.com/v2/resize:fit:1000/1*8AkBLiUgv6luaqiTwpEQZg.png"
                       },
                     }
                 },
                 new BlogPart() {
                   PartNo = 2,
                     Text = ".Net için gerekli olan ServiceStack, .Net Core için ServiceStack.Core paketlerini Nuget Manager dan yüklüyoruz.\r\nServiceStack.Redis den yararlanacağız.\r\n\r\n— CACHING —\r\n\r\nSürekli olarak değişmeyen verilerin kullanımı ve yönetimi için tekrar tekrar aynı verileri çekerek projeye fazladan yük olmak yerine verileri bellekte tutarak işlerimizi hızlıca çözebiliriz. Bunun için veri tabanında bulunan ve her zaman değişmeyecek olan User listemizi Redis ile bellekte tutacağız. Redis bize verileri tutmamız için 16 farklı db sunar. Bu verileri görmek için Redis Desktop Manager veya Another Redis Desktop Manager ı kullanacağız.\r\n\r\nVeri tabanından çekilecek olan bilgileri temsilen bir User listesi oluşturduk.\r\n\r\nŞimdi redis bağlantımızı yapıp çektiğimiz users bilgisini önce bellekte var mı diye kontrol edip yoksa Lists metodunu kullanarak belleğe ekleyeceğiz;",
                     CodeBlocks = new List < CodeBlock > () {
                       new CodeBlock() {
                         CodeNo = 1,
                           Code = " public class RedisService{\r\n private IRedisClient _client;\r\n private IRedisClientsManager _clientsManager;\r\n public RedisService(){\r\n _clientsManager=new BasicRedisClientManager(\"localhost?db=1\")\r\n _client = _clientsManager.GetClient(); \r\n }\r\n \r\n public List<User> GetCurrentUsers(string key){\r\n if (_client.ContainsKey(key))\r\n {\r\n return _client.Get<List<User>>(key);\r\n }\r\n var currentUsers = UserManager.GetAllUsers();\r\n SetLists<User>(key, currentUsers, DateTime.Now.AddDays(1),_client);\r\n return currentUsers;\r\n }\r\n \r\n public void SetLists<T>(string key, IEnumerable<T> list, DateTime expireTime,IRedisClient _client)\r\n {\r\n IRedisTypedClient<T> redisTyped = _client.As<T>();\r\n var currentList = redisTyped.Lists[key];\r\n var cachedList = currentList.Concat(list).ToList();\r\n _client.Set(key, cachedList, expireTime);\r\n }\r\n}"
                       },
                     }
                 }, new BlogPart() {
                   PartNo = 3,
                     Text = "Yukarıda RedisService in constructor ında “localhost?db=1” ifadesiyle 1. database i kullanacağımızı belirttik. IRedisClientsManager ı pub/sub/ messaging için birazdan kullanacağız.) GetCurrentUsers metoduna bir key vererek o key e ait bellekte tuttuğumuz bir değer var mı kontrolü yapıp yoksa SetLists metoduyla belleğe 1 günlüğüne ekledik. BasicRedisClientManager classı RedisManagerPool, PooledRedisClientManager gibi IRedisClientsManager interfacinden türemiştir. IoC için kullanımını kolaylıkla sağlayabiliriz.\r\n\r\nBunları program.cs de veya çalıştırmak istediğimiz yerde çalıştıralım;"
                 }, new BlogPart() {
                   PartNo = 4,
                     Text = "Verdiğimiz “urn:current:users” key i ile bellekte bu veriyi tutacağız. Buradaki “:” ifadesi klasörleme mantığında çalışmaktadır. Yani db1>urn>current>users>”urn:current:users” değeri bulunur.",
                     Images = new List < Image > () {
                       new Image() {
                           ImageNo = 2,
                             ImageInfo = "https://miro.medium.com/v2/resize:fit:1000/1*7txRKXUWYzygJeG48n3Fwg.png"
                         },
                         new Image() {
                           ImageNo = 3,
                             ImageInfo = "https://miro.medium.com/v2/resize:fit:700/1*yR7eNsrKSLh7UIkR3BH9Uw.png"
                         },
                     },
                     CodeBlocks = new List < CodeBlock > () {
                       new CodeBlock() {
                         CodeNo = 2,
                           Code = "class Program\r\n {\r\n static void Main(string[] args)\r\n {\r\n RedisService service= new RedisService();\r\n var userList= service.GetCurrentUsers(\"urn:current:users\");\r\n }\r\n }"
                       },
                     }
                 }, new BlogPart() {
                   PartNo = 5,
                     Text = "— PUB/SUB/MESSAGING —\r\n\r\nClient ların birbirlerinin yayınladıkları bilgilere ulaşmaları için kullanılan bir yöntemdir. Belirli kanallara üye olup bu kanallar üzerinde mesaj gönderebiliriz. Bu kanallara üye olan clientlar da bizim gönderdiğimiz mesajlara ulaşabilirler veya biz onların mesajlarına ulaşabiliriz. Aktif üyelikleri bulunduğu sürece her client bildirimleri almaya devam eder.\r\n\r\nBir veya birden fazla kanala üye olup o kanala gelen mesajları console a yazdırdığımız küçük bi uygulama yapalım.\r\n\r\nYukarıda oluşturduğumuz RedisService sınıfına Subscribe ve PublishMessage metodlarını ekleyelim.\r\n\r\n",
                     CodeBlocks = new List < CodeBlock > () {
                       new CodeBlock() {
                         CodeNo = 3,
                           Code = "\r\npublic void PooledSubscribe(string channel1, string channel2)\r\n {\r\n new RedisPubSubServer(_clientsManager, channel1, channel2)\r\n {\r\n OnMessage = (channel, msg) => \"Received '{0}' from '{1}'\".Print(msg, channel)\r\n }.Start();\r\n }\r\n \r\n public void PublishMessage(string channel, string message)\r\n {\r\n _redisClient.PublishMessage(\"channel-2\", \"sabit mesajımız Service Stackten\");\r\n _redisClient.PublishMessage(channel, message);\r\n }"
                       },
                     }
                 }, new BlogPart() {
                   PartNo = 6,
                     Text = "PooledSubscribe metoduyla iki kanala üye olduk. Aynı yerde sadece bir kanala da üye olabiliriz. PublishMessage metodunda ise vereceğimiz kanal ismine göre kanala mesaj gönderiyoruz ve dinlediğimiz channel-2 ye de bir mesaj gönderiyoruz. Mesajları OnMessage olayını tetikleyerek Print ile console a yazdırıyoruz. Program.cs de metodlarımızı çalıştıralım.\r\n\r\n",
                     CodeBlocks = new List < CodeBlock > () {
                       new CodeBlock() {
                         CodeNo = 4,
                           Code = "class Program\r\n {\r\n static void Main(string[] args)\r\n {\r\n RedisService service= new RedisService();\r\n var userList= service.GetCurrentUsers(\"urn:current:users\");\r\n \r\n service.PooledSubscribe(\"channel-1\", \"channel-2\");\r\n service.PublishMessage(\"channel-1\", JsonConvert.SerializeObject(userList));\r\n }\r\n }"
                       }
                     }
                 }, new BlogPart() {
                   PartNo = 7,
                     Text = "Console çıktımız;",
                     Images = new List < Image > () {
                       new Image() {
                         ImageNo = 8,
                           ImageInfo = "https://miro.medium.com/v2/resize:fit:1000/1*yhtgk1uIYQ9FwstEFw0f9g.png"
                       }
                     }
                 },
                 new BlogPart() {
                   PartNo = 8,
                     Text = "Evet, rediste caching ve pub/sub messaging işlemlerini temel bazda örneklendirdik. İlk medium yazımı da böylelikle yazmış bulunuyorum. StackExchange ile olan kullanım ve bir kaç örnekle daha yazılım mimarisine daha uygun olarak yazdığım proje github hesabımda mevcuttur."
                 }

             }
         }
       });

                await _context.SaveChangesAsync();
            }
        }
    }
}