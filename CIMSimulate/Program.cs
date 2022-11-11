using CIMSimulate.Service;
using CIMSimulate.Service.ScheduleService;
using CIMSimulate.Service.UtilS;
using Quartz;
using Newtonsoft.Json.Serialization;
using CIMSimulate.Service.SimulateService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<CIMService>();
builder.Services.AddSingleton<GreenTransService>();
builder.Services.AddSingleton<JQservice>();

builder.Services.AddSingleton<CacheService>();
builder.Services.AddSingleton<DBService>();
builder.Services.AddSingleton<FileService>();
builder.Services.AddSingleton<SoapService>();
builder.Services.AddSingleton<HttpService>();
builder.Services.AddSingleton<SeleniumService>();
builder.Services.AddSingleton<VerifyService>();

builder.Services.AddMemoryCache();
//builder.Services.AddSignalR().AddJsonProtocol();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("jobKey");
    //Register the job with the DI container
    //2版文件
    //http://www.quartz-scheduler.org/documentation/quartz-2.3.0/tutorials/crontrigger.html
    //3版文件
    //https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/crontriggers.html
    q.AddJob<Jobs>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts =>
        opts.ForJob(jobKey) // link to the DealerAndAgentSettlementJob
        .WithIdentity("jobKey-trigger") // give the trigger a unique name
        .WithDescription("DealerAndAgentSettlementJob Work")
        .WithCronSchedule("0 0 4 ? * *")//執行 every day 4:00 am
    );

    //example
    //q.AddTrigger(opts =>
    //    opts.ForJob(usersRecordJobKey)
    //    .WithIdentity("UsersRecordJob-trigger")
    //    .WithDescription("UsersRecordJob Work")
    //    .WithCronSchedule("0 0/1 * * * ?")//三分鐘跑一次
    //);

    builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
});



builder.Services.AddControllers().AddNewtonsoftJson(option =>
{
    //加入Newtonsoft.Json.Serialization;這行 才能讓 前端傳過來的 JSON 都自動序列化
    option.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
