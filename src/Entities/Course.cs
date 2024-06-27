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
    /// An entity based on the DaNiS database table "kurs"
    /// </summary>
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SchoolFormId { get; set; }
        public int? SchoolYearId { get; set; }
        public string SpecialName { get; set; }
        public int? SubjectId { get; set; }
        public int? TeacherId { get; set; }

        public static Course FromDb(DbDataReader reader)
        {
            return new Course
            {
                Id = reader.GetValue<int>("Id"),
                Name = reader.GetValue<string>("Bezeichnung"),
                SpecialName = reader.GetValue<string>("Spezial"),
                TeacherId = reader.GetValue<int?>("LehrerId"),
                SubjectId = reader.GetValue<int?>("FachId"),
                SchoolFormId = reader.GetValue<int?>("SchulformId"),
                SchoolYearId = reader.GetValue<int>("SchuljahrId")
            };
        }
    }
}
