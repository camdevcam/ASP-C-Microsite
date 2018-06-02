using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using DevExpress.Xpo.Demo.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DevExpress.Xpo.Demo.Core
{
    public static class XpoDemoDataHelper {
        static readonly string[][] demoData = new string[][]{
            new string[] { "Billy Bob", "Thorton", "sample@hotmail.com" },
            new string[] { "Sarah", "Palin", "sample@yahoo.com" },
            new string[] { "Samantha", "Obscuria", "sample@dmail.com" },
            new string[] { "Jamie", "Foxx", "sample@gmail.com" },
            new string[] { "Daniel", "Craig", "sample@aol.com" },
            new string[] { "Bart", "Simpson", "sample@protonmail.com" },
            new string[] { "Peter", "Gibbons", "sample@gov.org" },
            new string[] { "Bill", "Clinton", "sample@politico.com" },
            new string[] { "Clint", "Eastwood", "sample@harvard.edu" },
        };
        public static IApplicationBuilder UseXpoDemoData(this IApplicationBuilder app) {
            using(var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope()) {
                UnitOfWork uow = scope.ServiceProvider.GetService<UnitOfWork>();
                if(!uow.Query<User>().Any()) {
                    foreach(var row in demoData) {
                        var newUser = new User(uow) {
                            FirstName = row[0],
                            LastName = row[1],
                            Email = row[2]
                        };
                    }
                    uow.CommitChanges();
                }
            }
            return app;
        }
    }
}