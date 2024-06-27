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

using System;
using System.Data.Common;

namespace Enbrea.Danis.Db
{
    /// <summary>
    /// An entity based on the DaNiS database table "lehrer"
    /// </summary>
    public class Teacher
    {
        public DateOnly? Birthdate { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string StaffNumber { get; set; }
        public DateOnly? EnrollmentDate { get; set; }
        public string FirstNames { get; set; }
        public Gender? Gender { get; set; }
        public int Id { get; set; }
        public string LastName { get; set; }
        public DateOnly? LeaveDate { get; set; }
        public string Locality { get; set; }
        public int? Nationality1Id { get; set; }
        public int? Nationality2Id { get; set; }
        public string NickName { get; set; }
        public string PlaceOfBirth { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string Title { get; set; }

        public static Teacher FromDb(DbDataReader reader)
        {
            return new Teacher
            {
                Id = reader.GetValue<int>("Id"),
                Code = reader.GetValue<string>("Kuerzel"),
                Title = reader.GetValue<string>("AkadTitel"),
                NickName = reader.GetValue<string>("Rufname"),
                LastName = reader.GetValue<string>("Nachname"),
                FirstNames = reader.GetValue<string>("Vornamen"),
                Street = reader.GetValue<string>("Strasse"),
                PostalCode = reader.GetValue<string>("PLZ"),
                Locality = reader.GetValue<string>("Ort"),
                Suburb = reader.GetValue<string>("Ortsteil"),
                Region = reader.GetValue<string>("Landkreis"),
                Birthdate = reader.GetDateOnlyValue("Geburtsdatum"),
                PlaceOfBirth = reader.GetValue<string>("Geburtsort"),
                Gender = reader.GetGenderValue("Geschlecht"),
                EnrollmentDate = reader.GetDateOnlyValue("Zugang"),
                LeaveDate = reader.GetDateOnlyValue("Abgang"),
                StaffNumber = reader.GetValue<string>("Personalnummer"),
                Nationality1Id = reader.GetValue<int?>("StaatsangehoerigkeitId"),
                Nationality2Id = reader.GetValue<int?>("Staatsangehoerigkeit2Id")
            };
        }
    }
}
