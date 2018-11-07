using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GustavsBANKS.Repo;
using GustavsBANKS.Models;

namespace GustavsBANKS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            GetCustomers();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
           //app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void GetCustomers()
        {
            try
            {
                using (StreamReader sr = new StreamReader("CustomerFile.txt"))
                {
                    string line;

                    bool creatingCustomers = true;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == "Accounts")
                        {
                            creatingCustomers = false;
                            continue;
                        }
                            

                        if (creatingCustomers)
                        {
                            CreateCustomer(line);
                        }
                        else
                        {
                            CreateAccount(line);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
            }
        }

        private void CreateCustomer(string line)
        {
            var InfoArray = line.Split(";");
            BankRepository.Customers.Add(new Customer
            {
                CustomerId = int.Parse(InfoArray[0]),
                Name = InfoArray[1]
            });
        }

        private void CreateAccount(string line)
        {
            var InfoArray = line.Split(";");
            BankRepository.Customers.FirstOrDefault(c => c.CustomerId == int.Parse(InfoArray[2]))
                .Accounts.Add(new Account
                {
                    AccountNumber = int.Parse(InfoArray[0]),
                    Balance = decimal.Parse(InfoArray[1])

                });
        }

    }
}
