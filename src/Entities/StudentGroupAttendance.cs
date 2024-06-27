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
    /// An entity based on the DaNiS database table "jahrgangsdaten"
    /// </summary>
    public class StudentGroupAttendance
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public int? SchoolYearLevel { get; set; }
        public int? StudentId { get; set; }

        public static StudentGroupAttendance FromDb(DbDataReader reader)
        {
            return new StudentGroupAttendance
            {
                Id = reader.GetValue<int>("Id"),
                GroupId = reader.GetValue<int?>("GruppeId"),
                StudentId = reader.GetValue<int?>("SchuelerId"),
                SchoolYearLevel = reader.GetValue<int?>("Jahrgangsstufe")
            };
        }
    }
}
