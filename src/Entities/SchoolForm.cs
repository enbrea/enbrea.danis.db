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
    /// An entity based on the DaNiS database table "schulform"
    /// </summary>
    public class SchoolForm
    {
        public string Code { get; set; }
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public byte? RelevantForStatistics { get; set; }

        public static SchoolForm FromDb(DbDataReader reader)
        {
            return new SchoolForm
            {
                Id = reader.GetValue<int>("Id"),
                Code = reader.GetValue<string>("Kuerzel"),
                Key = reader.GetValue<string>("Kennzahl"),
                Name = reader.GetValue<string>("Bezeichnung"),
                RelevantForStatistics = reader.GetValue<byte?>("StatRel")
            };
        }
    }
}