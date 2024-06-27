
#region Enbrea - Copyright (C) STÜBER SYSTEMS GmbH
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

using System.Data.Common;

namespace Enbrea.Danis.Db
{
    /// <summary>
    /// An entity based on the DaNiS database table "schuljahr"
    /// </summary>
    public class SchoolYear
    {
        public bool CurrentYear { get; set; }
        public int Id { get; set; }
        public int? Year { get; set; }
        public string Name { get; set; }

        public static SchoolYear FromDb(DbDataReader reader)
        {
            return new SchoolYear
            {
                Id = reader.GetValue<int>("Id"),
                Name = reader.GetValue<string>("Bezeichnung"),
                Year = reader.GetValue<int?>("Jahr"),
                CurrentYear = reader.GetValue<byte>("Aktuell") == 1
            };
        }
    }
}