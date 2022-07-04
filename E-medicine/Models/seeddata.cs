using E_medicine.Areas.Identity.Data;
using E_medicine.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_medicine.Models
{
    public class seeddata
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<E_medicineUser>>();
            IdentityResult roleResult;
            //Add Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }
            E_medicineUser user = await UserManager.FindByEmailAsync("admin@admin.com");
            if (user == null)
            {
                var User = new E_medicineUser();
                User.Email = "admin@admin.com";
                User.UserName = "admin@admin.com";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Admin"); }
            }
        }
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new E_medicineContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<E_medicineContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();
                // Look for any movies.
                if (context.Company.Any() || context.Disase.Any() || context.Drugs.Any() || context.DisaseDrugs.Any())
                {
                    return; // DB has been seeded
                }
                context.Company.AddRange(
                new Company { Name = "Alkaloid", num_empl = 40, country = "Macedonia",image= "alkaloid.png" },
                  new Company { Name = "Replek", num_empl = 150, country = "Macedonia",image= "replek.png" },
                 new Company { Name = "Moderna", num_empl = 918, country = "USA",image= "Moderna-Logo-1.jpg" },
                   new Company { Name = "Pfizer", num_empl = 1100, country = "USA",image= "Pfizer_new_2021.jpeg" },
                     new Company { Name = "Debiopharm", num_empl = 90, country = "Slovakia",image= "pom.png" },
                      new Company { Name = "Bayer", num_empl = 110, country = "Serbia", image = "bayer.png" }
                );
                context.SaveChanges();
                context.Drugs.AddRange(
                new Drugs { Name = "Acetaminophen", DosageForm = "Powder", Strenght = "25g", CompanyId = context.Company.Single(d => d.Name == "Alkaloid").Id,image="prv.png" },
                new Drugs { Name = "Cymbalta", DosageForm = "Powder", Strenght = "3g", CompanyId = context.Company.Single(d => d.Name == "Alkaloid").Id,image="vtor.png" },
                new Drugs { Name = "Doxycycline", DosageForm = "Powder", Strenght = "10g", CompanyId = context.Company.Single(d => d.Name == "Alkaloid").Id,image="tret.png" },
                new Drugs { Name = "Entresto", DosageForm = "Injection", Strenght = "1g", CompanyId = context.Company.Single(d => d.Name == "Alkaloid").Id,image="cetvrt.png" },
                 new Drugs { Name = "Loratadine", DosageForm = "Powder", Strenght = "5g", CompanyId = context.Company.Single(d => d.Name == "Replek").Id,image="pet.png" },
                new Drugs { Name = "Lyrica", DosageForm = "Injection", Strenght = "10g", CompanyId = context.Company.Single(d => d.Name == "Replek").Id,image="ses.png" },
                new Drugs { Name = "Metoprolol", DosageForm = "Injection", Strenght = "8g", CompanyId = context.Company.Single(d => d.Name == "Replek").Id,image="sedum.png" },
                new Drugs { Name = "Viagra", DosageForm = "Injection", Strenght = "5g", CompanyId = context.Company.Single(d => d.Name == "Replek").Id ,image="osum.png"},
                new Drugs { Name = "Naltrexone", DosageForm = "Powder", Strenght = "5g", CompanyId = context.Company.Single(d => d.Name == "Moderna").Id ,image="devet.png"}
             
                );
                context.SaveChanges();
                
                       context.Disase.AddRange
                (
                 new Disase { Name = "Type1 Diabetes", Organs ="Autoimmune Diseases"},
                new Disase { Name = "MultipleSclerosis", Organs = "Autoimmune Diseases" },
                new Disase { Name = "Crohn's & Colitis", Organs = "Autoimmune Diseases"},
                new Disase { Name = "Lupus", Organs = "Autoimmune Diseases" },
                new Disase { Name = "Rheumatoid Arthritis", Organs = "Autoimmune Diseases"},
                new Disase { Name = "Type1 Diabetes", Organs = "Autoimmune Diseases" },
                new Disase { Name = "Sclerosis", Organs = "Autoimmune Diseases" },
                new Disase { Name = "Chron", Organs = "Autoimmune Diseases" },
                new Disase { Name = "Headache", Organs = "Autoimmune Diseases" },
                new Disase { Name = "LegProblem", Organs = "Autoimmune Diseases" },
                new Disase { Name = "Lupus", Organs = "Autoimmune Diseases"},
                new Disase { Name = "Asthma", Organs = "Autoimmune Diseases" },
                new Disase { Name = "Scleroderma", Organs = "Autoimmune Diseases" },
                new Disase { Name = "Liver", Organs = "Autoimmune Diseases" },
                new Disase { Name = "Cancer", Organs = "Autoimmune Diseases" },
                new Disase { Name = "Lupus", Organs = "Autoimmune Diseases" },
                new Disase { Name = "Chron", Organs = "Autoimmune Diseases" },
                new Disase { Name = "Type1 Diabetes", Organs = "Autoimmune Diseases"},
                new Disase { Name = "Sclerosis", Organs = "Autoimmune Diseases"},
                new Disase { Name = "Headache", Organs = "Autoimmune Diseases"}

                    );
                context.SaveChanges();
                context.DisaseDrugs.AddRange
                    (
                    new DisaseDrugs { DrugId=1,DisaseId=1 },
                    new DisaseDrugs { DrugId = 1, DisaseId = 2 },
                    new DisaseDrugs { DrugId = 2, DisaseId = 1 },
                    new DisaseDrugs { DrugId = 1, DisaseId = 3 },
                    new DisaseDrugs { DrugId = 1, DisaseId = 4 },
                    new DisaseDrugs { DrugId = 1, DisaseId = 5 },
                    new DisaseDrugs { DrugId = 1, DisaseId = 6 }



                    );
                context.SaveChanges();


            }

        }
    }
}