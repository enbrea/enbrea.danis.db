
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
    /// An entity based on the DaNiS database table "belegung"
    /// </summary>
    public class StudentCourseAttendance
    {
        public int? CourseId { get; set; }
        public int? GroupId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StudentId { get; set; }
        public int SubjectId { get; set; }
        public int? TeacherId { get; set; }

        public static StudentCourseAttendance FromDb(DbDataReader reader)
        {
            return new StudentCourseAttendance
            {
                Id = reader.GetValue<int>("Id"),
                Name = reader.GetValue<string>("KursBezeichnung"),
                CourseId = reader.GetValue<int?>("KursId"),
                SubjectId = reader.GetValue<int>("FachId"),
                StudentId = reader.GetValue<int>("SchuelerId"),
                TeacherId = reader.GetValue<int>("LehrerId"),
                GroupId = reader.GetValue<int>("GruppeId")
            };
        }
    }
}
