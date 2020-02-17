# Documentation  

## Overview  

A bootstraped MVC web app with ASP.NET Core 3 and Razor pages 

## Getting Started  

### How to get the app up and running

#### Create MVC app with Local User Authentication

# [ASP.NET Core Identity Module](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/add-user-data?view=aspnetcore-3.1&tabs=visual-studio#add-custom-user-data-to-the-identity-db)

## User authentication and scaffolding

### Getting Started

At app creation screen:
* Choose `Change Authentication` => `Local User Accounts` => `OK`
* From the NuGet Package Console, run `Update-Database`
* From `Startup.ConfigueServices`, add the following code block:
```
services.Configure<IdentityOptions>(options =>
    {
        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;
    });

    services.ConfigureApplicationCookie(options =>
    {
        // Cookie settings
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

        options.LoginPath = "/Identity/Account/Login";
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        options.SlidingExpiration = true;
    });
```

#### Install Entity Core Framework  

Tools => NuGet Package Manager => Package Manager Console  
`Install-Package Microsoft.EntityFrameworkCore.SqlServer` 

#### Scaffold Identity

* From Solution Explorer, right-click on the project > Add > New Scaffolded Item.
* From the left pane of the Add Scaffold dialog, select Identity > Add.
* In the Add Identity dialog, the following options:
* Select the existing layout file ~/Pages/Shared/_Layout.cshtml
* Select Overide All files
* Select the ApplicationDBContext for the context
* Select Add.
* Manually create Data folder in Areas/Identity and add User class (GoThereUser.cs)
* From the Package Manager Console, add a migration and update the database

#### Update Scaffold for custom user class

Several Identity/Pages must be updated to get the custom user class and fields working
* In Startup.Configure Services:
```
services.AddIdentity<GoThereUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();
```
This configures the identity service to use the custom user class

* In Shared/_LoginPartial.cshtml:
```
@using GoThere.Areas.Identity.Data;
@inject SignInManager<GoThereUser> SignInManager
@inject UserManager<GoThereUser> UserManager
```
This switch the user class from the generic to the custom

In ApplicationDBContext.cs:
```
public DbSet<GoThereUser> GoThereUsers { get; set; }
```

In the Login, Register, and Logout pages, update the generic identity user class to the custom user class  (Also _ManageNav need to have the user model changed, check others if an issue arises)

In Identity/Pages/Account/Manage/Index.cshtml.cs:
set get set fields for each user model field to manage
```
Input = new InputModel
{
    PhoneNumber = phoneNumber,
    FirstName = user.FirstName,
    LastName = user.LastName,
    Occupation = user.Occupation,
    Industry = user.Industry,
    PostalCode = user.PostalCode,
};
```

#### Create a model  

[Create a model](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-3.1&tabs=visual-studio) 

#### Create scaffold CRUD pages with EFCore  

Add new controller => MVC EFCore with Pages => enter model => enter context => check generate pages

In Shared/_Layout.cshtml, add a link to the new controller

####  Migration (sync model with database)  

From the NuGet Package Manager Console: 
`Add-Migration InitialCreate`  // The name [InitialCreate] can be whatever
`Update-Database`  

At this point the application should be minimally functional allowing user actions and CRUD operations for the primary model (Location)

#### Add a new field

* Navigate to the Model.cs file of the model that the new field will be added to  
* Inside the model class, add a variable with the type and name of the new field  
* Attributes can be added like [Display()], [RegularExpression()], or [Required] by prepending the new field variable
* Update any views that used the field
* Add a migration
* Update database

#### Form validation

* Form validation is provdied by the associated model's field attributes
* [RegularExpression()], [Required], and [DataType()] are commonly used


#### 