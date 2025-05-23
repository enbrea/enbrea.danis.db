﻿#region Enbrea - Copyright (C) STÜBER SYSTEMS GmbH
/*    
 *    Enbrea
 *    
 *    Copyright (C) STÜBER SYSTEMS GmbH
 *
 *    This program is free software: you can redistribute it and/or modify
 *    it under the terms of the GNU Affero General Public License, version 3,
 *    as published by the Free Software Foundation.
 *
 *    This program is distributed in the hope that it will be useful,
 *    but WITHOUT ANY WARRANTY; without even the implied warranty of
 *    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *    GNU Affero General Public License for more details.
 *
 *    You should have received a copy of the GNU Affero General Public License
 *    along with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 */
#endregion

using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Enbrea.Danis.Db.SmokeTest
{
    class Program
    {
        static async Task Main()
        {
            var Configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false)
               .AddJsonFile("appsettings.Development.json", optional: true)
               .Build();

            var appConfig = new AppConfig();
            Configuration.Bind("AppConfig", appConfig);

            var appService = new AppService(appConfig);
            await appService.StartAsync();
        }
    }
}
