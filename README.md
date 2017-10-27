# Mario Food Product Site

## By Jonathan Stein

#### _A mockup site for 'Mario's Specialty Food Products' made in DotNet with MVC, with Authentication and Ajax requests_

## Site Specs

 - Database

    - The site should have functionality to review products so your database should include a one-to-many relationship between Products and Reviews. All products must have a name, cost and country of origin. All reviews should have author, content_body and rating. (A rating can be a number between 1 and 5.) You can include other fields of your choosing as well.

- Landing Page

    - The landing page should include basic information about the company and should allow users to easily navigate to other areas of the site. This page should also include the three most recently added products and the three products with the most reviews.
- Products

The site needs to include a products section with a list of the tasty products that Mario sells. Each product should be click-able with a detail view.

   - Site admins should be able to add, update and delete new products. (Don't worry about user authentication; assume everyone who visits the site is an admin for now).
    Users should be able to click an individual product to see its detail page, including its average rating.
    Users should be able to add a review to a product.
    
    
   - Using .NET Core Identity, Employees at Mario's Speciality Food Products can register for admin accounts.
   - Employees can log in and out of their accounts.
   - Admin users are allowed to post new items.
   - Anonymous users should not be able to post new items.
   - Admin users can delete comments by any user.
   - From the product details page, make adding a new review into an AJAX action.


- Your site should include tests for the following:

    - Products and Reviews should be properly saved to the database.
    The Average rating of a Product should be accurately computed and displayed.
    Rating can only be an integer between 1 and 5.
    The review's content_body must be between 50 and 250 characters. (If not, submission should route to an error page).
    The controller should accurately return Products and Reviews in each dedicated route.

## Setup/Installation Requirements

* _Download and install [.NET Core 1.1 SDK](https://www.microsoft.com/net/download/core)_
* _Download and install [Mono](http://www.mono-project.com/download/)_
* _Download and install [MAMP](https://www.mamp.info/en/)_
* _Set MySQL Port to 3306_
* _Clone repository_

### Import Database with Dummies
##### Import from the Cloned Repository
* _Click 'Open start page' in MAMP_
* _Under 'Tools', select 'phpMyAdmin'_
* _Click 'Import' tab_
* _Choose database file (from cloned repository folder)_
* _Click 'Go'_

## Technologies Used
* _C#_
* _.NET_
* _MVC_
* _Entity Framework_
* _[Bootstrap](http://getbootstrap.com/getting-started/)_
* _[MySQL](https://www.mysql.com/)_

### License

Copyright (c) 2017 **_Jonathan Stein_**

*Licensed under the [MIT License](https://opensource.org/licenses/MIT)*


### ASP.Net
#### Packages
* Microsoft.AspNetCore.Mvc - Version 1.1.2
* Microsoft.EntityFrameworkCore - Version 1.1.2
* Pomelo.EntityFrameworkCore.MySql - Version 1.1.2
* Microsoft.AspNetCore.StaticFiles - Version 1.1.2
* Microsoft.AspNetCore - Version 1.1.2

### Migration
#### Add Packages
* Microsoft.EntityFrameworkCore.Design - Version 1.1.2

#### Add to .csproj
```
<Item Group>
  <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="1.0.0" />
</Item Group>
```

#### Commands in terminal or VS Package Console (Windows only)
* `dotnet restore`
* `dotnet ef migrations add Initial`
* `dotnet database update`
