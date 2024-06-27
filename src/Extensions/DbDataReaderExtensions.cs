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
    /// Extensions for <see cref="DbDataReader"/>
    /// </summary>
    public static class DbDataReaderExtensions
    {
        public static DateOnly? GetDateOnlyValue(this DbDataReader dbDataReader, string name)
        {
            var value = GetValue<DateTime?>(dbDataReader, name);

            if (value != null)
            {
                return DateOnly.FromDateTime((DateTime)value);
            }
            else
            {
                return null;
            }
        }

        public static Gender? GetGenderValue(this DbDataReader dbDataReader, string name)
        {
            var value = GetValue<char?>(dbDataReader, name);
            
            if (value != null)
            {
                return value switch
                {
                    'm' => Gender.Male,
                    'w' => Gender.Female,
                    'd' => Gender.Diverse,
                    _ => Gender.Unknown,
                };
            }
            else
            {
                return null;
            }
        }

        public static TValue GetValue<TValue>(this DbDataReader dbDataReader, string name)
        {
            var value = dbDataReader[name];

            if (value != null)
            {
                var t = value.GetType();

                if (t == typeof(DBNull))
                {
                    return default;
                }
                else
                {
                    var convertionType = Nullable.GetUnderlyingType(typeof(TValue));
                    if (convertionType == null)
                    {
                        convertionType = typeof(TValue);
                    }
                    return (TValue)Convert.ChangeType(value, convertionType);
                }
            }
            else
            {
                return default;
            }
        }
    }
}