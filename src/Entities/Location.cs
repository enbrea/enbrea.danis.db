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
    /// An entity based on the DaNiS database table "standort"
    /// </summary>
    public class Location
    {
        public string AddressLine { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public int Id { get; set; }
        public string Locality { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }

        public static Location FromDb(DbDataReader reader)
        {
            return new Location
            {
                Id = reader.GetValue<int>("Id"),
                Number = reader.GetValue<int>("Nr"),
                Name = reader.GetValue<string>("Name"),
                AddressLine = reader.GetValue<string>("Adresse"),
                PostalCode = reader.GetValue<string>("PLZ"),
                Locality = reader.GetValue<string>("Ort"),
                Phone = reader.GetValue<string>("Telefon"),
                Fax = reader.GetValue<string>("Fax"),
                Email = reader.GetValue<string>("EMail"),
            };
        }
    }
}