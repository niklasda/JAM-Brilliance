// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructuremapMvc.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Configuration;
using System.Web.Configuration;
using System.Web.Mvc;

using JAM.Core.Interfaces;
using JAM.Core.Logic;
using JAM.Core.Services;
using JAM.Brilliance;
using JAM.Brilliance.DependencyResolution;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using StructureMap;

using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(StructuremapMvc), "Start")]
[assembly: ApplicationShutdownMethod(typeof(StructuremapMvc), "End")]

namespace JAM.Brilliance
{
    public static class StructuremapMvc
    {
        public static StructureMapDependencyScope StructureMapDependencyScope { get; set; }

        public static void End()
        {
            StructureMapDependencyScope.Dispose();
        }

        public static void Start()
        {
            var connString = WebConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString;
            var storageConnString = WebConfigurationManager.ConnectionStrings[Constants.StorageConnectionStringName].ConnectionString;

            IContainer container = IoC.Initialize();
            container.Configure(x =>
            {
                x.For<IUserProfile>().Singleton().Use<UserProfile>();
                x.For<IDataCache>().Singleton().Use<DataCache>();

                x.For<IDataStorageConfigurationService>()
                    .Use<DataStorageConfigurationService>()
                    .Ctor<string>().Is(storageConnString);

                x.For<IDatabaseConfigurationService>()
                    .Use<DatabaseConfigurationService>()
                    .Ctor<string>().Is(connString);
            });

            StructureMapDependencyScope = new StructureMapDependencyScope(container);
            DependencyResolver.SetResolver(StructureMapDependencyScope);
            DynamicModuleUtility.RegisterModule(typeof(StructureMapScopeModule));
        }
    }
}