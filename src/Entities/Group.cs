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
    /// An entity based on the DaNiS database table "gruppe"
    /// </summary>
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Teacher1Id { get; set; }
        public int? Teacher2Id { get; set; }
        public int? SchoolFormId { get; set; }
        public int SchoolYearId { get; set; }
        public int SchoolYearLevel { get; set; }
        public string Room { get; set; }

        public static Group FromDb(DbDataReader reader)
        {
            return new Group
            {
                Id = reader.GetValue<int>("ID"),
                Name = reader.GetValue<string>("Bezeichnung"),
                Teacher1Id = reader.GetValue<int?>("LehrerId1"),
                Teacher2Id = reader.GetValue<int?>("LehrerId2"),
                SchoolFormId = reader.GetValue<int?>("SchulformId"),
                SchoolYearId = reader.GetValue<int>("SchuljahrId"),
                SchoolYearLevel = reader.GetValue<int>("Jahrgangsstufe"),
                Room = reader.GetValue<string>("Raum")
            };
        }
    }
}
