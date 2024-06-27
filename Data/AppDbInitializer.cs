using eTickets.Data.Enums;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;

namespace eTickets.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var servicescope = app.ApplicationServices.CreateScope())
            {
                var context = servicescope.ServiceProvider.GetService<appdbcontext>();
                context.Database.EnsureCreated();

                //Cinema
                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>() {
                    new Cinema()
                    {
                        Name="Cinema 1",
                        LogoImage="",
                        Description="this description of the first cinema"
                    },
                     new Cinema()
                    {
                        Name="Cinema 2",
                        LogoImage="",
                        Description="this description of the first cinema"
                    },
                      new Cinema()
                    {
                        Name="Cinema 3",
                        LogoImage="",
                        Description="this description of the first cinema"
                    }

                    });
                    context.SaveChanges();
                }
                //Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName="Actor 1",
                            ProfileImage="",
                            Bio="this is the bio of the first actor"
                        },
                        new Actor()
                        {
                            FullName="Actor 2",
                            ProfileImage="",
                            Bio="this is the bio of the first actor"
                        },
                        new Actor()
                        {
                            FullName="Actor 3",
                            ProfileImage="",
                            Bio="this is the bio of the first actor"
                        }
                    });
                    context.SaveChanges();

                }
                //Producers
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            FullName="Producer 1",
                            ProfileImage="",
                            Bio="this is the bio of the first Producer"
                        },
                        new Producer()
                        {
                            FullName="Producer 2",
                            ProfileImage="",
                            Bio="this is the bio of the first Producer"
                        },
                        new Producer()
                        {
                            FullName="Producer 3",
                            ProfileImage="",
                            Bio="this is the bio of the first Producer"
                        }
                    });
                    context.SaveChanges();
                }
                //Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>
                    {
                        new Movie()
                        {
                            Name="Ghost",
                            Description="this is the Ghost Movie D",
                            Price=39.5,
                            MovieImage="",
                            StartDate=DateTime.Now.AddDays(-10),
                            EndDate=DateTime.Now.AddDays(-2),
                            CinemaId=2,
                            ProducerId=1,
                            MovieCategory=MovieCategory.Cartoon
                        },
                        new Movie()
                        {
                            Name="Race",
                            Description="this is the Race Movie D",
                            Price=28,
                            MovieImage="",
                            StartDate=DateTime.Now,
                            EndDate=DateTime.Now.AddDays(5),
                            CinemaId=1,
                            ProducerId=3,
                            MovieCategory=MovieCategory.Decomintry
                        }
                    });
                    context.SaveChanges();
                }
                //Actors & Movies
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_movie>
                    {
                        new Actor_movie()
                        {
                            ActorId=1,
                            MovieId=2
                        },
                        new Actor_movie()
                        {
                            ActorId=2,
                            MovieId=2
                        },
                        new Actor_movie()
                        {
                            ActorId=2,
                            MovieId=1
                        },
                        new Actor_movie()
                        {
                            ActorId=3,
                            MovieId=1
                        }
                    });
                    context.SaveChanges();
                }

            }
            
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@etickets.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FirstName = "Admin",
                        LastName = "User",
                        UserName = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Ahmed066");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FirstName = "Application",
                        LastName = "User",
                        UserName = "user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
